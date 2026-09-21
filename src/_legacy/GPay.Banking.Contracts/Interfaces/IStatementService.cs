using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Dtos.Statements;

namespace GPay.Banking.Contracts.Interfaces;

/// <summary>
/// Statement / transaction history capability implemented by each bank adapter.
/// </summary>
public interface IStatementService
{
    /// <summary>
    /// Retrieves statement transactions for an account.
    /// </summary>
    Task<ApiResult<StatementResponse>> GetStatementAsync(
        StatementRequest request,
        string correlationId,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Alias interface for transaction history (same contract as statements).
/// </summary>
public interface ITransactionHistoryService : IStatementService
{
}
