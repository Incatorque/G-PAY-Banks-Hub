namespace GPay.Banking.Domain;

/// <summary>GPay order details needed to query the order's funding bank for payment status.</summary>
public sealed class GpayPaymentStatusCandidate
{
    public string Bank { get; init; } = string.Empty;

    public Guid OrderId { get; init; }

    public string OrderNumber { get; init; } = string.Empty;

    public string Reference { get; init; } = string.Empty;

    public decimal Amount { get; init; }

    public string Currency { get; init; } = string.Empty;

    public string FromAccountNumber { get; init; } = string.Empty;

    public string ToAccountNumber { get; init; } = string.Empty;
}
