using GPay.Banking.Domain;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace GPay.Banking.Helpers;

/// <summary>Delivers a payment lifecycle email outside the payment request.</summary>
public sealed class GpayPaymentInitiatedNotificationJob(IGpayPaymentNotifier notifier)
    : AsyncBackgroundJob<GpayPaymentInitiatedNotification>, ITransientDependency
{
    public override Task ExecuteAsync(GpayPaymentInitiatedNotification args) =>
        notifier.NotifyPaymentInitiatedAsync(args);
}
