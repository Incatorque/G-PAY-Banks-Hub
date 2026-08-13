using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using GPay.Banking.Absa.Configuration;
using GPay.Banking.Absa.Models.Avs;
using Microsoft.Extensions.Options;

namespace GPay.Banking.Absa.Clients;

/// <summary>
/// Absa Corporate API client used by bank capability services.
/// </summary>
public interface IAbsaCapiClient
{
    /// <summary>
    /// Indicates whether live CAPI credentials are present.
    /// </summary>
    bool IsConfigured { get; }

    /// <summary>
    /// Indicates whether the local AVS simulator should be used.
    /// </summary>
    bool UseSimulator { get; }

    /// <summary>
    /// Calls Absa Account Verification (ValidateBankDetails, and ValidateBankReference when pending).
    /// </summary>
    Task<AbsaAvsResponse> VerifyAccountAsync(AbsaAvsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a raw JSON request to Absa CAPI with security headers.
    /// </summary>
    Task<string> SendAsync(string relativePath, HttpMethod method, string? jsonBody, CancellationToken cancellationToken = default);
}

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

    /// <inheritdoc />
    public bool UseSimulator => _options.UseSimulator;

    /// <inheritdoc />
    public async Task<AbsaAvsResponse> VerifyAccountAsync(
        AbsaAvsRequest request,
        CancellationToken cancellationToken = default)
    {
        if (UseSimulator)
        {
            _logger.LogInformation("Absa AVS simulator used for CorrelationId={CorrelationId}", request.CorrelationId);
            return Simulate(request);
        }

        if (!IsConfigured)
        {
            throw new InvalidOperationException(
                "Absa CAPI is not configured for live AVS. Set Username, Password, CapiCode, ClientApiKey and UseSimulator=false.");
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
        CancellationToken cancellationToken = default)
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

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        _logger.LogInformation(
            "Absa CAPI {Method} {Uri} => {Status}",
            method,
            uri,
            (int)response.StatusCode);

        if (!response.IsSuccessStatusCode && string.IsNullOrWhiteSpace(responseBody))
        {
            throw new HttpRequestException($"Absa CAPI call failed with status {(int)response.StatusCode}.");
        }

        return responseBody;
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

    private static AbsaAvsResponse Simulate(AbsaAvsRequest request)
    {
        var hasIdentity = !string.IsNullOrWhiteSpace(request.ClientIdNumber);
        var hasName = !string.IsNullOrWhiteSpace(request.ClientName);

        return new AbsaAvsResponse
        {
            Status = 5,
            ReferenceNumber = "SIM" + DateTime.UtcNow.ToString("HHmmss"),
            CorrelationId = request.CorrelationId,
            ValueList =
            [
                new AbsaAvsValueItem { Key = "Account Open", Value = "Yes" },
                new AbsaAvsValueItem { Key = "Account Found", Value = "Yes" },
                new AbsaAvsValueItem { Key = "Account Open Longer Than 3 Months", Value = "Yes" },
                new AbsaAvsValueItem { Key = "ID Matched", Value = hasIdentity ? "Yes" : "Unverified" },
                new AbsaAvsValueItem { Key = "Name Matched", Value = hasName ? "Yes" : "Unverified" },
                new AbsaAvsValueItem { Key = "Account Allows Credit", Value = "Yes" },
                new AbsaAvsValueItem { Key = "Account Accepts Credit", Value = "Yes" },
                new AbsaAvsValueItem { Key = "Account Allows Debit", Value = "Yes" },
                new AbsaAvsValueItem { Key = "Account Accepts Debit", Value = "Yes" },
                new AbsaAvsValueItem
                {
                    Key = "Email Address Match",
                    Value = string.IsNullOrWhiteSpace(request.EmailAddress) ? "Unverified" : "Yes"
                },
                new AbsaAvsValueItem
                {
                    Key = "Cell Number Match",
                    Value = string.IsNullOrWhiteSpace(request.CellNumber) ? "Unverified" : "Yes"
                }
            ],
            ErrorList = [],
            ResponseUtcTime = DateTime.UtcNow.ToString("O"),
            UserFriendlyDisplayList = []
        };
    }
}

/// <summary>
/// Configures the Absa <see cref="HttpClient"/> including optional mTLS.
/// </summary>
public static class AbsaHttpClientConfigurator
{
    /// <summary>
    /// Applies certificate handler when configured.
    /// </summary>
    public static IHttpClientBuilder ConfigureAbsaPrimaryHandler(this IHttpClientBuilder builder)
    {
        builder.ConfigurePrimaryHttpMessageHandler(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AbsaCapiOptions>>().Value;
            var handler = new HttpClientHandler();

            if (!string.IsNullOrWhiteSpace(options.ClientCertificatePath) &&
                File.Exists(options.ClientCertificatePath))
            {
                var cert = string.IsNullOrWhiteSpace(options.ClientCertificatePassword)
                    ? X509CertificateLoader.LoadPkcs12FromFile(options.ClientCertificatePath, null)
                    : X509CertificateLoader.LoadPkcs12FromFile(
                        options.ClientCertificatePath,
                        options.ClientCertificatePassword);

                handler.ClientCertificates.Add(cert);
            }

            return handler;
        });

        return builder;
    }
}
