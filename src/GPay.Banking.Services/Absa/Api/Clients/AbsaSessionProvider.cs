using System.Text;
using System.Text.Json;

namespace GPay.Banking.Services.Absa.Api.Clients;

/// <summary>
/// Username/password session provider with in-memory cache (~25 minutes).
/// </summary>
internal sealed class AbsaSessionProvider : IAbsaSessionProvider
{
    private readonly HttpClient _httpClient;
    private readonly IAbsaRequestSigner _requestSigner;
    private readonly AbsaCapiOptions _options;
    private readonly ILogger<AbsaSessionProvider> _logger;
    private readonly SemaphoreSlim _lock = new(1, 1);

    private string? _session;
    private DateTimeOffset _expiresAt = DateTimeOffset.MinValue;

    private string? ValidSession =>
        string.IsNullOrWhiteSpace(_session) || DateTimeOffset.UtcNow >= _expiresAt ? null : _session;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbsaSessionProvider"/> class.
    /// </summary>
    public AbsaSessionProvider(HttpClient httpClient, IAbsaRequestSigner requestSigner, IOptions<AbsaCapiOptions> options, ILogger<AbsaSessionProvider> logger)
    {
        _httpClient = httpClient;
        _requestSigner = requestSigner;
        _options = options.Value;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<string> GetSessionAsync(CancellationToken cancellationToken = default)
    {
        if (ValidSession is { } cachedSession)
        {
            return cachedSession;
        }

        await _lock.WaitAsync(cancellationToken);
        try
        {
            if (ValidSession is { } refreshedSession)
            {
                return refreshedSession;
            }

            if (!AbsaCapiOptions.IsConfiguredCredential(_options.Username) ||
                !AbsaCapiOptions.IsConfiguredCredential(_options.Password))
            {
                throw new InvalidOperationException(
                    "AbsaCapi Username/Password are not configured. Set AbsaCapi__Username / AbsaCapi__Password " +
                    "via appsettings.Absa.json, appsettings.Local.json, user-secrets, or GPAY_AbsaCapi__* environment variables.");
            }

            var uri = AbsaUriBuilder.Build(_options.BaseUrl, _options.AuthenticatePath);

            var payload = JsonSerializer.Serialize(new
            {
                _options.Username,
                _options.Password
            }, AbsaJsonOptions.Default);

            using var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(payload, Encoding.UTF8, "application/json")
            };

            _requestSigner.ApplyHeaders(request, payload);

            _logger.LogInformation("Absa CAPI authenticate {Uri}", uri);

            using var response = await _httpClient.SendAsync(request, cancellationToken);

            var body = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Absa authenticate failed: {Status} {Body}", (int)response.StatusCode, body);
                throw new InvalidOperationException(
                    $"Absa authenticate failed with status {(int)response.StatusCode}: {body}");
            }

            var auth = JsonSerializer.Deserialize<AuthenticateResponse>(body, AbsaJsonOptions.Default)
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
            _lock.Release();
        }
    }

}
