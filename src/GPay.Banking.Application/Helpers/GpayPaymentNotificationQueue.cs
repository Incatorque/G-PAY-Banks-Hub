using GPay.Banking.Domain;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Uow;

namespace GPay.Banking.Helpers;

internal sealed class GpayPaymentNotificationQueue : IGpayPaymentNotificationQueue
{
    private readonly IBackgroundJobManager _backgroundJobs;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly ILogger<GpayPaymentNotificationQueue> _logger;

    public GpayPaymentNotificationQueue(
        IBackgroundJobManager backgroundJobs,
        IUnitOfWorkManager unitOfWorkManager,
        ILogger<GpayPaymentNotificationQueue> logger)
    {
        _backgroundJobs = backgroundJobs;
        _unitOfWorkManager = unitOfWorkManager;
        _logger = logger;
    }

    public async Task EnqueueAsync(GpayPaymentInitiatedNotification notification)
    {
        try
        {
            using var unitOfWork = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: false);
            await _backgroundJobs.EnqueueAsync(notification);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(
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
