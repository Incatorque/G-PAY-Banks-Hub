using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Dtos.InstantPayment;

namespace GPay.Banking.Contracts.Interfaces;

/// <summary>
/// Instant payment (PayShap) capability implemented by each bank adapter.
/// </summary>
public interface IInstantPaymentService
{
    /// <summary>
    /// Submits an instant payment instruction to the bank.
    /// </summary>
    Task<ApiResult<InstantPaymentResponse>> PayAsync(
        InstantPaymentRequest request,
        string correlationId,
        CancellationToken cancellationToken = default);
}
