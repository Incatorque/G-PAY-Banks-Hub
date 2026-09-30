using GPay.Banking.Services.Absa;
using GPay.Banking;
using GPay.Banking.Domain;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using GPay.Banking.Services;
using Microsoft.Extensions.Options;

namespace GPay.Banking.Services.Absa.Api.Clients;

/// <summary>
/// Username/password session provider with in-memory cache (~25 minutes).
/// </summary>
public sealed class AbsaSessionProvider : IAbsaSessionProvider
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = null
    };

    private readonly HttpClient _httpClient;
    private readonly IAbsaRequestSigner _requestSigner;
    private readonly AbsaCapiOptions _options;
    private readonly ILogger<AbsaSessionProvider> _logger;
    private readonly IBankApiCallAuditor _auditor;
    private readonly SemaphoreSlim _lock = new(1, 1);

    private string? _session;
    private DateTimeOffset _expiresAt = DateTimeOffset.MinValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbsaSessionProvider"/> class.
    /// </summary>
    public AbsaSessionProvider(
        HttpClient httpClient,
        IAbsaRequestSigner requestSigner,
        IOptions<AbsaCapiOptions> options,
        ILogger<AbsaSessionProvider> logger,
        IBankApiCallAuditor auditor)
    {
        _httpClient = httpClient;
        _requestSigner = requestSigner;
        _options = options.Value;
        _logger = logger;
        _auditor = auditor;
    }

    /// <inheritdoc />
    public async Task<string> GetSessionAsync(CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrWhiteSpace(_session) && DateTimeOffset.UtcNow < _expiresAt)
        {
            return _session!;
        }

        await _lock.WaitAsync(cancellationToken);
        try
        {
            if (!string.IsNullOrWhiteSpace(_session) && DateTimeOffset.UtcNow < _expiresAt)
            {
                return _session!;
            }

            if (!IsLiveCredential(_options.Username) || !IsLiveCredential(_options.Password))
            {
                throw new InvalidOperationException(
                    "AbsaCapi Username/Password are not configured. Set AbsaCapi__Username / AbsaCapi__Password " +
                    "via appsettings.Local.json, user-secrets, or GPAY_AbsaCapi__* environment variables.");
            }

            var uri = BuildUri(_options.AuthenticatePath);
            var payload = JsonSerializer.Serialize(new
            {
                Username = _options.Username,
                Password = _options.Password
            }, JsonOptions);

            using var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(payload, Encoding.UTF8, "application/json")
            };
            _requestSigner.ApplyHeaders(request, payload);

            var started = Stopwatch.StartNew();
            int? statusCode = null;
            string? body = null;
            var success = false;
            try
            {
                _logger.LogInformation("Absa CAPI authenticate {Uri}", uri);

                using var response = await _httpClient.SendAsync(request, cancellationToken);
                statusCode = (int)response.StatusCode;
                body = await response.Content.ReadAsStringAsync(cancellationToken);
                success = response.IsSuccessStatusCode;

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Absa authenticate failed: {Status} {Body}", statusCode, body);
                    throw new InvalidOperationException($"Absa authenticate failed with status {statusCode}: {body}");
                }

                var auth = JsonSerializer.Deserialize<AuthenticateResponse>(body, JsonOptions)
                    ?? throw new InvalidOperationException("Unable to deserialize Absa authenticate response.");

                if (auth.Success != true || string.IsNullOrWhiteSpace(auth.Session))
                {
                    var error = auth.ErrorList?.FirstOrDefault()?.Description ?? "Authentication unsuccessful.";
                    throw new InvalidOperationException($"Absa authenticate rejected: {error}");
                }

                _session = auth.Session;
                _expiresAt = DateTimeOffset.UtcNow.AddSeconds(Math.Max(60, _options.SessionCacheSeconds));
                _logger.LogInformation(
                    "Absa session acquired CorrelationId={CorrelationId} ExpiresAt={ExpiresAt}",
                    auth.CorrelationId,
                    _expiresAt);
                return _session;
            }
            finally
            {
                started.Stop();
                await AbsaCallAudit.WriteAsync(
                    _auditor,
                    _logger,
                    new BankApiCallAuditEntry
                    {
                        Direction = "Outbound",
                        Operation = "Authenticate",
                        HttpMethod = HttpMethod.Post.Method,
                        Path = uri.AbsolutePath,
                        HttpStatus = statusCode,
                        RequestJson = AbsaApiPayloadRedactor.Redact(payload),
                        ResponseJson = AbsaApiPayloadRedactor.Redact(body),
                        DurationMs = started.ElapsedMilliseconds,
                        Success = success
                    },
                    cancellationToken);
            }
        }
        finally
        {
            _lock.Release();
        }
    }

    private Uri BuildUri(string relativePath)
    {
        var baseUrl = _options.BaseUrl.TrimEnd('/') + "/";
        return new Uri(new Uri(baseUrl), relativePath.TrimStart('/'));
    }

    private static bool IsLiveCredential(string? value) =>
        !string.IsNullOrWhiteSpace(value) &&
        !value.StartsWith("SET_", StringComparison.OrdinalIgnoreCase);
}
