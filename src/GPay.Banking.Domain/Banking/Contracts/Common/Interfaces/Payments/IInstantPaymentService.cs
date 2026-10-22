using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;

using GPay.Banking;

namespace GPay.Banking.Domain;

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

    /// <summary>
    /// Queries payment status by transaction or API reference.
    /// </summary>
    Task<ApiResult<PaymentStatusResponse>> GetStatusAsync(
        PaymentStatusRequest request,
        string correlationId,
        CancellationToken cancellationToken = default);
}




