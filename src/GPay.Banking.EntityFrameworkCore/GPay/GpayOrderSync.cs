using GPay.Banking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GPay.Banking.EntityFrameworkCore.GPay;

/// <summary>
/// Applies Absa outcomes to the GPay database the way Absa host-to-host does:
/// an open order becomes Pending Bank Transfer, Completed, or Error, with an <c>OrderHistory</c> row.
/// A completed debit inserts <c>EntityBankStatement</c> and moves the order to Reconciled.
/// </summary>
public sealed class GpayOrderSync : IGpayOrderSync
{
    private readonly IDbContextFactory<GPayLegacyDbContext> _contexts;
    private readonly IConfiguration _configuration;
    private readonly ILogger<GpayOrderSync> _logger;

    public GpayOrderSync(
        IDbContextFactory<GPayLegacyDbContext> contexts,
        IConfiguration configuration,
        ILogger<GpayOrderSync> logger)
    {
        _contexts = contexts;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task ApplyPaymentAsync(GpayPaymentSyncRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_configuration.GetConnectionString("GPay")))
        {
            return;
        }

        var statusId = GpayOrderStatusIds.FromHubStatus(request.HubStatus);
        if (statusId is null)
        {
            return;
        }

        try
        {
            await using var db = await _contexts.CreateDbContextAsync(cancellationToken);
            var order = await FindOrderAsync(db, request, cancellationToken);
            if (order is null)
            {
                _logger.LogInformation(
                    "GPay order not found for payment Reference={Reference} TxRef={TxRef}",
                    request.Reference,
                    request.TransactionReference);
                return;
            }

            if (!GpayOrderStatusIds.IsOpen(order.FkOrderStatusId))
            {
                _logger.LogInformation(
                    "GPay order {OrderNumber} is not in PRS or an in-flight bank status; left unchanged",
                    order.OrderNumber);
                return;
            }

            var note = Trim(request.Note, 500) ?? request.HubStatus;
            await SetStatusAsync(db, order, statusId.Value, note, cancellationToken);

            if (statusId == GpayOrderStatusIds.Completed)
            {
                var statementId = await InsertStatementAsync(db, order, request, cancellationToken);
                if (statementId is not null)
                {
                    order.FkEntityBankStatementId = statementId;
                    await SetStatusAsync(db, order, GpayOrderStatusIds.Reconciled, "Statement linked", cancellationToken);
                }
            }

            await db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(
                "GPay order {OrderNumber} updated to {Status}",
                order.OrderNumber,
                request.HubStatus);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GPay order sync failed for Reference={Reference}", request.Reference);
        }
    }

    private static async Task SetStatusAsync(
        GPayLegacyDbContext db,
        GpayOrder order,
        Guid statusId,
        string? note,
        CancellationToken cancellationToken)
    {
        if (order.FkOrderStatusId == statusId && order.FkEntityBankStatementId is null)
        {
            return;
        }

        order.FkOrderStatusId = statusId;
        db.OrderHistories.Add(new GpayOrderHistory
        {
            Id = Guid.NewGuid(),
            FkOrderId = order.Id,
            FkOrderStatusId = statusId,
            Date = DateTime.Now,
            Note = note,
            FkHistoryCategoryId = GpayOrderStatusIds.StatusChangeCategory,
            Active = true
        });
    }

    private static async Task<Guid?> InsertStatementAsync(
        GPayLegacyDbContext db,
        GpayOrder order,
        GpayPaymentSyncRequest request,
        CancellationToken cancellationToken)
    {
        if (order.FkFromAccountId is null || order.FkEntityBankStatementId is not null || request.Amount == 0)
        {
            return null;
        }

        var eventNumber = Trim(request.TransactionReference, 50);
        if (!string.IsNullOrWhiteSpace(eventNumber))
        {
            var already = await db.EntityBankStatements.AnyAsync(
                x => x.FkAccountId == order.FkFromAccountId && x.EventNumber == eventNumber,
                cancellationToken);
            if (already)
            {
                return null;
            }
        }

        var codeId = await db.TransactionCodes
            .Where(x => x.Active != false && x.Name == "Requisition Payment")
            .Select(x => (int?)x.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (codeId is null or 0)
        {
            codeId = await db.TransactionCodes
                .Where(x => x.Active != false && x.ApplyOnDebit == true)
                .Select(x => (int?)x.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }
        if (codeId is null or 0)
        {
            return null;
        }

        var reference = Trim(First(order.ReferenceNumber, request.Reference, request.SourceStatementRef), 50);
        var statement = new GpayEntityBankStatement
        {
            Id = Guid.NewGuid(),
            FkAccountId = order.FkFromAccountId,
            FkEntityId = order.FkClientId,
            Description = Trim(First(request.SourceStatementRef, request.TargetStatementRef, "ABSA CAPI"), 250),
            ReferenceNumber = reference,
            TransactionDate = DateTime.Now,
            Amount = -Math.Abs(request.Amount),
            SyncDate = DateTime.Now,
            Reconciled = true,
            IsIntra = false,
            IsProcessed = true,
            Active = true,
            EventNumber = eventNumber,
            TransactionCodeId = codeId.Value,
            SupplierBankReference = Trim(order.SupplierBankReference, 50)
        };

        db.EntityBankStatements.Add(statement);
        return statement.Id;
    }

    private static async Task<GpayOrder?> FindOrderAsync(
        GPayLegacyDbContext db,
        GpayPaymentSyncRequest request,
        CancellationToken cancellationToken)
    {
        var keys = new[]
            {
                request.Reference,
                request.TransactionReference,
                request.SourceStatementRef,
                request.TargetStatementRef
            }
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x!.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        foreach (var key in keys)
        {
            var number = TryOrderNumber(key);
            if (number is null)
            {
                continue;
            }

            var byNumber = await db.Orders.FirstOrDefaultAsync(
                x => x.OrderNumber == number && x.Active != false,
                cancellationToken);
            if (byNumber is not null)
            {
                return byNumber;
            }
        }

        var shortKeys = keys.Where(x => x.Length <= 20).ToList();
        var mediumKeys = keys.Where(x => x.Length <= 50).ToList();
        if (shortKeys.Count == 0 && mediumKeys.Count == 0)
        {
            return null;
        }

        var matches = await db.Orders
            .Where(x => x.Active != false &&
                        ((shortKeys.Count > 0 && x.ReferenceNumber != null && shortKeys.Contains(x.ReferenceNumber)) ||
                         (mediumKeys.Count > 0 && x.SupplierBankReference != null && mediumKeys.Contains(x.SupplierBankReference)) ||
                         (mediumKeys.Count > 0 && x.ExtRef != null && mediumKeys.Contains(x.ExtRef))))
            .Take(2)
            .ToListAsync(cancellationToken);

        return matches.Count == 1 ? matches[0] : null;
    }

    private static int? TryOrderNumber(string value)
    {
        var text = value.Trim();
        if (text.StartsWith("GPAY", StringComparison.OrdinalIgnoreCase))
        {
            text = text[4..].Trim();
        }

        var digits = new string(text.TakeWhile(char.IsDigit).ToArray());
        return int.TryParse(digits, out var number) && number > 0 ? number : null;
    }

    private static string? First(params string?[] values) =>
        values.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x))?.Trim();

    private static string? Trim(string? value, int max)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var text = value.Trim();
        return text.Length <= max ? text : text[..max];
    }
}
