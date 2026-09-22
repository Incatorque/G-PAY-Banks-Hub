namespace GPay.Banking.BankHub;

/// <summary>
/// Transaction history result for an account and date range.
/// </summary>
public class TransactionHistoryResultDto
{
    /// <summary>
    /// <c>true</c> when the bank call completed and returned data
    /// (an empty <see cref="Transactions"/> list is still a successful call).
    /// </summary>
    public bool Success { get; set; }

    /// <summary>Server-allocated correlation id for this bank call.</summary>
    public string? CorrelationId { get; set; }

    /// <summary>Account number echoed from the request / bank.</summary>
    public string? AccountNumber { get; set; }

    /// <summary>Inclusive from-date used for the enquiry.</summary>
    public DateOnly? FromDate { get; set; }

    /// <summary>Inclusive to-date used for the enquiry.</summary>
    public DateOnly? ToDate { get; set; }

    /// <summary>Statement lines for the period (may be empty).</summary>
    public List<TransactionHistoryLineDto> Transactions { get; set; } = new();

    /// <summary>Count of <see cref="Transactions"/> (convenience for clients).</summary>
    public int Count => Transactions.Count;

    /// <summary>Normalized or bank error code when unsuccessful.</summary>
    public string? ErrorCode { get; set; }

    /// <summary>Error detail when <see cref="Success"/> is <c>false</c>.</summary>
    public string? ErrorMessage { get; set; }
}
