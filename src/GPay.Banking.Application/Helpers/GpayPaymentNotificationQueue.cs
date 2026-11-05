using GPay.Banking.Domain;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Uow;

namespace GPay.Banking.Helpers;

internal sealed class GpayPaymentNotificationQueue(
    IBackgroundJobManager backgroundJobs,
    IUnitOfWorkManager unitOfWorkManager,
    ILogger<GpayPaymentNotificationQueue> logger) : IGpayPaymentNotificationQueue
{
    public async Task EnqueueAsync(GpayPaymentInitiatedNotification notification)
    {
        try
        {
            using var unitOfWork = unitOfWorkManager.Begin(requiresNew: true, isTransactional: false);
            await backgroundJobs.EnqueueAsync(notification);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Failed to queue payment notification for {Bank} reference {Reference}.",
                notification.Bank,
                notification.Reference);
        }
    }
}

public interface IGpayPaymentNotificationQueue
{
    Task EnqueueAsync(GpayPaymentInitiatedNotification notification);
}
