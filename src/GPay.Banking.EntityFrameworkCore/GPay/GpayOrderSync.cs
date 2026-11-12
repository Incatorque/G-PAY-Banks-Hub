using GPay.Banking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GPay.Banking.EntityFrameworkCore.GPay;

/// <summary>
/// Reads Bank Hub-ready orders from the shared GPay database through EF Core LINQ.
/// </summary>
internal sealed class GpayOrderSync(
    BankingDbContext dbContext,
    IBankPaymentConfigurationProvider paymentConfigurationProvider,
    ILogger<GpayOrderSync> logger) : IGpayOrderSync
{
    private const string PaymentAcceptedTransactionCode = "3PIM Transaction";
    private const string AbsaBankNameToken = "ABSA";
    private const string FnbBankNameToken = "FNB";
    private const string FirstNationalBankNameToken = "FIRST NATIONAL";
    private const string NedbankNameToken = "NEDBANK";

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

        var bankHubReadyOrders = dbContext.Set<GpayOrderRow>()
            .AsNoTracking()
            .Where(order => order.StatusId == GpayOrderStatusIds.BankHubPrs && order.Active == true);

        var ordersFromEnabledBanks =
            from order in bankHubReadyOrders
            join payer in dbContext.Set<GpayAccountRow>().AsNoTracking()
                on order.FromAccountId equals (Guid?)payer.Id
            join payerBank in dbContext.Set<GpayBankRow>().AsNoTracking()
                on payer.BankId equals payerBank.Id
            let bankName = payerBank.Name.ToUpper()
            where payer.Active == true &&
                  payerBank.Active == true &&
                  order.FromAccountId != order.ToAccountId &&
                  ((includeAbsa && bankName.Contains(AbsaBankNameToken)) ||
                   (includeFnb &&
                    (bankName.Contains(FnbBankNameToken) || bankName.Contains(FirstNationalBankNameToken))) ||
                   (includeNedbank && bankName.Contains(NedbankNameToken)))
            select new
            {
                Order = order,
                Payer = payer,
                Bank = payerBank
            };

        var ordersWithPayeeAccounts =
            from candidate in ordersFromEnabledBanks
            join payee in dbContext.Set<GpayAccountRow>().AsNoTracking()
                on candidate.Order.ToAccountId equals (Guid?)payee.Id
            join payeeBank in dbContext.Set<GpayBankRow>().AsNoTracking()
                on payee.BankId equals payeeBank.Id into payeeBankGroup
            from payeeBank in payeeBankGroup.DefaultIfEmpty()
            join countryCurrency in dbContext.Set<GpayCountryCurrencyRow>().AsNoTracking()
                on candidate.Payer.CountryCurrencyId equals (int?)countryCurrency.Id
                into countryCurrencyGroup
            from countryCurrency in countryCurrencyGroup.DefaultIfEmpty()

            join currency in dbContext.Set<GpayCurrencyRow>().AsNoTracking()
                on countryCurrency.CurrencyId equals currency.Id
                into currencyGroup
            from currency in currencyGroup.DefaultIfEmpty()
            select new
            {
                candidate.Order,
                candidate.Payer,
                candidate.Bank,
                Payee = payee,
                PayeeBankName = payeeBank == null ? null : payeeBank.Name,
                Currency = currency.Code
            };

        var ordersWithAmount =
            from candidate in ordersWithPayeeAccounts
            let amount = dbContext.Set<GpayOrderItemRow>()
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
                candidate.Candidate.Order.ServiceType,
                FromAccountNumber = candidate.Candidate.Payer.AccountNumber,
                ToAccountNumber = candidate.Candidate.Payee.AccountNumber,
                ToBranchCode = candidate.Candidate.Payee.BranchCode,
                ToBankName = candidate.Candidate.PayeeBankName,
                candidate.Candidate.Currency,
                BeneficiaryName = dbContext.Set<GpayEntityRow>()
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
                Reference = ResolvePaymentReference(row.ReferenceNumber, row.OrderNumber),
                ServiceType = row.ServiceType,
                Amount = row.Amount,
                Currency = row.Currency,
                FromAccountNumber = row.FromAccountNumber,
                ToAccountNumber = row.ToAccountNumber,
                ToBranchCode = row.ToBranchCode,
                BeneficiaryBank = NormalizeBank(row.ToBankName),
                BeneficiaryName = row.BeneficiaryName
            })
            .ToList();

        logger.LogInformation(
            "GPay Bank Hub-ready poll found {OrderCount} candidate order(s) across enabled banks.",
            candidates.Count);

        if (candidates.Count == 0)
        {
            var bankHubReadyOrderCount = await bankHubReadyOrders.CountAsync(cancellationToken);

            var enabledBankOrderCount = await ordersFromEnabledBanks.CountAsync(cancellationToken);

            var orderWithPayeeCount = await ordersWithPayeeAccounts.CountAsync(cancellationToken);

            var orderWithAmountCount = await ordersWithAmount.CountAsync(cancellationToken);

            logger.LogWarning(
                "GPay Bank Hub-ready poll returned no eligible orders. ActiveBankHubReady={BankHubReadyCount}, EnabledBank={BankCount}, WithPayee={PayeeCount}, WithPositiveAmount={AmountCount}. EnabledBanks={EnabledBanks}.",
                bankHubReadyOrderCount,
                enabledBankOrderCount,
                orderWithPayeeCount,
                orderWithAmountCount,
                string.Join(",", enabledBanks));
        }

        return candidates;
    }

    public async Task<IReadOnlyList<GpayPaymentStatusCandidate>> GetOrdersForStatusCheckAsync(
        int batchSize,
        CancellationToken cancellationToken = default)
    {
        var query =
            from order in dbContext.Set<GpayOrderRow>().AsNoTracking()
            join status in dbContext.Set<GpayOrderStatusRow>().AsNoTracking()
                on order.StatusId equals status.Id
            join payer in dbContext.Set<GpayAccountRow>().AsNoTracking()
                on order.FromAccountId equals (Guid?)payer.Id
            join bank in dbContext.Set<GpayBankRow>().AsNoTracking()
                on payer.BankId equals bank.Id
            where status.Name.ToUpper() == "BankHub - SubmittedToQueue".ToUpper()
            orderby order.OrderDate, order.OrderNumber
            select new
            {
                order.Id,
                order.OrderNumber,
                SubmittedAt = dbContext.Set<GpayOrderHistoryRow>()
                    .Where(history => history.OrderId == order.Id && history.StatusId == GpayOrderStatusIds.BankHubSubmittedToQueue)
                    .Select(history => (DateTime?)history.Date)
                    .Max(),
                order.ReferenceNumber,
                BankName = bank.Name,
                FromAccountNumber = payer.AccountNumber,
                ToAccountNumber = dbContext.Set<GpayAccountRow>()
                    .Where(account => (Guid?)account.Id == order.ToAccountId)
                    .Select(account => account.AccountNumber)
                    .FirstOrDefault(),
                Currency = dbContext.Set<GpayCountryCurrencyRow>()
                    .Where(countryCurrency => (int?)countryCurrency.Id == payer.CountryCurrencyId)
                    .Join(
                        dbContext.Set<GpayCurrencyRow>(),
                        countryCurrency => countryCurrency.CurrencyId,
                        currency => currency.Id,
                        (countryCurrency, currency) => currency.Code)
                    .FirstOrDefault(),
                Amount = dbContext.Set<GpayOrderItemRow>()
                    .Where(item => item.OrderId == order.Id)
                    .Sum(item => (item.Price ?? 0m) * (item.Quantity ?? 0m))
            };

        var rows = await query.Take(Math.Max(1, batchSize)).ToListAsync(cancellationToken);
        var candidates = rows
            .Select(row => new GpayPaymentStatusCandidate
            {
                OrderId = row.Id,
                OrderNumber = row.OrderNumber.ToString(),
                Bank = NormalizeBank(row.BankName),
                Reference = ResolvePaymentReference(row.ReferenceNumber, row.OrderNumber),
                Amount = row.Amount,
                Currency = row.Currency ?? string.Empty,
                FromAccountNumber = row.FromAccountNumber,
                ToAccountNumber = row.ToAccountNumber ?? string.Empty,
                SubmittedAt = row.SubmittedAt
            })
            .ToList();

        logger.LogInformation(
            "Found {OrderCount} submitted GPay orders for bank status checks.",
            candidates.Count);

        return candidates;
    }

    public async Task<GpayPaymentQueuePage> GetQueuedAndSubmittedOrdersAsync(
        int skip,
        int take,
        string? bank,
        string? status,
        string? search,
        CancellationToken cancellationToken = default)
    {
        var query =
            from order in dbContext.Set<GpayOrderRow>().AsNoTracking()
            join orderStatus in dbContext.Set<GpayOrderStatusRow>().AsNoTracking()
                on order.StatusId equals orderStatus.Id
            join payer in dbContext.Set<GpayAccountRow>().AsNoTracking()
                on order.FromAccountId equals (Guid?)payer.Id
            join payerBank in dbContext.Set<GpayBankRow>().AsNoTracking()
                on payer.BankId equals payerBank.Id
            where order.Active == true &&
                  (order.StatusId == GpayOrderStatusIds.BankHubPrs ||
                   order.StatusId == GpayOrderStatusIds.BankHubSubmittedToQueue)
            select new { Order = order, Status = orderStatus.Name, BankName = payerBank.Name, Payer = payer };

        var availableBanks = (await query.Select(row => row.BankName).Distinct().OrderBy(name => name)
                .ToListAsync(cancellationToken))
            .Select(NormalizeBank)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (!string.IsNullOrWhiteSpace(bank))
        {
            var bankToken = bank.Trim().ToUpperInvariant() switch
            {
                "ABSA" => AbsaBankNameToken,
                "FNB" => FnbBankNameToken,
                "NEDBANK" => NedbankNameToken,
                _ => bank.Trim().ToUpperInvariant()
            };
            query = query.Where(row => row.BankName.ToUpper().Contains(bankToken));
        }

        if (string.Equals(status, "queued", StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(row => row.Order.StatusId == GpayOrderStatusIds.BankHubPrs);
        }
        else if (string.Equals(status, "submitted", StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(row => row.Order.StatusId == GpayOrderStatusIds.BankHubSubmittedToQueue);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var value = search.Trim();
            var hasOrderId = Guid.TryParse(value, out var orderId);
            query = query.Where(row =>
                row.Order.ReferenceNumber != null && row.Order.ReferenceNumber.Contains(value) ||
                row.Order.OrderNumber.ToString().Contains(value) ||
                (hasOrderId && row.Order.Id == orderId));
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var rows = await query
            .OrderByDescending(row => row.Order.OrderDate)
            .ThenByDescending(row => row.Order.OrderNumber)
            .Skip(Math.Max(0, skip))
            .Take(Math.Clamp(take, 1, 100))
            .Select(row => new
            {
                row.Order.Id,
                row.Order.OrderNumber,
                row.Order.ReferenceNumber,
                row.Order.OrderDate,
                row.Status,
                row.BankName,
                row.Order.ServiceType,
                Amount = dbContext.Set<GpayOrderItemRow>()
                    .Where(item => item.OrderId == row.Order.Id)
                    .Sum(item => (item.Price ?? 0m) * (item.Quantity ?? 0m)),
                Currency = dbContext.Set<GpayCountryCurrencyRow>()
                    .Where(countryCurrency => (int?)countryCurrency.Id == row.Payer.CountryCurrencyId)
                    .Join(dbContext.Set<GpayCurrencyRow>(), cc => cc.CurrencyId, currency => currency.Id,
                        (_, currency) => currency.Code)
                    .FirstOrDefault()
            })
            .ToListAsync(cancellationToken);

        var paymentConfigurations = new Dictionary<string, BankPaymentConfiguration?>(StringComparer.OrdinalIgnoreCase);
        foreach (var bankName in rows.Select(row => NormalizeBank(row.BankName)).Distinct(StringComparer.OrdinalIgnoreCase))
        {
            paymentConfigurations[bankName] = await paymentConfigurationProvider.GetAsync(bankName, cancellationToken);
        }

        return new GpayPaymentQueuePage
        {
            TotalCount = totalCount,
            Banks = availableBanks,
            Items = rows.Select(row => new GpayPaymentQueueItem
            {
                OrderId = row.Id,
                OrderNumber = row.OrderNumber.ToString(),
                Reference = ResolvePaymentReference(row.ReferenceNumber, row.OrderNumber),
                Bank = NormalizeBank(row.BankName),
                Route = "H2H",
                Rail = ResolvePaymentRail(
                    row.BankName,
                    row.Amount,
                    row.ServiceType,
                    paymentConfigurations.GetValueOrDefault(NormalizeBank(row.BankName))),
                Status = row.Status,
                OrderDate = row.OrderDate,
                Amount = row.Amount,
                Currency = row.Currency ?? string.Empty
            }).ToList()
        };
    }

    public Task<string?> GetOrderStatusAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var query =
            from order in dbContext.Set<GpayOrderRow>().AsNoTracking()
            join status in dbContext.Set<GpayOrderStatusRow>().AsNoTracking()
                on order.StatusId equals status.Id
            where order.Id == orderId
            select status.Name;

        return query.FirstOrDefaultAsync(cancellationToken);
    }

    public Task<bool> IsPublicHolidayAsync(DateTime date, CancellationToken cancellationToken = default)
    {
        var dayStart = date.Date;
        var nextDay = dayStart.AddDays(1);
        return dbContext.Set<GpayHolidayRow>().AsNoTracking()
            .AnyAsync(holiday => holiday.Date >= dayStart && holiday.Date < nextDay, cancellationToken);
    }

    private static string NormalizeBank(string? bankName)
    {
        if (string.IsNullOrWhiteSpace(bankName))
        {
            return string.Empty;
        }

        var normalized = bankName.ToUpperInvariant();

        if (normalized.Contains(AbsaBankNameToken))
        {
            return BankCode.Absa.ToString();
        }

        if (normalized.Contains(FnbBankNameToken) || normalized.Contains(FirstNationalBankNameToken))
        {
            return BankCode.Fnb.ToString();
        }

        if (normalized.Contains(NedbankNameToken))
        {
            return BankCode.Nedbank.ToString();
        }

        return bankName.Trim();
    }

    private static string ResolvePaymentReference(string? reference, int orderNumber) =>
        string.IsNullOrWhiteSpace(reference) ? orderNumber.ToString() : reference;

    private static string ResolvePaymentRail(
        string? bankName,
        decimal amount,
        string? serviceType,
        BankPaymentConfiguration? configuration)
    {
        var bankCode = NormalizeBank(bankName);
        if (!Enum.TryParse<BankCode>(bankCode, true, out var bank))
        {
            return string.Empty;
        }

        try
        {
            var orderRail = PaymentRailRouting.ForOrder(bank, amount, serviceType, configuration);
            return string.IsNullOrWhiteSpace(orderRail)
                ? PaymentRailRouting.ForBankRequest(bank, null)
                : orderRail;
        }
        catch (InvalidOperationException)
        {
            return string.Empty;
        }
    }

    public async Task ApplyPaymentAsync(
        GpayPaymentSyncRequest request,
        CancellationToken cancellationToken = default)
    {
        if (GpayOrderStatusIds.FromHubStatus(request.HubStatus) is not Guid targetStatus)
        {
            logger.LogDebug("No GPay order status transition for Bank Hub status {Status}.", request.HubStatus);
            return;
        }

        var query = dbContext.Set<GpayOrderRow>();
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
                logger.LogWarning(
                    "Skipped GPay status sync because reference {Reference} matches multiple orders. Pass the GPay order id to resolve it safely.",
                    request.Reference);
                return;
            }
        }

        if (order is null)
        {
            logger.LogWarning(
                "Could not find GPay order to sync Bank Hub status. OrderId={OrderId}, Reference={Reference}, Status={Status}",
                request.GpayOrderId,
                request.Reference,
                request.HubStatus);
            return;
        }

        if (order.StatusId == targetStatus)
        {
            if (targetStatus == GpayOrderStatusIds.Completed)
            {
                await EnsureAcceptedPaymentStatementAsync(order, request, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            return;
        }

        if (!GpayOrderStatusIds.IsOpen(order.StatusId))
        {
            logger.LogWarning(
                "Ignored Bank Hub status {HubStatus} for GPay order {OrderNumber}; its current status {CurrentStatus} is already closed.",
                request.HubStatus,
                order.OrderNumber,
                order.StatusId);
            return;
        }

        if (targetStatus == GpayOrderStatusIds.Completed)
        {
            await EnsureAcceptedPaymentStatementAsync(order, request, cancellationToken);
        }

        var previousStatus = order.StatusId;
        order.StatusId = targetStatus;

        var historyNote = string.IsNullOrWhiteSpace(request.Note)
            ? $"Bank Hub payment status: {request.HubStatus}"
            : request.Note;
        var history = new GpayOrderHistoryRow
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            StatusId = targetStatus,
            HistoryCategoryId = GpayOrderStatusIds.StatusChangeCategory,
            Date = DateTime.Now,
            Note = Truncate(historyNote, 200),
            UserId = order.UserId
        };
        dbContext.Set<GpayOrderHistoryRow>().Add(history);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "Updated GPay order {OrderNumber} ({OrderId}) status from {PreviousStatus} to {NewStatus} after Bank Hub payment status {HubStatus}.",
            order.OrderNumber,
            order.Id,
            previousStatus,
            targetStatus,
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
            logger.LogWarning(
                "Cannot create an accepted-payment statement for GPay order {OrderNumber}; it has no payer account.",
                order.OrderNumber);
            return;
        }

        var account = await dbContext.Set<GpayAccountRow>()
            .AsNoTracking()
            .Where(row => row.Id == order.FromAccountId.Value)
            .Select(row => new { row.Id, row.EntityId })
            .FirstOrDefaultAsync(cancellationToken);

        var transactionCodeId = await dbContext.Set<GpayTransactionCodeRow>()
            .AsNoTracking()
            .Where(row => row.Name == PaymentAcceptedTransactionCode && row.Active == true)
            .Select(row => (int?)row.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (account is null || !transactionCodeId.HasValue)
        {
            logger.LogError(
                "Cannot create accepted-payment statement for GPay order {OrderNumber}; payer account or active transaction code '{TransactionCode}' was not found.",
                order.OrderNumber,
                PaymentAcceptedTransactionCode);
            return;
        }

        var amount = request.Amount > 0m
            ? request.Amount
            : await dbContext.Set<GpayOrderItemRow>()
                .Where(item => item.OrderId == order.Id)
                .SumAsync(
                    item => (item.Price ?? 0m) * (item.Quantity ?? 0m),
                    cancellationToken);

        if (amount <= 0m)
        {
            logger.LogError(
                "Cannot create accepted-payment statement for GPay order {OrderNumber}; its amount is not positive.",
                order.OrderNumber);
            return;
        }

        var reference = StringValueResolver.FirstNonEmpty(
            request.Reference,
            order.ReferenceNumber,
            request.TransactionReference,
            order.OrderNumber.ToString())!;

        var existingStatement = await dbContext.Set<GpayEntityBankStatementRow>()
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
                Description = Truncate(reference, 250)!,
                ReferenceNumber = Truncate(reference, 50)!,
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

            dbContext.Set<GpayEntityBankStatementRow>().Add(statement);

            var auditUserId = order.UserId ?? await dbContext.Set<GpayOrderHistoryRow>()
                .AsNoTracking()
                .Where(history => history.OrderId == order.Id && history.UserId.HasValue)
                .OrderByDescending(history => history.Date)
                .Select(history => history.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (auditUserId.HasValue)
            {
                dbContext.Set<GpayEntityBankStatementLogRow>().Add(new GpayEntityBankStatementLogRow
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
                logger.LogWarning(
                    "GPay order {OrderNumber} has no user id; its EntityBankStatementLog entry cannot be created.",
                    order.OrderNumber);
            }
        }

        order.EntityBankStatementId = statementId;
    }

    private static string? Truncate(string? value, int maxLength) =>
        string.IsNullOrWhiteSpace(value)
            ? null
            : value.Length <= maxLength ? value : value[..maxLength];
}
