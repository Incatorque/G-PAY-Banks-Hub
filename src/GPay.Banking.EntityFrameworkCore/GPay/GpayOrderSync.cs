using GPay.Banking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GPay.Banking.EntityFrameworkCore.GPay;

/// <summary>
/// Reads PayShap orders from the shared GPay database through EF Core LINQ.
/// </summary>
public sealed class GpayOrderSync : IGpayOrderSync
{
    private const string PaymentAcceptedTransactionCode = "3PIM Transaction";

    private readonly BankingDbContext _dbContext;
    private readonly ILogger<GpayOrderSync> _logger;

    public GpayOrderSync(
        BankingDbContext dbContext,
        ILogger<GpayOrderSync> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IReadOnlyList<GpayPaymentOrderCandidate>> GetEligiblePaymentOrdersAsync(
        int batchSize,
        IReadOnlyCollection<BankCode> enabledBanks,
        CancellationToken cancellationToken = default)
    {
        if (enabledBanks.Count == 0)
        {
            return [];
        }

        var includeAbsa = enabledBanks.Contains(BankCode.Absa);

        var includeFnb = enabledBanks.Contains(BankCode.Fnb);

        var includeNedbank = enabledBanks.Contains(BankCode.Nedbank);

        var prsOrders = _dbContext.Set<GpayOrderRow>()
            .AsNoTracking()
            .Where(order =>
                order.StatusId == GpayOrderStatusIds.Prs &&
                order.Active == true);

        var ordersFromEnabledBanks =
            from order in prsOrders
            join payer in _dbContext.Set<GpayAccountRow>().AsNoTracking()
                on order.FromAccountId equals (Guid?)payer.Id
            join payerBank in _dbContext.Set<GpayBankRow>().AsNoTracking()
                on payer.BankId equals payerBank.Id
            where payer.Active == true &&
                  payerBank.Active == true &&
                  order.FromAccountId != order.ToAccountId &&
                  ((includeAbsa && payerBank.Name.ToUpper().Contains("ABSA"))
                  || (includeFnb &&
                      (payerBank.Name.ToUpper().Contains("FNB") ||
                       payerBank.Name.ToUpper().Contains("FIRST NATIONAL")))
                  || (includeNedbank && payerBank.Name.ToUpper().Contains("NEDBANK")))
            select new
            {
                Order = order,
                Payer = payer,
                Bank = payerBank
            };

        var ordersWithPayeeAccounts =
            from candidate in ordersFromEnabledBanks
            join payee in _dbContext.Set<GpayAccountRow>().AsNoTracking()
                on candidate.Order.ToAccountId equals (Guid?)payee.Id
            join countryCurrency in _dbContext.Set<GpayCountryCurrencyRow>().AsNoTracking()
    on candidate.Payer.CountryCurrencyId equals (int?)countryCurrency.Id
    into countryCurrencyGroup
            from countryCurrency in countryCurrencyGroup.DefaultIfEmpty()

            join currency in _dbContext.Set<GpayCurrencyRow>().AsNoTracking()
                on countryCurrency.CurrencyId equals currency.Id
                into currencyGroup
            from currency in currencyGroup.DefaultIfEmpty()
            select new
            {
                candidate.Order,
                candidate.Payer,
                candidate.Bank,
                Payee = payee,
                Currency = currency.Code
            };

        var ordersWithAmount =
            from candidate in ordersWithPayeeAccounts
            let amount = _dbContext.Set<GpayOrderItemRow>()
                .Where(item => item.OrderId == candidate.Order.Id)
                .Sum(item => (item.Price ?? 0m) * (item.Quantity ?? 0m))
            where amount > 0m
            select new
            {
                Candidate = candidate,
                Amount = amount
            };

        var query =
            from candidate in ordersWithAmount
            orderby candidate.Candidate.Order.OrderDate,
                candidate.Candidate.Order.OrderNumber
            select new
            {
                OrderId = candidate.Candidate.Order.Id,
                candidate.Candidate.Order.OrderNumber,
                candidate.Candidate.Order.ReferenceNumber,
                BankName = candidate.Candidate.Bank.Name,
                candidate.Amount,
                FromAccountNumber = candidate.Candidate.Payer.AccountNumber,
                ToAccountNumber = candidate.Candidate.Payee.AccountNumber,
                ToBranchCode = candidate.Candidate.Payee.BranchCode,
                candidate.Candidate.Currency,
                BeneficiaryName = _dbContext.Set<GpayEntityRow>()
                    .Where(entity => entity.Id == candidate.Candidate.Payee.EntityId)
                    .Select(entity => entity.Name)
                    .FirstOrDefault()
            };

        var rows = await query
            .Take(Math.Max(1, batchSize))
            .ToListAsync(cancellationToken);
        var candidates = rows
            .Select(row => new GpayPaymentOrderCandidate
            {
                OrderId = row.OrderId,
                OrderNumber = row.OrderNumber.ToString(),
                Bank = NormalizeBank(row.BankName),
                Reference = string.IsNullOrWhiteSpace(row.ReferenceNumber)
                    ? row.OrderNumber.ToString()
                    : row.ReferenceNumber,
                Amount = row.Amount,
                Currency = row.Currency,
                FromAccountNumber = row.FromAccountNumber,
                ToAccountNumber = row.ToAccountNumber,
                ToBranchCode = row.ToBranchCode,
                BeneficiaryName = row.BeneficiaryName
            })
            .ToList();

        var eligibleCandidates = new List<GpayPaymentOrderCandidate>(candidates.Count);
        foreach (var order in candidates)
        {
            var currentOrderStatus = await GetOrderStatusAsync(order.OrderId, cancellationToken);
            if (!string.Equals(currentOrderStatus, "PRS", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation(
                    "Skipping GPay order {OrderNumber}; its current status is {OrderStatus}, not PRS.",
                    order.OrderNumber,
                    currentOrderStatus ?? "<missing>");

                continue;
            }

            eligibleCandidates.Add(order);
        }

        candidates = eligibleCandidates;

        _logger.LogInformation(
            "GPay PRS poll found {OrderCount} eligible order(s) across enabled banks.",
            candidates.Count);

        if (candidates.Count == 0)
        {
            var prsOrderCount = await prsOrders.CountAsync(cancellationToken);

            var enabledBankOrderCount = await ordersFromEnabledBanks.CountAsync(cancellationToken);

            var orderWithPayeeCount = await ordersWithPayeeAccounts.CountAsync(cancellationToken);

            var orderWithAmountCount = await ordersWithAmount.CountAsync(cancellationToken);

            _logger.LogWarning(
                "GPay PRS poll returned no eligible orders. ActivePRS={PrsCount}, EnabledBank={BankCount}, WithPayee={PayeeCount}, WithPositiveAmount={AmountCount}. EnabledBanks={EnabledBanks}.",
                prsOrderCount,
                enabledBankOrderCount,
                orderWithPayeeCount,
                orderWithAmountCount,
                string.Join(",", enabledBanks));
        }

        return candidates;
    }

    public async Task<IReadOnlyList<GpayPaymentStatusCandidate>> GetOrdersForStatusCheckAsync(
        CancellationToken cancellationToken = default)
    {
        var startOfToday = DateTime.Today;
        var startOfTomorrow = startOfToday.AddDays(1);

        var query =
            from order in _dbContext.Set<GpayOrderRow>().AsNoTracking()
            join status in _dbContext.Set<GpayOrderStatusRow>().AsNoTracking()
                on order.StatusId equals status.Id
            join payer in _dbContext.Set<GpayAccountRow>().AsNoTracking()
                on order.FromAccountId equals (Guid?)payer.Id
            join bank in _dbContext.Set<GpayBankRow>().AsNoTracking()
                on payer.BankId equals bank.Id
            where order.OrderDate >= startOfToday
                  && order.OrderDate < startOfTomorrow
                  && status.Name.ToUpper() != "ERROR"
                  && status.Name.ToUpper() != "COMPLETED"
                  && status.Name.ToUpper() != "RECONCILED"
            orderby order.OrderDate, order.OrderNumber
            select new
            {
                order.Id,
                order.OrderNumber,
                order.ReferenceNumber,
                BankName = bank.Name,
                FromAccountNumber = payer.AccountNumber,
                ToAccountNumber = _dbContext.Set<GpayAccountRow>()
                    .Where(account => (Guid?)account.Id == order.ToAccountId)
                    .Select(account => account.AccountNumber)
                    .FirstOrDefault(),
                Currency = _dbContext.Set<GpayCountryCurrencyRow>()
                    .Where(countryCurrency => (int?)countryCurrency.Id == payer.CountryCurrencyId)
                    .Join(
                        _dbContext.Set<GpayCurrencyRow>(),
                        countryCurrency => countryCurrency.CurrencyId,
                        currency => currency.Id,
                        (countryCurrency, currency) => currency.Code)
                    .FirstOrDefault(),
                Amount = _dbContext.Set<GpayOrderItemRow>()
                    .Where(item => item.OrderId == order.Id)
                    .Sum(item => (item.Price ?? 0m) * (item.Quantity ?? 0m))
            };

        var rows = await query.ToListAsync(cancellationToken);
        var candidates = rows
            .Select(row => new GpayPaymentStatusCandidate
            {
                OrderId = row.Id,
                OrderNumber = row.OrderNumber.ToString(),
                Bank = NormalizeBank(row.BankName),
                Reference = string.IsNullOrWhiteSpace(row.ReferenceNumber)
                    ? row.OrderNumber.ToString()
                    : row.ReferenceNumber,
                Amount = row.Amount,
                Currency = row.Currency ?? string.Empty,
                FromAccountNumber = row.FromAccountNumber,
                ToAccountNumber = row.ToAccountNumber ?? string.Empty
            })
            .ToList();

        _logger.LogInformation(
            "Found {OrderCount} today's non-terminal GPay orders for bank status checks.",
            candidates.Count);

        return candidates;
    }

    public Task<string?> GetOrderStatusAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var query =
            from order in _dbContext.Set<GpayOrderRow>().AsNoTracking()
            join status in _dbContext.Set<GpayOrderStatusRow>().AsNoTracking()
                on order.StatusId equals status.Id
            where order.Id == orderId
            select status.Name;

        return query.FirstOrDefaultAsync(cancellationToken);
    }

    private static string NormalizeBank(string bankName)
    {
        var normalized = bankName.ToUpperInvariant();

        if (normalized.Contains("ABSA"))
        {
            return BankCode.Absa.ToString();
        }

        if (normalized.Contains("FNB") || normalized.Contains("FIRST NATIONAL"))
        {
            return BankCode.Fnb.ToString();
        }

        if (normalized.Contains("NEDBANK"))
        {
            return BankCode.Nedbank.ToString();
        }

        return bankName.Trim();
    }

    public async Task ApplyPaymentAsync(
        GpayPaymentSyncRequest request,
        CancellationToken cancellationToken = default)
    {
        var targetStatus = GpayOrderStatusIds.FromHubStatus(request.HubStatus);
        if (!targetStatus.HasValue)
        {
            _logger.LogDebug("No GPay order status transition for Bank Hub status {Status}.", request.HubStatus);
            return;
        }

        var query = _dbContext.Set<GpayOrderRow>().AsQueryable();
        GpayOrderRow? order = null;
        if (request.GpayOrderId.HasValue)
        {
            order = await query.FirstOrDefaultAsync(x => x.Id == request.GpayOrderId.Value, cancellationToken);
        }

        if (order is null && !string.IsNullOrWhiteSpace(request.Reference))
        {
            var referenceMatches = await query
                .Where(x => x.ReferenceNumber == request.Reference)
                .Take(2)
                .ToListAsync(cancellationToken);
            if (referenceMatches.Count == 1)
            {
                order = referenceMatches[0];
            }
            else if (referenceMatches.Count > 1)
            {
                _logger.LogWarning(
                    "Skipped GPay status sync because reference {Reference} matches multiple orders. Pass the GPay order id to resolve it safely.",
                    request.Reference);
                return;
            }
        }

        if (order is null)
        {
            _logger.LogWarning(
                "Could not find GPay order to sync Bank Hub status. OrderId={OrderId}, Reference={Reference}, Status={Status}",
                request.GpayOrderId,
                request.Reference,
                request.HubStatus);
            return;
        }

        if (order.StatusId == targetStatus.Value)
        {
            if (targetStatus.Value == GpayOrderStatusIds.Completed)
            {
                await EnsureAcceptedPaymentStatementAsync(order, request, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return;
        }

        if (!GpayOrderStatusIds.IsOpen(order.StatusId))
        {
            _logger.LogWarning(
                "Ignored Bank Hub status {HubStatus} for GPay order {OrderNumber}; its current status {CurrentStatus} is already closed.",
                request.HubStatus,
                order.OrderNumber,
                order.StatusId);
            return;
        }

        if (targetStatus.Value == GpayOrderStatusIds.Completed)
        {
            await EnsureAcceptedPaymentStatementAsync(order, request, cancellationToken);
        }

        var previousStatus = order.StatusId;
        order.StatusId = targetStatus.Value;

        var historyNote = string.IsNullOrWhiteSpace(request.Note)
            ? $"Bank Hub payment status: {request.HubStatus}"
            : request.Note;
        var history = new GpayOrderHistoryRow
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            StatusId = targetStatus.Value,
            HistoryCategoryId = GpayOrderStatusIds.StatusChangeCategory,
            Date = DateTime.Now,
            Note = historyNote.Length <= 200 ? historyNote : historyNote[..200],
            UserId = order.UserId
        };
        _dbContext.Set<GpayOrderHistoryRow>().Add(history);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated GPay order {OrderNumber} ({OrderId}) status from {PreviousStatus} to {NewStatus} after Bank Hub payment status {HubStatus}.",
            order.OrderNumber,
            order.Id,
            previousStatus,
            targetStatus.Value,
            request.HubStatus);
    }

    private async Task EnsureAcceptedPaymentStatementAsync(
        GpayOrderRow order,
        GpayPaymentSyncRequest request,
        CancellationToken cancellationToken)
    {
        if (order.EntityBankStatementId.HasValue)
        {
            return;
        }

        if (!order.FromAccountId.HasValue)
        {
            _logger.LogWarning(
                "Cannot create an accepted-payment statement for GPay order {OrderNumber}; it has no payer account.",
                order.OrderNumber);
            return;
        }

        var account = await _dbContext.Set<GpayAccountRow>()
            .AsNoTracking()
            .Where(row => row.Id == order.FromAccountId.Value)
            .Select(row => new { row.Id, row.EntityId })
            .FirstOrDefaultAsync(cancellationToken);

        var transactionCodeId = await _dbContext.Set<GpayTransactionCodeRow>()
            .AsNoTracking()
            .Where(row => row.Name == PaymentAcceptedTransactionCode && row.Active == true)
            .Select(row => (int?)row.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (account is null || !transactionCodeId.HasValue)
        {
            _logger.LogError(
                "Cannot create accepted-payment statement for GPay order {OrderNumber}; payer account or active transaction code '{TransactionCode}' was not found.",
                order.OrderNumber,
                PaymentAcceptedTransactionCode);
            return;
        }

        var amount = request.Amount > 0m
            ? request.Amount
            : await _dbContext.Set<GpayOrderItemRow>()
                .Where(item => item.OrderId == order.Id)
                .SumAsync(
                    item => (item.Price ?? 0m) * (item.Quantity ?? 0m),
                    cancellationToken);

        if (amount <= 0m)
        {
            _logger.LogError(
                "Cannot create accepted-payment statement for GPay order {OrderNumber}; its amount is not positive.",
                order.OrderNumber);
            return;
        }

        var reference = FirstNonEmpty(
            request.Reference,
            order.ReferenceNumber,
            request.TransactionReference,
            order.OrderNumber.ToString());

        var existingStatement = await _dbContext.Set<GpayEntityBankStatementRow>()
            .FirstOrDefaultAsync(
                statement => statement.AccountId == account.Id
                             && statement.ReferenceNumber == reference
                             && statement.Amount == -Math.Abs(amount),
                cancellationToken);

        var statementId = existingStatement?.Id ?? Guid.NewGuid();
        if (existingStatement is null)
        {
            var now = DateTime.Now;
            var statement = new GpayEntityBankStatementRow
            {
                Id = statementId,
                Description = reference.Length <= 250 ? reference : reference[..250],
                ReferenceNumber = reference.Length <= 50 ? reference : reference[..50],
                TransactionDate = now,
                Amount = -Math.Abs(amount),
                SyncDate = now,
                Reconciled = false,
                EntityId = account.EntityId,
                AccountId = account.Id,
                IsIntra = false,
                IsProcessed = true,
                TransactionCodeId = transactionCodeId.Value,
                SupplierBankReference = Truncate(request.TransactionReference, 50),
                Active = true
            };

            _dbContext.Set<GpayEntityBankStatementRow>().Add(statement);

            var auditUserId = order.UserId ?? await _dbContext.Set<GpayOrderHistoryRow>()
                .AsNoTracking()
                .Where(history => history.OrderId == order.Id && history.UserId.HasValue)
                .OrderByDescending(history => history.Date)
                .Select(history => history.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (auditUserId.HasValue)
            {
                _dbContext.Set<GpayEntityBankStatementLogRow>().Add(new GpayEntityBankStatementLogRow
                {
                    Id = Guid.NewGuid(),
                    EntityBankStatementId = statementId,
                    UpdatedDate = now,
                    UserId = auditUserId.Value,
                    UpdateDescription = "Bank Hub created a provisional statement after the bank accepted the payment.",
                    IsCopied = false
                });
            }
            else
            {
                _logger.LogWarning(
                    "GPay order {OrderNumber} has no user id; its EntityBankStatementLog entry cannot be created.",
                    order.OrderNumber);
            }
        }

        order.EntityBankStatementId = statementId;
    }

    private static string FirstNonEmpty(params string?[] values) =>
        values.First(value => !string.IsNullOrWhiteSpace(value))!.Trim();

    private static string? Truncate(string? value, int maxLength) =>
        string.IsNullOrWhiteSpace(value)
            ? null
            : value.Length <= maxLength ? value : value[..maxLength];
}
