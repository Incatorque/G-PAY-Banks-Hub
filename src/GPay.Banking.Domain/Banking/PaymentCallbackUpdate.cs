namespace GPay.Banking.Domain;

/// <summary>
/// Bank-agnostic payment fields extracted from a callback for persistence.
/// </summary>
public sealed class PaymentCallbackUpdate
{
    public string? TransactionReference { get; init; }

    public string? ApiReference { get; init; }

    public string? Status { get; init; }

    public string? PaymentRail { get; init; }

    public decimal? Amount { get; init; }

    public string? Currency { get; init; }

    public string? ErrorMessage { get; init; }

    public string? ResponseJson { get; init; }
}
