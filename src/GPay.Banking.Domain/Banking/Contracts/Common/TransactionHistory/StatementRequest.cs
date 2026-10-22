namespace GPay.Banking.Domain.Dtos;


using GPay.Banking;

/// <summary>
/// GPay statement / transaction history request.
/// </summary>
public sealed class StatementRequest
{
    /// <summary>
    /// Account number.
    /// </summary>
    public required string AccountNumber { get; init; }

    /// <summary>
    /// Inclusive start date (UTC).
    /// </summary>
    public DateOnly FromDate { get; init; }

    /// <summary>
    /// Inclusive end date (UTC).
    /// </summary>
    public DateOnly ToDate { get; init; }

    /// <summary>
    /// Maximum number of transactions to return.
    /// </summary>
    public int? PageSize { get; init; }
}




