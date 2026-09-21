namespace GPay.Banking.Services.Absa.Api.Clients;

/// <summary>
/// Provides Absa CAPI session tokens via <c>/api/User/Authenticate</c> (MIG §5).
/// </summary>
public interface IAbsaSessionProvider
{
    /// <summary>
    /// Returns a valid Absa session string.
    /// </summary>
    Task<string> GetSessionAsync(CancellationToken cancellationToken = default);
}
