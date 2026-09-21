namespace GPay.Banking.Contracts.Dtos.Statements;

/// <summary>
/// A single statement / transaction history line.
/// </summary>
public sealed class StatementTransaction
{
    /// <summary>
    /// Transaction date/time (UTC).
    /// </summary>
    public DateTimeOffset TransactionDateUtc { get; init; }

    /// <summary>
    /// Transaction amount (positive credit, negative debit).
    /// </summary>
    public decimal Amount { get; init; }

    /// <summary>
    /// Narrative / description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Bank reference.
    /// </summary>
    public string? Reference { get; init; }

    /// <summary>
    /// Running balance after the transaction when supplied.
    /// </summary>
    public decimal? BalanceAfter { get; init; }
}
