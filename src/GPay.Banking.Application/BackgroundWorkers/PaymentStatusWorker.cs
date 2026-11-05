using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace GPay.Banking.BackgroundWorkers;

/// <summary>Routes today's open GPay orders to their bank status services and syncs results.</summary>
public sealed class PaymentStatusWorker : AsyncPeriodicBackgroundWorkerBase
{
    public PaymentStatusWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory) : base(timer, serviceScopeFactory)
    {
        Timer.Period = 60_000;
    }

    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        var services = workerContext.ServiceProvider;

        var options = services.GetRequiredService<IOptions<PaymentStatusPollOptions>>().Value;

        Timer.Period = Math.Max(15, options.PeriodSeconds) * 1000;

        if (!options.Enabled)
        {
            return;
        }

        var logger = services.GetRequiredService<ILogger<PaymentStatusWorker>>();

        var gpay = services.GetRequiredService<IGpayOrderSync>();

        var bankRouter = services.GetRequiredService<IBankRouterService>();

        var notifications = services.GetRequiredService<IGpayPaymentNotificationQueue>();

        var uowManager = services.GetRequiredService<IUnitOfWorkManager>();

        using var uow = uowManager.Begin(requiresNew: true, isTransactional: true);

        var orders = await gpay.GetOrdersForStatusCheckAsync(options.BatchSize);

        foreach (var order in orders)
        {
            try
            {
                var bank = BankCodeParser.Parse(order.Bank);

                if (bank != BankCode.Absa) //Only ABSA for now.
                {
                    continue;
                }

                var result = await bankRouter.GetPaymentStatusAsync(
                    bank,
                    new PaymentStatusRequest
                    {
                        GpayOrderId = order.OrderId,
                        TransactionReference = order.Reference
                    },
                    Guid.NewGuid().ToString("N"));

                if (!result.Success || result.Data is null)
                {
                    await notifications.EnqueueAsync(PaymentNotificationFactory.StatusCheckError(
                        order,
                        result.Error?.Code ?? "PAYMENT_STATUS_ERROR",
                        result.Error?.Message ?? "No status data returned."));
                    logger.LogWarning(
                        "Status check at {Bank} failed for GPay order {OrderNumber}: {Message}",
                        order.Bank,
                        order.OrderNumber,
                        result.Error?.Message ?? "No status data returned.");
                    continue;
                }

                var status = result.Data;
                var normalizedStatus = PaymentStatusClassifier.Normalize(status);
                var previousOrderStatus = await gpay.GetOrderStatusAsync(order.OrderId);

                await gpay.ApplyPaymentAsync(new GpayPaymentSyncRequest
                {
                    GpayOrderId = order.OrderId,
                    Reference = status.Reference ?? order.Reference,
                    TransactionReference = status.TransactionReference,
                    SourceStatementRef = status.SourceStatementRef,
                    TargetStatementRef = status.TargetStatementRef,
                    HubStatus = normalizedStatus,
                    Note = status.ResultDescription,
                    Amount = status.Amount ?? 0m
                });

                var updatedOrderStatus = await gpay.GetOrderStatusAsync(order.OrderId);
                var orderStatusChanged = !string.Equals(previousOrderStatus, updatedOrderStatus, StringComparison.OrdinalIgnoreCase);
                var isSuccess = PaymentStatusClassifier.IsSuccess(normalizedStatus);
                var isFailure = PaymentStatusClassifier.IsFailure(normalizedStatus);
                if (isFailure || (orderStatusChanged && isSuccess))
                {
                    await notifications.EnqueueAsync(PaymentNotificationFactory.StatusCheckResult(
                        order.Bank,
                        order.OrderNumber,
                        status.Amount ?? order.Amount,
                        status.Currency ?? order.Currency,
                        order.FromAccountNumber,
                        order.ToAccountNumber,
                        status.Reference ?? order.Reference,
                        normalizedStatus,
                        isFailure,
                        status.ErrorCode ?? normalizedStatus,
                        status.ResultDescription,
                        status.ResultDescription ?? (isFailure
                            ? "The bank reported that payment settlement failed."
                            : "Payment successfully processed and settled."),
                        status.TransactionReference ?? status.TransactionId));
                }

                logger.LogInformation(
                    "Updated GPay order {OrderNumber} from {Bank} status {Status}.",
                    order.Bank,
                    order.OrderNumber,
                    normalizedStatus);
            }
            catch (NotSupportedException ex)
            {
                await notifications.EnqueueAsync(PaymentNotificationFactory.StatusCheckError(
                    order, "PAYMENT_STATUS_UNSUPPORTED", ex.Message));
                logger.LogWarning(
                    ex,
                    "Skipping GPay order {OrderNumber}; no payment status service is registered for bank {Bank}.",
                    order.OrderNumber,
                    order.Bank);
            }
            catch (Exception ex)
            {
                await notifications.EnqueueAsync(PaymentNotificationFactory.StatusCheckError(
                    order, "PAYMENT_STATUS_EXCEPTION", ex.Message));
                logger.LogError(
                    ex,
                    "Status check at {Bank} failed for GPay order {OrderNumber}.",
                    order.Bank,
                    order.OrderNumber);
            }
        }

        await uow.CompleteAsync();
    }

}
