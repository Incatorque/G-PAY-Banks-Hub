using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace GPay.Banking.BackgroundWorkers;

/// <summary>Polls Bank Hub-ready G-PAY orders and routes each enabled bank to its payment adapter.</summary>
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
            candidates = await gpay.GetEligiblePaymentOrdersAsync(Math.Max(1, options.BatchSize), enabledBanks);

            await pollUow.CompleteAsync();
        }

        if (candidates.Count == 0)
        {
            logger.LogDebug("No eligible Bank Hub-ready payment orders found for enabled banks.");

            return;
        }

        logger.LogInformation("Found {OrderCount} eligible Bank Hub-ready payment order(s).", candidates.Count);

        foreach (var order in candidates)
        {
            try
            {
                using var uow = uowManager.Begin(requiresNew: true, isTransactional: true);

                var currentOrderStatus = await gpay.GetOrderStatusAsync(order.OrderId);

                if (!IsReadyForPayment(currentOrderStatus))
                {
                    logger.LogInformation(
                        "Skipping GPay order {OrderNumber}; its current status is {OrderStatus}, not Bank Hub-ready.",
                        order.OrderNumber,
                        currentOrderStatus ?? "<missing>");

                    continue;
                }

                var paymentRail = string.Equals(order.Bank, BankCode.Absa.ToString(), StringComparison.OrdinalIgnoreCase)
                    ? ResolveAbsaPaymentRail(order)
                    : string.Empty;

                var paymentRequest = new InitiatePaymentRequestDto
                {
                    Bank = order.Bank,
                    FromAccountNumber = order.FromAccountNumber,
                    ToAccountNumber = order.ToAccountNumber,
                    ToBranchCode = order.ToBranchCode,
                    Amount = order.Amount,
                    Currency = order.Currency,
                    Reference = order.Reference,
                    BeneficiaryName = order.BeneficiaryName,
                    BeneficiaryBank = order.BeneficiaryBank,
                    PaymentRail = paymentRail,
                };

                var result = await initiator.InitiateAsync(paymentRequest, order.OrderId, order.OrderNumber);

                await uow.CompleteAsync();

                logger.LogInformation(
                    "{Bank} payment submission for GPay order {OrderNumber} returned {Status} (success={Success}, correlation={CorrelationId}).",
                    order.Bank,
                    order.OrderNumber,
                    result.Status,
                    result.Success,
                    result.CorrelationId);
            }
            catch (PaymentDeferredException ex)
            {
                logger.LogInformation(
                    "Deferring Absa PAAF payment for GPay order {OrderNumber}: {DeferralReason} The order remains queued for the next Bank Hub poll.",
                    order.OrderNumber,
                    ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Failed to submit payment for GPay order {OrderNumber} at bank {Bank} (reference {Reference}).",
                    order.OrderNumber,
                    order.Bank,
                    order.Reference);
                logger.LogInformation(
                    "GPay order {OrderNumber} will be rechecked on the next Bank Hub-ready poll based on its GPay order status.",
                    order.OrderNumber);
            }
        }
    }

    private static bool IsReadyForPayment(string? status) =>
        string.Equals(status, "BankHub - PRS", StringComparison.OrdinalIgnoreCase);

    private static string ResolveAbsaPaymentRail(GpayPaymentOrderCandidate order)
    {
        if (order.Amount <= 50000m)
        {
            return "RPP";
        }

        if (order.Amount > 250000m)
        {
            return "PAAF";
        }

        return string.Equals(order.ServiceType, "02", StringComparison.Ordinal)
            ? "IIP"
            : "PAAF";
    }

}
