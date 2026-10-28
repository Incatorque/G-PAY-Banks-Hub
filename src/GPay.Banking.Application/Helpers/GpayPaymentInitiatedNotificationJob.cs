using GPay.Banking.Domain;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace GPay.Banking.Helpers;

/// <summary>Delivers a payment lifecycle email outside the payment request.</summary>
public sealed class GpayPaymentInitiatedNotificationJob
    : AsyncBackgroundJob<GpayPaymentInitiatedNotification>, ITransientDependency
{
    private readonly IGpayPaymentNotifier _notifier;

    public GpayPaymentInitiatedNotificationJob(IGpayPaymentNotifier notifier)
    {
        _notifier = notifier;
    }

    public override Task ExecuteAsync(GpayPaymentInitiatedNotification args) =>
        _notifier.NotifyPaymentInitiatedAsync(args);
}
