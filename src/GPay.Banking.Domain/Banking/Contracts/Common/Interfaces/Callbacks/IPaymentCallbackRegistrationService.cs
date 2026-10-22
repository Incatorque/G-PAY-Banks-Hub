using GPay.Banking.Domain.Dtos;

namespace GPay.Banking.Domain;

/// <summary>
/// Bank channel-level payment callback URL registration (Absa Register / UnRegister).
/// </summary>
/// <remarks>
/// Absa Payment API v1.8 exposes Register and UnRegister only. Amend is a re-register with new details.
/// </remarks>
public interface IPaymentCallbackRegistrationService
{
    /// <summary>Registers the channel callback URI with the bank.</summary>
    Task<ApiResult<PaymentCallbackRegistrationResponse>> RegisterAsync(
        PaymentCallbackRegistrationRequest request,
        string correlationId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Amends the channel callback URI (Absa: re-register with updated Uri/Token/SupportEmail).
    /// </summary>
    Task<ApiResult<PaymentCallbackRegistrationResponse>> AmendAsync(
        PaymentCallbackRegistrationRequest request,
        string correlationId,
        CancellationToken cancellationToken = default);

    /// <summary>Unregisters the channel callback URI with the bank.</summary>
    Task<ApiResult<PaymentCallbackRegistrationResponse>> UnregisterAsync(
        PaymentCallbackUnregisterRequest request,
        string correlationId,
        CancellationToken cancellationToken = default);
}
