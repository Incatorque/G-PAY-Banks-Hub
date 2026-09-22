using Volo.Abp.Application.Services;

namespace GPay.Banking.BankHub;

/// <summary>
/// Account transaction history (statement) API.
/// </summary>
/// <remarks>
/// <para>
/// Conventional HTTP route (ABP):
/// <c>POST /api/app/transaction-history/get</c>
/// </para>
/// <para>
/// Requires JWT Bearer. Permission: <c>Banking.TransactionHistory.View</c>.
/// Uses the bank statement capability (<see cref="Domain.IStatementService"/> /
/// <see cref="Domain.ITransactionHistoryService"/>). Absa currently returns a stub empty list until CAPI statement is wired.
/// </para>
/// </remarks>
public interface ITransactionHistoryAppService : IApplicationService
{
    /// <summary>
    /// Retrieves transaction history for an account over an inclusive date range.
    /// </summary>
    /// <param name="input">Bank, account number, from/to dates, optional page size.</param>
    /// <returns>Normalized statement lines plus <c>correlationId</c>.</returns>
    /// <remarks>Permission: <c>Banking.TransactionHistory.View</c>.</remarks>
    Task<TransactionHistoryResultDto> GetAsync(TransactionHistoryRequestDto input);
}
