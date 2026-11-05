using System.Text;
using System.Text.Json;

namespace GPay.Banking.Services.Absa.Api.Clients;

/// <summary>
/// Username/password session provider with in-memory cache (~25 minutes).
/// </summary>
internal sealed class AbsaSessionProvider(
    HttpClient httpClient,
    IAbsaRequestSigner requestSigner,
    IOptions<AbsaCapiOptions> options,
    ILogger<AbsaSessionProvider> logger) : IAbsaSessionProvider
{
    private readonly AbsaCapiOptions _options = options.Value;
    private readonly SemaphoreSlim _lock = new(1, 1);

    private string? _session;
    private DateTimeOffset _expiresAt = DateTimeOffset.MinValue;

    private string? ValidSession =>
        string.IsNullOrWhiteSpace(_session) || DateTimeOffset.UtcNow >= _expiresAt ? null : _session;

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
                    "AbsaCapi Username/Password are not configured.");
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

            requestSigner.ApplyHeaders(request, payload);

            logger.LogInformation("Absa CAPI authenticate {Uri}", uri);

            using var response = await httpClient.SendAsync(request, cancellationToken);

            var body = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError("Absa authenticate failed: {Status} {Body}", (int)response.StatusCode, body);
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

            logger.LogInformation(
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
