using GPay.Banking.Services.Absa;
using GPay.Banking;
using GPay.Banking.Domain;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using GPay.Banking.Services;
using GPay.Banking.Services.Absa.Api.Models.Avs;
using GPay.Banking.Services.Absa.Api.Models.Payment;
using Microsoft.Extensions.Options;

namespace GPay.Banking.Services.Absa.Api.Clients;

/// <summary>
/// HTTP implementation of Absa CAPI AVS per MIG – AVS API v1.
/// </summary>
public sealed class AbsaCapiClient : IAbsaCapiClient
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = null,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    private readonly HttpClient _httpClient;
    private readonly IAbsaSessionProvider _sessionProvider;
    private readonly IAbsaRequestSigner _requestSigner;
    private readonly AbsaCapiOptions _options;
    private readonly ILogger<AbsaCapiClient> _logger;
    private readonly IBankApiCallAuditor _auditor;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbsaCapiClient"/> class.
    /// </summary>
    public AbsaCapiClient(
        HttpClient httpClient,
        IAbsaSessionProvider sessionProvider,
        IAbsaRequestSigner requestSigner,
        IOptions<AbsaCapiOptions> options,
        ILogger<AbsaCapiClient> logger,
        IBankApiCallAuditor auditor)
    {
        _httpClient = httpClient;
        _sessionProvider = sessionProvider;
        _requestSigner = requestSigner;
        _options = options.Value;
        _logger = logger;
        _auditor = auditor;
    }

    /// <inheritdoc />
    public bool IsConfigured =>
        !string.IsNullOrWhiteSpace(_options.BaseUrl) &&
        IsLiveCredential(_options.Username) &&
        IsLiveCredential(_options.Password) &&
        IsLiveCredential(_options.CapiCode) &&
        IsLiveCredential(_options.ClientApiKey);

    private static bool IsLiveCredential(string? value) =>
        !string.IsNullOrWhiteSpace(value) &&
        !value.StartsWith("SET_", StringComparison.OrdinalIgnoreCase);

    private string DescribeMissingConfiguration()
    {
        var missing = new List<string>();
        if (string.IsNullOrWhiteSpace(_options.BaseUrl))
        {
            missing.Add("BaseUrl");
        }

        if (!IsLiveCredential(_options.Username))
        {
            missing.Add("Username");
        }

        if (!IsLiveCredential(_options.Password))
        {
            missing.Add("Password");
        }

        if (!IsLiveCredential(_options.CapiCode))
        {
            missing.Add("CapiCode");
        }

        if (!IsLiveCredential(_options.ClientApiKey))
        {
            missing.Add("ClientApiKey");
        }

        return string.Join(", ", missing);
    }

    /// <inheritdoc />
    public async Task<AbsaAvsResponse> VerifyAccountAsync(
        AbsaAvsRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!IsConfigured)
        {
            throw new InvalidOperationException(
                $"Absa CAPI is not configured for live AVS. Missing or placeholder: {DescribeMissingConfiguration()}. " +
                "Set values in appsettings.Absa.json, appsettings.Local.json, user-secrets, or GPAY_AbsaCapi__* environment variables. Local settings override bank settings. Placeholders starting with SET_ are ignored.");
        }

        request.Session = await _sessionProvider.GetSessionAsync(cancellationToken);
        var detailsJson = JsonSerializer.Serialize(request, JsonOptions);

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
        if (!IsConfigured)
        {
            throw new InvalidOperationException(
                $"Absa CAPI is not configured for live payment. Missing or placeholder: {DescribeMissingConfiguration()}. " +
                "Set values in appsettings.Absa.json, appsettings.Local.json, user-secrets, or GPAY_AbsaCapi__* environment variables. Local settings override bank settings. Placeholders starting with SET_ are ignored.");
        }

        request.Session = await _sessionProvider.GetSessionAsync(cancellationToken);
        var json = JsonSerializer.Serialize(request, JsonOptions);

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
        if (!IsConfigured)
        {
            throw new InvalidOperationException(
                $"Absa CAPI is not configured for live payment status. Missing or placeholder: {DescribeMissingConfiguration()}.");
        }

        request.Session = await _sessionProvider.GetSessionAsync(cancellationToken);
        var json = JsonSerializer.Serialize(request, JsonOptions);

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
        if (!IsConfigured)
        {
            throw new InvalidOperationException(
                $"Absa CAPI is not configured for payment callback register. Missing or placeholder: {DescribeMissingConfiguration()}.");
        }

        request.Session = await _sessionProvider.GetSessionAsync(cancellationToken);
        var json = JsonSerializer.Serialize(request, JsonOptions);
        var body = await SendAsync(_options.PaymentCallbackRegisterPath, HttpMethod.Post, json, cancellationToken);
        return DeserializePaymentCallbackRegister(body);
    }

    /// <inheritdoc />
    public async Task<AbsaPaymentCallbackRegisterResponse> UnregisterPaymentCallbackAsync(
        AbsaPaymentCallbackUnregisterRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!IsConfigured)
        {
            throw new InvalidOperationException(
                $"Absa CAPI is not configured for payment callback unregister. Missing or placeholder: {DescribeMissingConfiguration()}.");
        }

        request.Session = await _sessionProvider.GetSessionAsync(cancellationToken);
        var json = JsonSerializer.Serialize(request, JsonOptions);
        var body = await SendAsync(_options.PaymentCallbackUnregisterPath, HttpMethod.Post, json, cancellationToken);
        return DeserializePaymentCallbackRegister(body);
    }

    private async Task<AbsaAvsResponse> PollBankReferenceAsync(
        AbsaAvsResponse detailsResponse,
        AbsaAvsRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(detailsResponse.ReferenceNumber))
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
                ReferenceNumber = detailsResponse.ReferenceNumber!,
                CapiCode = request.CapiCode,
                Session = session
            };
            var json = JsonSerializer.Serialize(referenceRequest, JsonOptions);

            _logger.LogInformation(
                "Absa ValidateBankReference attempt={Attempt}/{Max} Ref={Ref} CorrelationId={CorrelationId}",
                attempt,
                maxAttempts,
                detailsResponse.ReferenceNumber,
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

    /// <inheritdoc />
    public async Task<string> SendAsync(
        string relativePath,
        HttpMethod method,
        string? jsonBody,
        CancellationToken cancellationToken = default,
        Guid? gpayOrderId = null)
    {
        if (string.IsNullOrWhiteSpace(_options.BaseUrl))
        {
            throw new InvalidOperationException("AbsaCapi:BaseUrl is not configured.");
        }

        var uri = relativePath.StartsWith("http", StringComparison.OrdinalIgnoreCase)
            ? new Uri(relativePath)
            : new Uri(new Uri(_options.BaseUrl.TrimEnd('/') + "/"), relativePath.TrimStart('/'));

        using var request = new HttpRequestMessage(method, uri);
        if (jsonBody is not null)
        {
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        }

        _requestSigner.ApplyHeaders(request, jsonBody);

        var started = Stopwatch.StartNew();
        int? statusCode = null;
        string? responseBody = null;
        string? errorCode = null;
        var success = false;
        try
        {
            using var response = await _httpClient.SendAsync(request, cancellationToken);
            statusCode = (int)response.StatusCode;
            responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            success = response.IsSuccessStatusCode || !string.IsNullOrWhiteSpace(responseBody);

            _logger.LogInformation(
                "Absa CAPI {Method} {Uri} => {Status}",
                method,
                uri,
                statusCode);

            if (!response.IsSuccessStatusCode && string.IsNullOrWhiteSpace(responseBody))
            {
                errorCode = statusCode.ToString();
                throw new HttpRequestException($"Absa CAPI call failed with status {statusCode}.");
            }

            return responseBody;
        }
        finally
        {
            started.Stop();
            await AbsaCallAudit.WriteAsync(
                _auditor,
                _logger,
                new BankApiCallAuditEntry
                {
                    GpayOrderId = gpayOrderId,
                    Direction = "Outbound",
                    Operation = OperationName(relativePath),
                    HttpMethod = method.Method,
                    Path = uri.AbsolutePath,
                    HttpStatus = statusCode,
                    RequestJson = AbsaApiPayloadRedactor.Redact(jsonBody),
                    ResponseJson = AbsaApiPayloadRedactor.Redact(responseBody),
                    DurationMs = started.ElapsedMilliseconds,
                    Success = success,
                    ErrorCode = errorCode
                },
                cancellationToken);
        }
    }

    private static string OperationName(string relativePath)
    {
        var path = relativePath.Split('?', 2)[0].Trim('/');
        var slash = path.LastIndexOf('/');
        return slash >= 0 ? path[(slash + 1)..] : path;
    }

    private static AbsaAvsResponse Deserialize(string body, string? correlationId)
    {
        try
        {
            return JsonSerializer.Deserialize<AbsaAvsResponse>(body, JsonOptions)
                   ?? new AbsaAvsResponse
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
                   };
        }
        catch (JsonException)
        {
            return new AbsaAvsResponse
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
            };
        }
    }

    /// <summary>
    /// Absa bank codes per MIG: 000016 (or sample shorthand "16").
    /// </summary>
    internal static bool IsAbsaBank(string? bankCode)
    {
        if (string.IsNullOrWhiteSpace(bankCode))
        {
            return false;
        }

        var normalized = bankCode.Trim().TrimStart('0');
        return normalized == "16";
    }

    internal static bool IsNonAbsaBank(string? bankCode) => !IsAbsaBank(bankCode);

    private static AbsaPaymentInitiateResponse DeserializePaymentInitiate(string body)
    {
        try
        {
            return JsonSerializer.Deserialize<AbsaPaymentInitiateResponse>(body, JsonOptions)
                   ?? new AbsaPaymentInitiateResponse
                   {
                       Status = 0,
                       ErrorList =
                       [
                           new AbsaPaymentErrorItem
                           {
                               Code = "DESERIALIZE_ERROR",
                               Description = "Unable to deserialize Absa payment initiate response."
                           }
                       ]
                   };
        }
        catch (JsonException)
        {
            return new AbsaPaymentInitiateResponse
            {
                Status = 0,
                ErrorList =
                [
                    new AbsaPaymentErrorItem
                    {
                        Code = "DESERIALIZE_ERROR",
                        Description = "Unable to deserialize Absa payment initiate response."
                    }
                ]
            };
        }
    }

    private static AbsaPaymentStatusResponse DeserializePaymentStatus(string body)
    {
        try
        {
            return JsonSerializer.Deserialize<AbsaPaymentStatusResponse>(body, JsonOptions)
                   ?? new AbsaPaymentStatusResponse
                   {
                       ErrorList =
                       [
                           new AbsaPaymentErrorItem
                           {
                               Code = "DESERIALIZE_ERROR",
                               Description = "Unable to deserialize Absa payment status response."
                           }
                       ]
                   };
        }
        catch (JsonException)
        {
            return new AbsaPaymentStatusResponse
            {
                ErrorList =
                [
                    new AbsaPaymentErrorItem
                    {
                        Code = "DESERIALIZE_ERROR",
                        Description = "Unable to deserialize Absa payment status response."
                    }
                ]
            };
        }
    }

    private static AbsaPaymentCallbackRegisterResponse DeserializePaymentCallbackRegister(string body)
    {
        try
        {
            return JsonSerializer.Deserialize<AbsaPaymentCallbackRegisterResponse>(body, JsonOptions)
                   ?? new AbsaPaymentCallbackRegisterResponse
                   {
                       Status = 0,
                       ErrorList =
                       [
                           new AbsaPaymentErrorItem
                           {
                               Code = "DESERIALIZE_ERROR",
                               Description = "Unable to deserialize Absa payment callback register response."
                           }
                       ]
                   };
        }
        catch (JsonException)
        {
            return new AbsaPaymentCallbackRegisterResponse
            {
                Status = 0,
                ErrorList =
                [
                    new AbsaPaymentErrorItem
                    {
                        Code = "DESERIALIZE_ERROR",
                        Description = "Unable to deserialize Absa payment callback register response."
                    }
                ]
            };
        }
    }
}
