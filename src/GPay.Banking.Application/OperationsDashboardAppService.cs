using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace GPay.Banking;

[Authorize]
public class OperationsDashboardAppService(
    IRepository<BankHubApiCall, Guid> apiCallRepository,
    IGpayOrderSync gpayOrderSync)
    : ApplicationService, IOperationsDashboardAppService
{
    public async Task<BankApiCallPageDto> GetCallsAsync(BankApiCallQueryDto input)
    {
        var allCalls = await apiCallRepository.GetQueryableAsync();
        var query = allCalls;

        if (!string.IsNullOrWhiteSpace(input.Bank))
        {
            query = query.Where(call => call.BankCode == input.Bank);
        }

        if (!string.IsNullOrWhiteSpace(input.Direction))
        {
            query = query.Where(call => call.Direction == input.Direction);
        }

        if (!string.IsNullOrWhiteSpace(input.Operation))
        {
            query = query.Where(call => call.Operation == input.Operation);
        }

        if (input.Success.HasValue)
        {
            query = query.Where(call => call.Success == input.Success.Value);
        }

        if (input.From.HasValue)
        {
            query = query.Where(call => call.CreationTime >= input.From.Value);
        }

        if (input.To.HasValue)
        {
            query = query.Where(call => call.CreationTime < input.To.Value);
        }

        if (!string.IsNullOrWhiteSpace(input.Search))
        {
            var search = input.Search.Trim();
            var hasOrderId = Guid.TryParse(search, out var orderId);
            query = query.Where(call =>
                (call.CorrelationId != null && call.CorrelationId.Contains(search)) ||
                (call.Path != null && call.Path.Contains(search)) ||
                (call.ErrorCode != null && call.ErrorCode.Contains(search)) ||
                (hasOrderId && call.GpayOrderId == orderId));
        }

        var totalCount = await AsyncExecuter.CountAsync(query);
        var failedCount = await AsyncExecuter.CountAsync(query.Where(call => !call.Success));
        var averageDuration = await AsyncExecuter.AverageAsync(
            query.Where(call => call.DurationMs.HasValue),
            call => (double?)call.DurationMs);
        var minDuration = await AsyncExecuter.MinAsync(query.Select(call => call.DurationMs));
        var maxDuration = await AsyncExecuter.MaxAsync(query.Select(call => call.DurationMs));
        var outboundCount = await AsyncExecuter.CountAsync(query.Where(call => call.Direction == "Outbound"));
        var inboundCount = await AsyncExecuter.CountAsync(query.Where(call => call.Direction == "Inbound"));
        var uniqueOrderCount = await AsyncExecuter.CountAsync(query
            .Where(call => call.GpayOrderId.HasValue)
            .Select(call => call.GpayOrderId)
            .Distinct());

        var take = Math.Clamp(input.Take, 1, 100);
        var skip = Math.Max(0, input.Skip);
        var today = Clock.Now.Date;
        var trendStart = today.AddDays(-13);
        var dailyActivity = await AsyncExecuter.ToListAsync(query
            .Where(call => call.CreationTime >= trendStart)
            .GroupBy(call => call.CreationTime.Date)
            .Select(group => new BankApiCallTrendDto
            {
                Day = group.Key,
                Total = group.Count(),
                Failed = group.Count(call => !call.Success),
                AverageDurationMs = group.Where(call => call.DurationMs.HasValue)
                    .Average(call => (double?)call.DurationMs)
            })
            .OrderBy(item => item.Day));
        var bankBreakdown = await AsyncExecuter.ToListAsync(query
            .GroupBy(call => call.BankCode)
            .Select(group => new BankApiCallBreakdownDto
            {
                Label = group.Key,
                Total = group.Count(),
                Failed = group.Count(call => !call.Success),
                SuccessRate = Math.Round(100d * (group.Count() - group.Count(call => !call.Success)) / group.Count(), 1),
                AverageDurationMs = group.Where(call => call.DurationMs.HasValue)
                    .Average(call => (double?)call.DurationMs)
            })
            .OrderByDescending(item => item.Total)
            .ThenBy(item => item.Label));
        var statusBreakdown = await AsyncExecuter.ToListAsync(query
            .GroupBy(call => call.HttpStatus)
            .Select(group => new BankApiCallBreakdownDto
            {
                Label = group.Key.HasValue ? group.Key.Value.ToString() : "No response",
                Total = group.Count(),
                Failed = group.Count(call => !call.Success),
                SuccessRate = Math.Round(100d * (group.Count() - group.Count(call => !call.Success)) / group.Count(), 1),
                AverageDurationMs = group.Where(call => call.DurationMs.HasValue)
                    .Average(call => (double?)call.DurationMs)
            })
            .OrderByDescending(item => item.Total));
        var operationBreakdown = await AsyncExecuter.ToListAsync(query
            .GroupBy(call => call.Operation)
            .Select(group => new BankApiCallBreakdownDto
            {
                Label = group.Key,
                Total = group.Count(),
                Failed = group.Count(call => !call.Success),
                SuccessRate = Math.Round(100d * (group.Count() - group.Count(call => !call.Success)) / group.Count(), 1),
                AverageDurationMs = group.Where(call => call.DurationMs.HasValue)
                    .Average(call => (double?)call.DurationMs)
            })
            .OrderByDescending(item => item.Total)
            .ThenBy(item => item.Label));
        var directionBreakdown = await AsyncExecuter.ToListAsync(query
            .GroupBy(call => call.Direction)
            .Select(group => new BankApiCallBreakdownDto
            {
                Label = group.Key,
                Total = group.Count(),
                Failed = group.Count(call => !call.Success),
                SuccessRate = Math.Round(100d * (group.Count() - group.Count(call => !call.Success)) / group.Count(), 1),
                AverageDurationMs = group.Where(call => call.DurationMs.HasValue)
                    .Average(call => (double?)call.DurationMs)
            })
            .OrderByDescending(item => item.Total));
        var failureBreakdown = await AsyncExecuter.ToListAsync(query
            .Where(call => !call.Success)
            .GroupBy(call => call.ErrorCode ?? "Unspecified")
            .Select(group => new BankApiCallBreakdownDto
            {
                Label = group.Key,
                Total = group.Count(),
                Failed = group.Count(),
                SuccessRate = 0
            })
            .OrderByDescending(item => item.Total)
            .ThenBy(item => item.Label)
            .Take(8));
        var items = await AsyncExecuter.ToListAsync(query
            .OrderByDescending(call => call.CreationTime)
            .Select(call => new BankApiCallListItemDto
            {
                Id = call.Id,
                CreationTime = call.CreationTime,
                BankCode = call.BankCode,
                Direction = call.Direction,
                Operation = call.Operation,
                HttpMethod = call.HttpMethod,
                Path = call.Path,
                HttpStatus = call.HttpStatus,
                CorrelationId = call.CorrelationId,
                GpayOrderId = call.GpayOrderId,
                DurationMs = call.DurationMs,
                Success = call.Success,
                ErrorCode = call.ErrorCode
            })
            .Skip(skip)
            .Take(take));

        return new BankApiCallPageDto
        {
            TotalCount = totalCount,
            PageCount = items.Count,
            FailedCount = failedCount,
            SuccessRate = totalCount == 0 ? 0d : Math.Round(100d * (totalCount - failedCount) / totalCount, 1),
            AverageDurationMs = averageDuration,
            MinDurationMs = minDuration,
            MaxDurationMs = maxDuration,
            OutboundCount = outboundCount,
            InboundCount = inboundCount,
            UniqueOrderCount = uniqueOrderCount,
            Items = items,
            Banks = await AsyncExecuter.ToListAsync(allCalls.Select(call => call.BankCode).Distinct().OrderBy(bank => bank)),
            Operations = await AsyncExecuter.ToListAsync(allCalls.Select(call => call.Operation).Distinct().OrderBy(operation => operation)),
            DailyActivity = dailyActivity,
            BankBreakdown = bankBreakdown,
            StatusBreakdown = statusBreakdown,
            OperationBreakdown = operationBreakdown,
            DirectionBreakdown = directionBreakdown,
            FailureBreakdown = failureBreakdown
        };
    }

    public async Task<BankApiCallDto> GetCallAsync(Guid id)
    {
        var call = await apiCallRepository.GetAsync(id);
        return new BankApiCallDto
        {
            Id = call.Id,
            CreationTime = call.CreationTime,
            BankCode = call.BankCode,
            Direction = call.Direction,
            Operation = call.Operation,
            HttpMethod = call.HttpMethod,
            Path = call.Path,
            HttpStatus = call.HttpStatus,
            CorrelationId = call.CorrelationId,
            GpayOrderId = call.GpayOrderId,
            DurationMs = call.DurationMs,
            Success = call.Success,
            ErrorCode = call.ErrorCode,
            RequestJson = call.RequestJson,
            ResponseJson = call.ResponseJson
        };
    }

    public async Task<BankPaymentQueuePageDto> GetTransactionsAsync(BankPaymentQueueQueryDto input)
    {
        var page = await gpayOrderSync.GetQueuedAndSubmittedOrdersAsync(
            input.Skip,
            input.Take,
            input.Bank,
            input.Status,
            input.Search);

        return new BankPaymentQueuePageDto
        {
            TotalCount = page.TotalCount,
            Banks = page.Banks.ToList(),
            Items = page.Items.Select(item => new BankPaymentQueueItemDto
            {
                OrderId = item.OrderId,
                OrderNumber = item.OrderNumber,
                Reference = item.Reference,
                Bank = item.Bank,
                Route = item.Route,
                Rail = item.Rail,
                Status = item.Status,
                OrderDate = item.OrderDate,
                Amount = item.Amount,
                Currency = item.Currency
            }).ToList()
        };
    }

}
