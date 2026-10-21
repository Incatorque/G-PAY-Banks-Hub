namespace GPay.Banking.Domain;

/// <summary>
/// Reads eligible GPay orders and writes normalized bank outcomes back to GPay.
/// </summary>
public interface IGpayOrderSync
{
    Task<IReadOnlyList<GpayPaymentOrderCandidate>> GetEligiblePaymentOrdersAsync( int batchSize, IReadOnlyCollection<BankCode> enabledBanks, CancellationToken cancellationToken = default);

    /// <summary>Reads the current GPay order status immediately before bank submission.</summary>
    Task<string?> GetOrderStatusAsync(Guid orderId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates <c>Order.FkOrderStatusID</c> and inserts an <c>OrderHistory</c> audit row.
    /// Order item rows are left unchanged because their status foreign key targets <c>OrderItemStatus</c>.
    /// </summary>
    Task ApplyPaymentAsync(GpayPaymentSyncRequest request, CancellationToken cancellationToken = default);
}

/// <summary>GPay PRS order details needed to submit a bank payment.</summary>
public sealed class GpayPaymentOrderCandidate
{
    public string Bank { get; init; } = string.Empty;

    public Guid OrderId { get; init; }
    public string OrderNumber { get; init; } = string.Empty;
    public string Reference { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public string FromAccountNumber { get; init; } = string.Empty;
    public string ToAccountNumber { get; init; } = string.Empty;
    public string ToBranchCode { get; init; } = string.Empty;
    public string? BeneficiaryName { get; init; }
}

/// <summary>
/// Fields used to find the GPay order and describe the bank outcome.
/// </summary>
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
