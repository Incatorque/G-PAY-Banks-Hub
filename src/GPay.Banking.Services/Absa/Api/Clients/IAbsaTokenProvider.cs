namespace GPay.Banking.Services.Absa.Api.Clients;

/// <summary>
/// Backward-compatible alias used by older registrations/tests.
/// </summary>
public interface IAbsaTokenProvider : IAbsaSessionProvider
{
    /// <summary>
    /// Alias for <see cref="IAbsaSessionProvider.GetSessionAsync"/>.
    /// </summary>
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default) =>
        GetSessionAsync(cancellationToken);
}
