namespace GPay.Banking.BankHub;

/// <summary>
/// A single transaction history / statement line.
/// </summary>
public class TransactionHistoryLineDto
{
    /// <summary>Transaction date/time (UTC).</summary>
    public DateTimeOffset TransactionDateUtc { get; set; }

    /// <summary>Amount (positive = credit, negative = debit).</summary>
    public decimal Amount { get; set; }

    /// <summary>Narrative / description from the bank.</summary>
    public string? Description { get; set; }

    /// <summary>Bank or scheme reference for the line.</summary>
    public string? Reference { get; set; }

    /// <summary>Running balance after the transaction when the bank supplies it.</summary>
    public decimal? BalanceAfter { get; set; }
}
