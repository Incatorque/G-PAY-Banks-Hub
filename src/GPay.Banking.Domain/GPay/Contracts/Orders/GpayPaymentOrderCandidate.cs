namespace GPay.Banking.Domain;

/// <summary>GPay Bank Hub-ready order details needed to submit a bank payment.</summary>
public sealed class GpayPaymentOrderCandidate
{
    public string Bank { get; init; } = string.Empty;

    public Guid OrderId { get; init; }

    public string OrderNumber { get; init; } = string.Empty;

    public string Reference { get; init; } = string.Empty;

    public string? ServiceType { get; init; }

    public decimal Amount { get; init; }

    public string Currency { get; init; } = string.Empty;

    public string FromAccountNumber { get; init; } = string.Empty;

    public string ToAccountNumber { get; init; } = string.Empty;

    public string ToBranchCode { get; init; } = string.Empty;

    public string BeneficiaryBank { get; init; } = string.Empty;

    public string? BeneficiaryName { get; init; }
}
