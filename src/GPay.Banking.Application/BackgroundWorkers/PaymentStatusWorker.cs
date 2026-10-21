using System.Text.Json;
using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace GPay.Banking.BackgroundWorkers;

/// <summary>Polls bank status using GPay order state and the existing bank API audit log.</summary>
public sealed class PaymentStatusWorker : AsyncPeriodicBackgroundWorkerBase
{
    public PaymentStatusWorker(
        AbpAsyncTimer timer,
        IServiceScopeFactory serviceScopeFactory)
        : base(timer, serviceScopeFactory)
    {
        Timer.Period = 60_000;
    }

    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        var services = workerContext.ServiceProvider;
        var options = services.GetRequiredService<IOptions<PaymentStatusPollOptions>>().Value;
        var periodSeconds = Math.Max(15, options.PeriodSeconds);
        Timer.Period = periodSeconds * 1000;

        if (!options.Enabled)
        {
            return;
        }

        var logger = services.GetRequiredService<ILogger<PaymentStatusWorker>>();
        var uowManager = services.GetRequiredService<IUnitOfWorkManager>();
        var apiCalls = services.GetRequiredService<IRepository<BankHubApiCall, Guid>>();
        var resolver = services.GetRequiredService<IBankCapabilityResolver>();
        var gpay = services.GetRequiredService<IGpayOrderSync>();
        var batchSize = Math.Max(1, options.BatchSize);
        var cutoff = DateTime.UtcNow.AddSeconds(-periodSeconds);

        using var uow = uowManager.Begin(requiresNew: true, isTransactional: true);

        var calls = await apiCalls.GetListAsync(call =>
            call.Direction == "Outbound" &&
            call.GpayOrderId.HasValue);

        var latestInitiations = calls
            .Where(call => string.Equals(call.Operation, "initiate", StringComparison.OrdinalIgnoreCase))
            .Select(call =>
            {
                var (transactionReference, apiReference) = ReadPaymentReferences(call.ResponseJson);
                return new
                {
                    Call = call,
                    TransactionReference = transactionReference,
                    ApiReference = apiReference
                };
            })
            .Where(payment =>
                !string.IsNullOrWhiteSpace(payment.TransactionReference) ||
                !string.IsNullOrWhiteSpace(payment.ApiReference))
            .GroupBy(payment => payment.Call.GpayOrderId!.Value)
            .Select(group => group.OrderByDescending(payment => payment.Call.CreationTime).First())
            .OrderBy(payment => payment.Call.CreationTime)
            .ToList();

        var latestStatusChecks = calls
            .Where(call => string.Equals(call.Operation, "Status", StringComparison.OrdinalIgnoreCase))
            .GroupBy(call => call.GpayOrderId!.Value)
            .ToDictionary(group => group.Key, group => group.Max(call => call.CreationTime));

        var due = new List<(BankHubApiCall Initiation, string? TransactionReference, string? ApiReference)>();
        foreach (var payment in latestInitiations)
        {
            var initiation = payment.Call;
            var orderId = initiation.GpayOrderId!.Value;
            var orderStatus = await gpay.GetOrderStatusAsync(orderId);
            if (!IsStatusPollable(orderStatus))
            {
                continue;
            }

            if (latestStatusChecks.TryGetValue(orderId, out var lastCheck) && lastCheck > cutoff)
            {
                continue;
            }

            due.Add((initiation, payment.TransactionReference, payment.ApiReference));
            if (due.Count >= batchSize)
            {
                break;
            }
        }

        if (due.Count == 0)
        {
            await uow.CompleteAsync();
            return;
        }

        logger.LogInformation("Payment status poll checking {Count} GPay order(s).", due.Count);

