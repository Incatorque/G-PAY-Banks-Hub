using GPay.Banking.Domain;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace GPay.Banking.Helpers;

/// <summary>Delivers the payment initiation email outside the payment submission request.</summary>
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
