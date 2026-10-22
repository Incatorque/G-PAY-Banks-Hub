using GPay.Banking.Domain.Dtos;

namespace GPay.Banking.Domain;

/// <summary>
/// Routes normalized payment requests to the adapter for the selected bank.
/// </summary>
public interface IBankRouterService
{
    Task<ApiResult<InstantPaymentResponse>> SubmitPaymentAsync(
        BankCode bank,
        InstantPaymentRequest request,
        string correlationId,
        CancellationToken cancellationToken = default);

    Task<ApiResult<PaymentStatusResponse>> GetPaymentStatusAsync(
        BankCode bank,
        PaymentStatusRequest request,
        string correlationId,
        CancellationToken cancellationToken = default);
}
