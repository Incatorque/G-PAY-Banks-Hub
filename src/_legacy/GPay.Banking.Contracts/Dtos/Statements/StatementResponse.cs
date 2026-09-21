namespace GPay.Banking.Contracts.Dtos.Statements;

/// <summary>
/// GPay statement / transaction history response.
/// </summary>
public sealed class StatementResponse
{
    /// <summary>
    /// Account number.
    /// </summary>
    public required string AccountNumber { get; init; }

    /// <summary>
    /// Transactions in the requested period.
    /// </summary>
    public IReadOnlyList<StatementTransaction> Transactions { get; init; } = [];
}