        foreach (var payment in due)
        {
            var initiation = payment.Initiation;
            var gpayOrderId = initiation.GpayOrderId!.Value;
            try
            {
                var bank = BankCodeParser.Parse(initiation.BankCode);
                var paymentService = resolver.GetInstantPayment(bank);
                var result = await paymentService.GetStatusAsync(
                    new PaymentStatusRequest
                    {
                        GpayOrderId = gpayOrderId,
                        TransactionReference = payment.TransactionReference,
                        ApiReference = payment.ApiReference
                    },
                    Guid.NewGuid().ToString("N"));

                if (!result.Success || result.Data is null)
                {
                    logger.LogWarning(
                        "Bank status check failed for GPay order {GpayOrderId}: {Message}",
                        gpayOrderId,
                        result.Error?.Message ?? "No status data returned.");
                    continue;
                }

                var status = result.Data;
                await gpay.ApplyPaymentAsync(new GpayPaymentSyncRequest
                {
                    GpayOrderId = gpayOrderId,
                    Reference = status.Reference,
                    TransactionReference = status.TransactionReference,
                    SourceStatementRef = status.SourceStatementRef,
                    TargetStatementRef = status.TargetStatementRef,
                    HubStatus = status.RawStatusLabel ?? status.Status,
                    Note = status.ResultDescription,
                    Amount = status.Amount ?? 0m
                });
            }
            catch (NotSupportedException ex)
            {
                logger.LogWarning(
                    ex,
                    "Skipping GPay order {GpayOrderId}; no status adapter is registered for bank {BankCode}.",
                    gpayOrderId,
                    initiation.BankCode);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Status poll failed for GPay order {GpayOrderId} at bank {BankCode}.",
                    gpayOrderId,
                    initiation.BankCode);
            }
        }

        await uow.CompleteAsync();
    }

    private static bool IsStatusPollable(string? status)
    {
        var normalized = new string((status ?? string.Empty)
            .Where(char.IsLetterOrDigit)
            .Select(char.ToUpperInvariant)
            .ToArray());

        return normalized is "SUBMITTED" or "SUBMITTEDTOQUEUE" or "PENDINGBANKTRANSFER" or "BANKTRANSFERPROCESSING";
    }

    private static (string? TransactionReference, string? ApiReference) ReadPaymentReferences(string? responseJson)
    {
        if (string.IsNullOrWhiteSpace(responseJson))
        {
            return (null, null);
        }

        try
        {
            using var document = JsonDocument.Parse(responseJson);
            if (document.RootElement.ValueKind != JsonValueKind.Object ||
                !TryGetProperty(document.RootElement, "Correlations", out var correlations) ||
                correlations.ValueKind != JsonValueKind.Array)
            {
                return (null, null);
            }

            string? transactionReference = null;
            string? apiReference = null;
            foreach (var correlation in correlations.EnumerateArray())
            {
                if (correlation.ValueKind != JsonValueKind.Object)
                {
                    continue;
                }

                if (!TryGetProperty(correlation, "Correlation", out var typeElement) &&
                    !TryGetProperty(correlation, "Type", out typeElement))
                {
                    continue;
                }

                var type = typeElement.ValueKind == JsonValueKind.Number
                    ? typeElement.GetInt32()
                    : int.TryParse(typeElement.ToString(), out var parsedType) ? parsedType : 0;
                var value = ReadString(correlation, "CorrelationId") ?? ReadString(correlation, "Value");

                if (type == 3)
                {
                    apiReference = value;
                }
                else if (type == 4)
                {
                    transactionReference = value;
                }
            }

            return (transactionReference, apiReference);
        }
        catch (JsonException)
        {
            return (null, null);
        }
    }

    private static string? ReadString(JsonElement element, string propertyName) =>
        TryGetProperty(element, propertyName, out var value) && value.ValueKind == JsonValueKind.String
            ? value.GetString()
            : null;

    private static bool TryGetProperty(JsonElement element, string propertyName, out JsonElement value)
    {
        foreach (var property in element.EnumerateObject())
        {
            if (string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            {
                value = property.Value;
                return true;
            }
        }

        value = default;
        return false;
    }
}
