namespace GPay.Banking.Domain.Dtos;

/// <summary>
/// Unregister a previously registered bank payment-status callback URL.
/// </summary>
public sealed class PaymentCallbackUnregisterRequest
{
    /// <summary>Callback URI previously registered with the bank.</summary>
    public required string Uri { get; init; }
}
