namespace GPay.Banking.Domain;

/// <summary>Delivers lifecycle emails after a Bank Hub payment changes a GPay order.</summary>
public interface IGpayPaymentNotifier
{
    Task NotifyPaymentInitiatedAsync(
        GpayPaymentInitiatedNotification notification,
        CancellationToken cancellationToken = default);
}
