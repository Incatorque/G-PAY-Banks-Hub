using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;

using GPay.Banking;

namespace GPay.Banking.Domain;

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




