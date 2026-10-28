using System.Text;
using System.Text.Json;
using GPay.Banking;
using GPay.Banking.Domain;
using GPay.Banking.Services;
using GPay.Banking.Services.Absa;
using GPay.Banking.Services.Absa.Api.Models.Avs;
using GPay.Banking.Services.Absa.Api.Models.Payment;

namespace GPay.Banking.Services.Absa.Api.Clients;

/// <summary>
/// HTTP implementation of Absa CAPI AVS per MIG – AVS API v1.
/// </summary>
internal sealed class AbsaCapiClient : IAbsaCapiClient
{
    private readonly HttpClient _httpClient;
    private readonly IAbsaSessionProvider _sessionProvider;
    private readonly IAbsaRequestSigner _requestSigner;
    private readonly AbsaCapiOptions _options;
    private readonly ILogger<AbsaCapiClient> _logger;
    private readonly string _missingConfiguration;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbsaCapiClient"/> class.
    /// </summary>
    public AbsaCapiClient(
        HttpClient httpClient,
        IAbsaSessionProvider sessionProvider,
        IAbsaRequestSigner requestSigner,
        IOptions<AbsaCapiOptions> options,
        ILogger<AbsaCapiClient> logger)
    {
        _httpClient = httpClient;
        _sessionProvider = sessionProvider;
        _requestSigner = requestSigner;
        _options = options.Value;
        _logger = logger;
        _missingConfiguration = string.Join(", ", GetMissingConfiguration(_options));
    }

    private bool IsConfigured => _missingConfiguration.Length == 0;

    private static List<string> GetMissingConfiguration(AbsaCapiOptions options)
    {
        var missing = new List<string>();
        if (string.IsNullOrWhiteSpace(options.BaseUrl))
        {
            missing.Add("BaseUrl");
        }

        if (!AbsaCapiOptions.IsConfiguredCredential(options.Username))
        {
            missing.Add("Username");
        }

        if (!AbsaCapiOptions.IsConfiguredCredential(options.Password))
        {
            missing.Add("Password");
        }

        if (!AbsaCapiOptions.IsConfiguredCredential(options.CapiCode))
        {
            missing.Add("CapiCode");
        }

        if (!AbsaCapiOptions.IsConfiguredCredential(options.ClientApiKey))
        {
            missing.Add("ClientApiKey");
        }

        return missing;
    }

    private void EnsureConfigured(string operation, bool includeConfigurationHelp = false)
    {
        if (IsConfigured)
        {
            return;
        }

        var message = $"Absa CAPI is not configured for {operation}. Missing or placeholder: {_missingConfiguration}.";
        if (includeConfigurationHelp)
        {
            message += " Set values in appsettings.Absa.json, appsettings.Local.json, user-secrets, or GPAY_AbsaCapi__* environment variables. Local settings override bank settings. Placeholders starting with SET_ are ignored.";
        }

        throw new InvalidOperationException(message);
    }

    /// <inheritdoc />
    public async Task<AbsaAvsResponse> VerifyAccountAsync(
        AbsaAvsRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureConfigured("live AVS", includeConfigurationHelp: true);

        request.Session = await _sessionProvider.GetSessionAsync(cancellationToken);
        var detailsJson = JsonSerializer.Serialize(request, AbsaJsonOptions.Default);

        _logger.LogInformation(
            "Absa ValidateBankDetails BankCode={BankCode} Account={Account} Branch={Branch} CorrelationId={CorrelationId}",
            request.BankCode,
            request.AccountNumber,
            request.BranchCode,
            request.CorrelationId);

        var detailsBody = await SendAsync(_options.ValidateBankDetailsPath, HttpMethod.Post, detailsJson, cancellationToken);
        var detailsResponse = Deserialize(detailsBody, request.CorrelationId);

        if (detailsResponse.HasErrors)
        {
            return detailsResponse;
        }

        // Absa (on-us): synchronous — ValueList returned on first call.
        if (detailsResponse.IsSuccessStatus && detailsResponse.ValueList is { Count: > 0 })
        {
            return detailsResponse;
        }

        // Non-Absa: pending → poll ValidateBankReference with ReferenceNumber.
        if (detailsResponse.IsPendingStatus ||
            (IsNonAbsaBank(request.BankCode) && !string.IsNullOrWhiteSpace(detailsResponse.ReferenceNumber)))
        {
            return await PollBankReferenceAsync(detailsResponse, request, cancellationToken);
        }

        return detailsResponse;
    }

