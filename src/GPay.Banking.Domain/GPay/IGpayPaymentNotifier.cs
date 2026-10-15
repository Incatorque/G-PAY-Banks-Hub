namespace GPay.Banking.Domain;

/// <summary>Delivers lifecycle emails after a Bank Hub payment changes a GPay order.</summary>
public interface IGpayPaymentNotifier
{
    Task NotifyFailureAsync(int orderNumber, string? reference, string? reason, CancellationToken cancellationToken = default);
    Task NotifyOrderCompletedAsync(int orderNumber, string? reference, string? status, string? note, CancellationToken cancellationToken = default);
    Task NotifyOrderReconciledAsync(int orderNumber, string? reference, CancellationToken cancellationToken = default);
}
