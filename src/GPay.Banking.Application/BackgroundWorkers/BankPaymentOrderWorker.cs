using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using GPay.Banking.Services.Absa;
using GPay.Banking.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace GPay.Banking.BackgroundWorkers;

/// <summary>Polls GPay PRS orders and routes each enabled bank to its payment adapter.</summary>
public sealed class BankPaymentOrderWorker : AsyncPeriodicBackgroundWorkerBase
{
    public BankPaymentOrderWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory) : base(timer, serviceScopeFactory)
    {
        Timer.Period = 60_000;
    }

    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        var services = workerContext.ServiceProvider;

        var options = services.GetRequiredService<IOptions<BankPaymentIntakeOptions>>().Value;

        var absaOptions = services.GetRequiredService<IOptions<AbsaCapiOptions>>().Value;

        Timer.Period = Math.Max(30, options.PollPeriodSeconds) * 1000;

        var enabledBanks = options.EnabledBanks;

        if (enabledBanks.Count == 0)
        {
            return;
        }

        var logger = services.GetRequiredService<ILogger<BankPaymentOrderWorker>>();

        var gpay = services.GetRequiredService<IGpayOrderSync>();

        var initiator = services.GetRequiredService<IInstantPaymentInitiator>();

        var uowManager = services.GetRequiredService<IUnitOfWorkManager>();

        IReadOnlyList<GpayPaymentOrderCandidate> candidates;

        using (var pollUow = uowManager.Begin(requiresNew: true, isTransactional: false))
        {
            candidates = await gpay.GetEligiblePaymentOrdersAsync(Math.Max(1, options.BatchSize),enabledBanks);

            await pollUow.CompleteAsync();
        }

        if (candidates.Count == 0)
        {
            logger.LogDebug("No eligible PRS payment orders found for enabled banks.");

            return;
        }

        logger.LogInformation("Found {OrderCount} eligible PRS payment order(s).", candidates.Count);

        foreach (var order in candidates)
        {
            try
            {
                var bank = BankCodeParser.Parse(order.Bank);

                var bankOptions = options.ForBank(bank);

                var configuredSourceAccount = bankOptions.SourceAccountNumber;

                var configuredSourceShortName = bankOptions.SourceShortName;

                if (bank == BankCode.Absa)
                {
                    configuredSourceAccount = FirstConfigured(configuredSourceAccount, absaOptions.DefaultSourceAccountNumber);

                    configuredSourceShortName = FirstConfigured(configuredSourceShortName, absaOptions.DefaultSourceShortName);
                }

                var fromAccountNumber = FirstConfigured(configuredSourceAccount, order.FromAccountNumber)!;

                using var uow = uowManager.Begin(requiresNew: true, isTransactional: true);

                var currentOrderStatus = await gpay.GetOrderStatusAsync(order.OrderId);

                if (!string.Equals(currentOrderStatus, "PRS", StringComparison.OrdinalIgnoreCase))
                {
                    logger.LogInformation(
                        "Skipping GPay order {OrderNumber}; its current status is {OrderStatus}, not PRS.",
                        order.OrderNumber,
                        currentOrderStatus ?? "<missing>");

                    continue;
                }

                var paymentRequest = new InitiatePaymentRequestDto
                {
                    Bank = order.Bank,
                    FromAccountNumber = fromAccountNumber,
                    FromShortName = configuredSourceShortName,
                    ToAccountNumber = order.ToAccountNumber,
                    ToBranchCode = order.ToBranchCode,
                    Amount = order.Amount,
                    Currency = "ZAR",
                    Reference = order.Reference,
                    BeneficiaryName = order.BeneficiaryName,
                    PaymentRail = "RPP"
                };

                var result = await initiator.InitiateAsync(paymentRequest, order.OrderId);

                await uow.CompleteAsync();

                var _params = new[]
                { 
                    order.Bank, 
                    order.OrderNumber,
                    result.Status,
                    result.Success.ToString(),
                    result.CorrelationId
                };

                logger.LogInformation("{Bank} payment submission for GPay order {OrderNumber} returned {Status} (success={Success}, correlation={CorrelationId}).", _params);
            }
            catch (Exception ex)
            {
                var _params = new[]
                {
                    order.OrderNumber,
                    order.Bank,
                    order.Reference
                };

                logger.LogError(ex, "Failed to submit payment for GPay order {OrderNumber} at bank {Bank} (reference {Reference}).", _params);
                logger.LogInformation(
                    "GPay order {OrderNumber} will be rechecked on the next PRS poll based on its GPay order status.",
                    order.OrderNumber);
            }
        }
    }

    private static string? FirstConfigured(params string?[] values) => values.FirstOrDefault(value => !string.IsNullOrWhiteSpace(value))?.Trim();
}
