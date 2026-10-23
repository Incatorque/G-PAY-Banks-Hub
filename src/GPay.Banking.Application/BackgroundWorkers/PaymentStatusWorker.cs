using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.BackgroundJobs;
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

        var backgroundJobs = services.GetRequiredService<IBackgroundJobManager>();

        var uowManager = services.GetRequiredService<IUnitOfWorkManager>();

        using var uow = uowManager.Begin(requiresNew: true, isTransactional: true);

        var orders = await gpay.GetOrdersForStatusCheckAsync();

        foreach (var order in orders)
        {
            try
            {
                var bank = GPay.Banking.Helpers.BankCodeParser.Parse(order.Bank);

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
                    logger.LogWarning(
                        "Status check at {Bank} failed for GPay order {OrderNumber}: {Message}",
                        order.Bank,
                        order.OrderNumber,
                        result.Error?.Message ?? "No status data returned.");
                    continue;
                }

                var status = result.Data;
                var previousOrderStatus = await gpay.GetPreviousOrderStatusAsync(order.OrderId);

                await gpay.ApplyPaymentAsync(new GpayPaymentSyncRequest
                {
                    GpayOrderId = order.OrderId,
                    Reference = status.Reference ?? order.Reference,
                    TransactionReference = status.TransactionReference,
                    SourceStatementRef = status.SourceStatementRef,
                    TargetStatementRef = status.TargetStatementRef,
                    HubStatus = status.RawStatusLabel ?? status.Status,
                    Note = status.ResultDescription,
                    Amount = status.Amount ?? 0m
                });

                var normalizedStatus = status.RawStatusLabel ?? status.Status;
                var updatedOrderStatus = await gpay.GetOrderStatusAsync(order.OrderId);
                var orderStatusChanged = !string.Equals(previousOrderStatus, updatedOrderStatus, StringComparison.OrdinalIgnoreCase);
                var isSuccess = IsSuccessfulStatus(normalizedStatus);
                var isFailure = IsFailureStatus(normalizedStatus);
                if (orderStatusChanged && (isSuccess || isFailure))
                {
                    try
                    {
                        await backgroundJobs.EnqueueAsync(new GpayPaymentInitiatedNotification
                        {
                            Bank = order.Bank,
                            OrderNumber = order.OrderNumber,
                            Amount = status.Amount ?? order.Amount,
                            Currency = status.Currency ?? order.Currency,
                            FromAccountNumber = order.FromAccountNumber,
                            ToAccountNumber = order.ToAccountNumber,
                            Reference = status.Reference ?? order.Reference,
                            Status = normalizedStatus,
                            IsStatusCheck = true,
                            IsError = IsFailureStatus(normalizedStatus),
                            ErrorCode = status.ErrorCode ?? normalizedStatus,
                            ErrorMessage = status.ResultDescription,
                            StatusDescription = status.ResultDescription ?? "Payment successfully processed and settled.",
                            TransactionReference = status.TransactionReference ?? status.TransactionId
                        });
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(
                            ex,
                            "Failed to queue completed payment status email for GPay order {OrderNumber}.",
                            order.OrderNumber);
                    }
                }

                logger.LogInformation(
                    "Updated GPay order {OrderNumber} from {Bank} status {Status}.",
                    order.Bank,
                    order.OrderNumber,
                    status.RawStatusLabel ?? status.Status);
            }
            catch (NotSupportedException ex)
            {
                logger.LogWarning(
                    ex,
                    "Skipping GPay order {OrderNumber}; no payment status service is registered for bank {Bank}.",
                    order.OrderNumber,
                    order.Bank);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Status check at {Bank} failed for GPay order {OrderNumber}.",
                    order.Bank,
                    order.OrderNumber);
            }
        }

        await uow.CompleteAsync();
    }

    private static bool IsSuccessfulStatus(string? status) =>
        string.Equals(status, "Completed", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(status, "Accepted", StringComparison.OrdinalIgnoreCase);

    private static bool IsFailureStatus(string? status)
    {
        var normalizedStatus = status?.Trim();
        return normalizedStatus?.Contains("FAIL", StringComparison.OrdinalIgnoreCase) == true ||
               normalizedStatus?.Contains("ERROR", StringComparison.OrdinalIgnoreCase) == true ||
               normalizedStatus?.Contains("DUPLICATE", StringComparison.OrdinalIgnoreCase) == true ||
               normalizedStatus?.Contains("REJECT", StringComparison.OrdinalIgnoreCase) == true ||
               normalizedStatus?.Contains("REVERS", StringComparison.OrdinalIgnoreCase) == true ||
               normalizedStatus?.Contains("UNSUCCESSFUL", StringComparison.OrdinalIgnoreCase) == true ||
               normalizedStatus?.Contains("CANCEL", StringComparison.OrdinalIgnoreCase) == true;
    }
}
