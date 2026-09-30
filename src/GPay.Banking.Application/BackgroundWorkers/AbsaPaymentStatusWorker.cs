using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Services.Absa;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace GPay.Banking.BackgroundWorkers;

/// <summary>
/// Re-queries Absa payment status for open payments.
/// </summary>
/// <remarks>
/// Same host pattern as <c>EndOfDaySweepReportWorker</c> in G-PAY-ABP:
/// <see cref="AsyncPeriodicBackgroundWorkerBase"/> registered from the application module.
/// G-PAY_BanksNew host-to-host reads orders in PRS, then <c>ChangeStatus</c> writes
/// <c>Order.FkOrderStatusId</c> and <c>OrderHistory</c>. A posted debit inserts
/// <c>EntityBankStatement</c> and moves the order to Reconciled.
/// This worker does that through <see cref="IGpayOrderSync"/> when <c>ConnectionStrings:GPay</c> is set.
/// </remarks>
public class AbsaPaymentStatusWorker : AsyncPeriodicBackgroundWorkerBase
{
    public AbsaPaymentStatusWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory)
        : base(timer, serviceScopeFactory)
    {
        Timer.Period = 60_000;
    }

    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        var options = workerContext.ServiceProvider.GetRequiredService<IOptions<AbsaStatusPollOptions>>().Value;
        var periodSeconds = Math.Max(15, options.PeriodSeconds);
        Timer.Period = periodSeconds * 1000;

        if (!options.Enabled)
        {
            return;
        }

        var logger = workerContext.ServiceProvider.GetRequiredService<ILogger<AbsaPaymentStatusWorker>>();
        var uowManager = workerContext.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

        using var uow = uowManager.Begin(requiresNew: true, isTransactional: true);
        var repository = workerContext.ServiceProvider.GetRequiredService<IRepository<BankHubPaymentRecord, Guid>>();
        var resolver = workerContext.ServiceProvider.GetRequiredService<IBankCapabilityResolver>();
        var cutoff = DateTime.UtcNow.AddSeconds(-periodSeconds);
        var batchSize = Math.Max(1, options.BatchSize);

        var open = await repository.GetListAsync(x =>
            x.Status == "Submitted" || x.Status == "Pending" || x.Status == "Queued" || x.Status == "Saved");
        var due = open
            .Where(x => x.LastStatusCheckTime is null || x.LastStatusCheckTime < cutoff)
            .Where(x => !string.IsNullOrWhiteSpace(x.TransactionReference) || !string.IsNullOrWhiteSpace(x.ApiReference))
            .OrderBy(x => x.LastStatusCheckTime ?? DateTime.MinValue)
            .Take(batchSize)
            .ToList();

        if (due.Count == 0)
        {
            await uow.CompleteAsync();
            return;
        }

        logger.LogInformation("Absa status poll checking {Count} payment(s)", due.Count);
        var payments = resolver.GetInstantPayment(BankCode.Absa);
        var gpay = workerContext.ServiceProvider.GetRequiredService<IGpayOrderSync>();

        foreach (var record in due)
        {
            record.LastStatusCheckTime = DateTime.UtcNow;
            try
            {
                var correlationId = Guid.NewGuid().ToString("N");
                var result = await payments.GetStatusAsync(
                    new PaymentStatusRequest
                    {
                        TransactionReference = record.TransactionReference,
                        ApiReference = record.ApiReference
                    },
                    correlationId);

                if (!result.Success || result.Data is null)
                {
                    record.ErrorMessage = result.Error?.Message ?? record.ErrorMessage;
                    await repository.UpdateAsync(record, autoSave: true);
                    continue;
                }

                var data = result.Data;
                record.Status = data.RawStatusLabel ?? data.Status;
                record.BankStatusCode = data.BankStatusCode?.ToString();
                record.TransactionReference = data.TransactionReference ?? record.TransactionReference;
                record.ApiReference = data.ApiReference ?? record.ApiReference;
                if (data.Amount is > 0)
                {
                    record.Amount = data.Amount.Value;
                }

                record.ResultDescription = data.ResultDescription;
                record.ErrorMessage = data.ErrorCode is null ? null : data.ResultDescription;
                record.ResponseJson = System.Text.Json.JsonSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Absa status poll failed for payment {PaymentId}", record.Id);
                record.ErrorMessage = ex.Message;
            }

            await repository.UpdateAsync(record, autoSave: true);
            await gpay.ApplyPaymentAsync(new GpayPaymentSyncRequest
            {
                Reference = record.Reference,
                TransactionReference = record.TransactionReference,
                HubStatus = record.Status,
                Note = record.ErrorMessage ?? record.ResultDescription,
                Amount = record.Amount
            });
        }

        await uow.CompleteAsync();
    }
}
