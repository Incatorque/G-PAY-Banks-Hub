using GPay.Banking.Services.Absa.Api.Models.Avs;
using GPay.Banking.Services.Absa.Api.Models.Payment;

namespace GPay.Banking.Services.Absa.Api.Clients;

/// <summary>
/// Absa Corporate API client used by bank capability services.
/// </summary>
public interface IAbsaCapiClient
{
    /// <summary>
    /// Calls Absa Account Verification (ValidateBankDetails, and ValidateBankReference when pending).
    /// </summary>
    Task<AbsaAvsResponse> VerifyAccountAsync(AbsaAvsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Initiates an Absa instant payment (PayShap / RTC).
    /// </summary>
    Task<AbsaPaymentInitiateResponse> InitiatePaymentAsync(
        AbsaPaymentInitiateRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Queries Absa payment status by correlation references.
    /// </summary>
    Task<AbsaPaymentStatusResponse> GetPaymentStatusAsync(
        AbsaPaymentStatusRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers a payment callback URI with Absa.
    /// </summary>
    Task<AbsaPaymentCallbackRegisterResponse> RegisterPaymentCallbackAsync(
        AbsaPaymentCallbackRegisterRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Unregisters a payment callback URI with Absa.
    /// </summary>
    Task<AbsaPaymentCallbackRegisterResponse> UnregisterPaymentCallbackAsync(
        AbsaPaymentCallbackUnregisterRequest request,
        CancellationToken cancellationToken = default);

}
