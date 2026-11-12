using GPay.Banking.Services.Capitec.Api.Models.Payment;

namespace GPay.Banking.Services.Capitec.Api.Clients;

/// <summary>
/// Capitec API client used by bank capability services.
/// </summary>
public interface ICapitecClient
{
    /// <summary>
    /// Indicates whether live API credentials are present.
    /// </summary>
    bool IsConfigured { get; }

    /// <summary>
    /// Initiates one CPS credit transfer.
    /// </summary>
    Task<CapitecPaymentInitiateResponse> InitiatePaymentAsync(
        CapitecPaymentInitiateRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Queries a CPS payment by the original message ID and instruction UETR.
    /// </summary>
    Task<CapitecPaymentStatusResponse> GetPaymentStatusAsync(
        CapitecPaymentStatusRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Sends a raw JSON request to Capitec CAPI.</summary>
    Task<string> SendAsync(string relativePath, HttpMethod method, string? jsonBody, CancellationToken cancellationToken = default, Guid? gpayOrderId = null);

}
