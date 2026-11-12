using GPay.Banking.Services.Capitec;
using GPay.Banking.Domain;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Options;
using GPay.Banking.Services.Capitec.Api.Clients;

namespace GPay.Banking.Services.Capitec.Api.Clients;

/// <summary>
/// OAuth password-grant token provider with expiry-aware in-memory caching.
/// </summary>
public sealed class CapitecAccessTokenProvider : ICapitecAccessTokenProvider
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = null
    };

    private readonly HttpClient _httpClient;
    private readonly CapitecOptions _options;
    private readonly ILogger<CapitecAccessTokenProvider> _logger;
    private readonly IBankApiCallAuditor _auditor;
    private readonly SemaphoreSlim _lock = new(1, 1);

    private string? _accessToken;
    private DateTimeOffset _expiresAt = DateTimeOffset.MinValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="CapitecAccessTokenProvider"/> class.
    /// </summary>
    public CapitecAccessTokenProvider(
        HttpClient httpClient,
        IOptions<CapitecOptions> options,
        ILogger<CapitecAccessTokenProvider> logger,
        IBankApiCallAuditor auditor)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
        _auditor = auditor;
    }

    /// <inheritdoc />
    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        var form = new Dictionary<string, string>
        {
            ["grant_type"] = "password",
            ["client_id"] = _options.ClientId,
            ["client_secret"] = _options.ClientSecret,
            ["scope"] = _options.Scope,
            ["resource"] = _options.Resource,
            ["username"] = _options.Username,
            ["password"] = _options.Password
        };

        if (!string.IsNullOrWhiteSpace(_accessToken) && DateTimeOffset.UtcNow < _expiresAt)
        {
            return _accessToken!;
        }

        await _lock.WaitAsync(cancellationToken);
        try
        {
            if (!string.IsNullOrWhiteSpace(_accessToken) && DateTimeOffset.UtcNow < _expiresAt)
            {
                return _accessToken!;
            }

            if (!IsLiveCredential(_options.ClientId) ||
                !IsLiveCredential(_options.ClientSecret) ||
                !IsLiveCredential(_options.Username) ||
                !IsLiveCredential(_options.Password))
            {
                throw new InvalidOperationException(
                    "Capitec ClientId, ClientSecret, Username, and Password are not configured. Set Capitec__* values " +
                    "via appsettings.Local.json, user-secrets, or GPAY_Capitec__* environment variables.");
            }

            using var request = new HttpRequestMessage(HttpMethod.Post, _options.TokenUrl)
            {
                Content = new FormUrlEncodedContent(form)
            };

            var started = Stopwatch.StartNew();
            int? statusCode = null;
            string? body = null;
            var success = false;
            try
            {
                using var response = await _httpClient.SendAsync(request, cancellationToken);

                statusCode = (int)response.StatusCode;

                body = await response.Content.ReadAsStringAsync(cancellationToken);

                success = response.IsSuccessStatusCode;

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(
                        $"Capitec token endpoint returned status code {statusCode}. Response: {body}");
                }
                var auth = JsonSerializer.Deserialize<CapitecTokenResponse>(body, JsonOptions)
                    ?? throw new InvalidOperationException("Unable to deserialize Capitec authenticate response.");

                if (string.IsNullOrWhiteSpace(auth.AccessToken))
                {
                    throw new InvalidOperationException(
                        "Token endpoint returned no access token.");
                }
                if (auth.ExpiresIn <= 60)
                {
                    throw new InvalidOperationException("Capitec identity provider returned a token with an invalid or too-short expiry.");
                }

                _accessToken = auth.AccessToken;
                _expiresAt = DateTimeOffset.UtcNow.AddSeconds(auth.ExpiresIn - 60);
                _logger.LogInformation("Capitec token acquired. ExpiresAt={ExpiresAt}", _expiresAt);
                return _accessToken;
            }
            finally
            {
                started.Stop();
                await CapitecCallAudit.WriteAsync(
                    _auditor,
                    _logger,
                    new BankApiCallAuditEntry
                    {
                        BankCode = BankCode.Capitec.ToString(),
                        Direction = "Outbound",
                        Operation = "Authenticate",
                        HttpMethod = HttpMethod.Post.Method,
                        HttpStatus = statusCode,
                        RequestJson = "OAuth Token Request",
                        ResponseJson = CapitecApiPayloadRedactor.Redact(body),
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

    private static bool IsLiveCredential(string? value) =>
        !string.IsNullOrWhiteSpace(value) &&
        !value.StartsWith("SET_", StringComparison.OrdinalIgnoreCase);
}