    /// <inheritdoc />
    public async Task<AbsaPaymentInitiateResponse> InitiatePaymentAsync(
        AbsaPaymentInitiateRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureConfigured("live payment", includeConfigurationHelp: true);

        request.Session = await _sessionProvider.GetSessionAsync(cancellationToken);
        var json = JsonSerializer.Serialize(request, AbsaJsonOptions.Default);

        _logger.LogInformation(
            "Absa PaymentInitiate Amount={Amount} Currency={Currency} Rail={Rail} TxRef={TxRef}",
            request.Economics.Amount,
            request.Economics.CurrencyCode,
            request.Economics.Indicator,
            request.Economics.TransactionRef);

        var body = await SendAsync(
            _options.PaymentInitiatePath,
            HttpMethod.Post,
            json,
            cancellationToken,
            request.GpayOrderId);
        return DeserializePaymentInitiate(body);
    }

    /// <inheritdoc />
    public async Task<AbsaPaymentStatusResponse> GetPaymentStatusAsync(
        AbsaPaymentStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureConfigured("live payment status");

        request.Session = await _sessionProvider.GetSessionAsync(cancellationToken);
        var json = JsonSerializer.Serialize(request, AbsaJsonOptions.Default);

        _logger.LogInformation(
            "Absa PaymentStatus Correlations={Count}",
            request.Correlations.Count);

        var body = await SendAsync(
            _options.PaymentStatusPath,
            HttpMethod.Post,
            json,
            cancellationToken,
            request.GpayOrderId);
        return DeserializePaymentStatus(body);
    }

    /// <inheritdoc />
    public async Task<AbsaPaymentCallbackRegisterResponse> RegisterPaymentCallbackAsync(
        AbsaPaymentCallbackRegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureConfigured("payment callback register");

        request.Session = await _sessionProvider.GetSessionAsync(cancellationToken);
        var json = JsonSerializer.Serialize(request, AbsaJsonOptions.Default);
        var body = await SendAsync(_options.PaymentCallbackRegisterPath, HttpMethod.Post, json, cancellationToken);
        return DeserializePaymentCallbackRegister(body);
    }

    /// <inheritdoc />
    public async Task<AbsaPaymentCallbackRegisterResponse> UnregisterPaymentCallbackAsync(
        AbsaPaymentCallbackUnregisterRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureConfigured("payment callback unregister");

        request.Session = await _sessionProvider.GetSessionAsync(cancellationToken);
        var json = JsonSerializer.Serialize(request, AbsaJsonOptions.Default);
        var body = await SendAsync(_options.PaymentCallbackUnregisterPath, HttpMethod.Post, json, cancellationToken);
        return DeserializePaymentCallbackRegister(body);
    }

    private async Task<AbsaAvsResponse> PollBankReferenceAsync(
        AbsaAvsResponse detailsResponse,
        AbsaAvsRequest request,
        CancellationToken cancellationToken)
    {
        var referenceNumber = detailsResponse.ReferenceNumber;
        if (string.IsNullOrWhiteSpace(referenceNumber))
        {
            return new AbsaAvsResponse
            {
                Status = "033",
                CorrelationId = request.CorrelationId,
                ErrorList =
                [
                    new AbsaAvsErrorItem
                    {
                        Code = "MISSING_REFERENCE",
                        Description = "ValidateBankDetails did not return ReferenceNumber for non-Absa enquiry."
                    }
                ]
            };
        }

        var maxAttempts = Math.Max(1, _options.ReferencePollMaxAttempts);
        var delay = Math.Max(200, _options.ReferencePollIntervalMs);
        AbsaAvsResponse? last = detailsResponse;

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var session = await _sessionProvider.GetSessionAsync(cancellationToken);
            var referenceRequest = new AbsaValidateBankReferenceRequest
            {
                ReferenceNumber = referenceNumber,
                CapiCode = request.CapiCode,
                Session = session
            };
            var json = JsonSerializer.Serialize(referenceRequest, AbsaJsonOptions.Default);

            _logger.LogInformation(
                "Absa ValidateBankReference attempt={Attempt}/{Max} Ref={Ref} CorrelationId={CorrelationId}",
                attempt,
                maxAttempts,
                referenceNumber,
                request.CorrelationId);

            var body = await SendAsync(_options.ValidateBankReferencePath, HttpMethod.Post, json, cancellationToken);
            last = Deserialize(body, request.CorrelationId);

            if (last.HasErrors)
            {
                return last;
            }

            if (last.IsSuccessStatus && last.ValueList is { Count: > 0 })
            {
                return last;
            }

            if (last.StatusCode is "033" or "099")
            {
                return last;
            }

            if (attempt < maxAttempts)
            {
                await Task.Delay(delay, cancellationToken);
            }
        }

