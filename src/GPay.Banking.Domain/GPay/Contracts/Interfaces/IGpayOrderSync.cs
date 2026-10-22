namespace GPay.Banking.Domain;

/// <summary>
/// Reads eligible GPay orders and writes normalized bank outcomes back to GPay.
/// </summary>
public interface IGpayOrderSync
{
    Task<IReadOnlyList<GpayPaymentOrderCandidate>> GetEligiblePaymentOrdersAsync(
        int batchSize,
        IReadOnlyCollection<BankCode> enabledBanks,
        CancellationToken cancellationToken = default);

    /// <summary>Gets today's non-terminal GPay orders funded from supported bank accounts.</summary>
    Task<IReadOnlyList<GpayPaymentStatusCandidate>> GetOrdersForStatusCheckAsync(
        CancellationToken cancellationToken = default);

    /// <summary>Reads the current GPay order status immediately before bank submission.</summary>
    Task<string?> GetOrderStatusAsync(Guid orderId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates <c>Order.FkOrderStatusID</c> and inserts an <c>OrderHistory</c> audit row.
    /// Order item rows are left unchanged because their status foreign key targets <c>OrderItemStatus</c>.
    /// </summary>
    Task ApplyPaymentAsync(GpayPaymentSyncRequest request, CancellationToken cancellationToken = default);
}
