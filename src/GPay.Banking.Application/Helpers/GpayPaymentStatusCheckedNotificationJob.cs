using GPay.Banking.Domain;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace GPay.Banking.Helpers;

/// <summary>Delivers a completed payment status report outside the polling worker.</summary>
public sealed class GpayPaymentStatusCheckedNotificationJob
    : AsyncBackgroundJob<GpayPaymentInitiatedNotification>, ITransientDependency
{
    private readonly IGpayPaymentNotifier _notifier;

    public GpayPaymentStatusCheckedNotificationJob(IGpayPaymentNotifier notifier)
    {
        _notifier = notifier;
    }

    public override Task ExecuteAsync(GpayPaymentInitiatedNotification args) =>
        _notifier.NotifyPaymentStatusCheckedAsync(args);
}
