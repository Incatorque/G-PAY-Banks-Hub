using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Dtos.Balances;

namespace GPay.Banking.Contracts.Interfaces;

/// <summary>
/// Account balance capability implemented by each bank adapter.
/// </summary>
public interface IBalanceService
{
    /// <summary>
    /// Retrieves account balances from the bank.
    /// </summary>
    Task<ApiResult<BalanceResponse>> GetBalanceAsync(
        BalanceRequest request,
        string correlationId,
        CancellationToken cancellationToken = default);
}
