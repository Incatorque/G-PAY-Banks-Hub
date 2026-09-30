namespace GPay.Banking.Domain;

/// <summary>
/// Writes an Absa payment outcome onto the GPay order the same way host-to-host does.
/// </summary>
public interface IGpayOrderSync
{
    /// <summary>
    /// Updates <c>Order</c> and inserts <c>OrderHistory</c>.
    /// On a completed payment, inserts <c>EntityBankStatement</c> and moves the order to Reconciled.
    /// <c>OrderItem.FkOrderStatusID</c> points at <c>OrderItemStatus</c> (New / Planned), so item rows are left as they are.
    /// </summary>
    Task ApplyPaymentAsync(GpayPaymentSyncRequest request, CancellationToken cancellationToken = default);
}

/// <summary>
/// Fields used to find the GPay order and describe the bank outcome.
/// </summary>
public sealed class GpayPaymentSyncRequest
{
    public string? Reference { get; init; }

    public string? TransactionReference { get; init; }

    public string? SourceStatementRef { get; init; }

    public string? TargetStatementRef { get; init; }

    /// <summary>Hub label: Submitted, Pending, Completed, Failed, Duplicate, Error.</summary>
    public string? HubStatus { get; init; }

    public string? Note { get; init; }

    public decimal Amount { get; init; }
}
