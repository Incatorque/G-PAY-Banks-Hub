namespace GPay.Banking.Services.Capitec.Api.Clients;

/// <summary>
/// Retrieves and caches OAuth 2.0 bearer tokens for Capitec CPS.
/// </summary>
public interface ICapitecAccessTokenProvider
{
    /// <summary>
    /// Returns a valid Capitec access token.
    /// </summary>
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default);
}

