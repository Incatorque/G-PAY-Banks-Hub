namespace GPay.Banking.Domain;

/// <summary>Fields used to find the GPay order and describe the bank outcome.</summary>
public sealed class GpayPaymentSyncRequest
{
    public Guid? GpayOrderId { get; init; }

    public string? Reference { get; init; }

    public string? TransactionReference { get; init; }

    public string? SourceStatementRef { get; init; }

    public string? TargetStatementRef { get; init; }

    /// <summary>Hub label: Submitted, Pending, Completed, Failed, Duplicate, Error.</summary>
    public string? HubStatus { get; init; }

    public string? Note { get; init; }

    public decimal Amount { get; init; }
}
