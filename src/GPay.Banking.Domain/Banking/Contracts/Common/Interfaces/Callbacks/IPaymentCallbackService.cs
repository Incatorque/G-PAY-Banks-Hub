using GPay.Banking;

namespace GPay.Banking.Domain;

/// <summary>
/// Bank-specific inbound payment callback handler (token, payload shape, mapping).
/// </summary>
public interface IPaymentCallbackService
{
    BankCode Bank { get; }

    Task<PaymentCallbackResult> HandlePaymentAsync(
        PaymentCallbackInbound inbound,
        CancellationToken cancellationToken = default);
}
