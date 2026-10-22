using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;

using GPay.Banking;

namespace GPay.Banking.Domain;

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