        return last ?? detailsResponse;
    }

    private async Task<string> SendAsync(
        string relativePath,
        HttpMethod method,
        string? jsonBody,
        CancellationToken cancellationToken = default,
        Guid? gpayOrderId = null)
    {
        var uri = AbsaUriBuilder.Resolve(_options.BaseUrl, relativePath);

        using var request = new HttpRequestMessage(method, uri);
        if (gpayOrderId is Guid orderId)
        {
            request.Options.Set(BankApiCallAuditMetadata.GpayOrderId, orderId);
        }

        if (jsonBody is not null)
        {
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        }

        _requestSigner.ApplyHeaders(request, jsonBody);

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        _logger.LogInformation(
            "Absa CAPI {Method} {Uri} => {Status}",
            method,
            uri,
            (int)response.StatusCode);

        if (!response.IsSuccessStatusCode && string.IsNullOrWhiteSpace(responseBody))
        {
            throw new HttpRequestException(
                $"Absa CAPI call failed with status {(int)response.StatusCode}.");
        }

        return responseBody;
    }

    private static AbsaAvsResponse Deserialize(string body, string? correlationId)
    {
        return DeserializeOrFallback(body, () => new AbsaAvsResponse
        {
            Status = "033",
            CorrelationId = correlationId,
            ErrorList =
            [
                new AbsaAvsErrorItem
                {
                    Code = "DESERIALIZE_ERROR",
                    Description = "Unable to deserialize Absa AVS response."
                }
            ]
        });
    }

    /// <summary>
    /// Absa bank codes per MIG: 000016 (or sample shorthand "16").
    /// </summary>
    internal static bool IsAbsaBank(string? bankCode) => bankCode?.Trim().TrimStart('0') == "16";

    internal static bool IsNonAbsaBank(string? bankCode) => !IsAbsaBank(bankCode);

    private static AbsaPaymentInitiateResponse DeserializePaymentInitiate(string body)
    {
        return DeserializeOrFallback(body, () => new AbsaPaymentInitiateResponse
        {
            Status = 0,
            ErrorList = [CreateDeserializeError("Unable to deserialize Absa payment initiate response.")]
        });
    }

    private static AbsaPaymentStatusResponse DeserializePaymentStatus(string body)
    {
        return DeserializeOrFallback(body, () => new AbsaPaymentStatusResponse
        {
            ErrorList = [CreateDeserializeError("Unable to deserialize Absa payment status response.")]
        });
    }

    private static AbsaPaymentCallbackRegisterResponse DeserializePaymentCallbackRegister(string body)
    {
        return DeserializeOrFallback(body, () => new AbsaPaymentCallbackRegisterResponse
        {
            Status = 0,
            ErrorList = [CreateDeserializeError("Unable to deserialize Absa payment callback register response.")]
        });
    }

    private static AbsaPaymentErrorItem CreateDeserializeError(string description) => new()
    {
        Code = "DESERIALIZE_ERROR",
        Description = description
    };

    private static T DeserializeOrFallback<T>(string body, Func<T> fallback) where T : class
    {
        try
        {
            return JsonSerializer.Deserialize<T>(body, AbsaJsonOptions.Default) ?? fallback();
        }
        catch (JsonException)
        {
            return fallback();
        }
    }
}
