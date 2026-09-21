namespace GPay.Banking.Services.Absa.Api.Clients;

/// <summary>
/// Builds Absa CAPI request security headers (MIG §3): X-Date, X-Client-API-Key, X-Signature.
/// </summary>
public interface IAbsaRequestSigner
{
    /// <summary>
    /// Applies required Absa headers to an outbound request.
    /// </summary>
    void ApplyHeaders(HttpRequestMessage request, string? jsonBody);
}
