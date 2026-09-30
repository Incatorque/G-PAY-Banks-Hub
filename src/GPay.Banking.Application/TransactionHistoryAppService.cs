using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Helpers;
using GPay.Banking.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace GPay.Banking;

/// <summary>
/// Transaction history AppService — account statement lines for a date range.
/// </summary>
/// <remarks>
/// Route: <c>POST /api/app/transaction-history/get</c>.
/// Requires JWT and <c>Banking.TransactionHistory.View</c>.
/// </remarks>
[Authorize(TransactionHistoryPermissions.Default)]
public class TransactionHistoryAppService : ApplicationService, ITransactionHistoryAppService
{
    private readonly IBankCapabilityResolver _resolver;

    public TransactionHistoryAppService(IBankCapabilityResolver resolver)
    {
        _resolver = resolver;
    }

    /// <summary>
    /// Retrieves transaction history for an account over an inclusive date range.
    /// </summary>
    /// <param name="input">Bank, account number, from/to dates, optional page size.</param>
    /// <returns>Normalized statement lines plus <c>correlationId</c>.</returns>
    /// <remarks>Permission: <c>Banking.TransactionHistory.View</c>.</remarks>
    [Authorize(TransactionHistoryPermissions.View)]
    public async Task<TransactionHistoryResultDto> GetAsync(TransactionHistoryRequestDto input)
    {
        if (string.IsNullOrWhiteSpace(input.AccountNumber))
        {
            throw new UserFriendlyException("Account number is required.");
        }

        if (input.ToDate < input.FromDate)
        {
            throw new UserFriendlyException("ToDate must be on or after FromDate.");
        }

        if (input.PageSize is <= 0)
        {
            throw new UserFriendlyException("PageSize must be greater than zero when supplied.");
        }

        var bank = BankCodeParser.Parse(input.Bank);
        var correlationId = Guid.NewGuid().ToString("N");
        var service = _resolver.GetStatement(bank);

        var result = await service.GetStatementAsync(
            new StatementRequest
            {
                AccountNumber = input.AccountNumber.Trim(),
                FromDate = input.FromDate,
                ToDate = input.ToDate,
                PageSize = input.PageSize
            },
            correlationId);

        return Map(result, correlationId, input.FromDate, input.ToDate);
    }

    private static TransactionHistoryResultDto Map(
        ApiResult<StatementResponse> result,
        string correlationId,
        DateOnly fromDate,
        DateOnly toDate)
    {
        if (!result.Success || result.Data is null)
        {
            return new TransactionHistoryResultDto
            {
                Success = false,
                CorrelationId = correlationId,
                FromDate = fromDate,
                ToDate = toDate,
                ErrorCode = result.Error?.Code,
                ErrorMessage = result.Error?.Message,
                AbsaError = AbsaErrorResponses.From(result.Error)
            };
        }

        var data = result.Data;
        return new TransactionHistoryResultDto
        {
            Success = true,
            CorrelationId = correlationId,
            AccountNumber = data.AccountNumber,
            FromDate = fromDate,
            ToDate = toDate,
            Transactions = data.Transactions
                .Select(t => new TransactionHistoryLineDto
                {
                    TransactionDateUtc = t.TransactionDateUtc,
                    Amount = t.Amount,
                    Description = t.Description,
                    Reference = t.Reference,
                    BalanceAfter = t.BalanceAfter
                })
                .ToList()
        };
    }
}
