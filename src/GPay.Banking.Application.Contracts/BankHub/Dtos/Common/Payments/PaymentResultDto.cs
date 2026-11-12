namespace GPay.Banking.BankHub;

/// <summary>
/// Normalized instant-payment result from initiate or live status enquiry.
/// </summary>
/// <remarks>
/// Typical <see cref="Status"/> values: <c>Submitted</c>, <c>Completed</c>, <c>Failed</c>,
/// <c>Duplicate</c>, <c>Pending</c>, <c>Error</c>.
/// Prefer polling get-status or waiting for the bank callback for terminal states after Submitted.
/// </remarks>
public class PaymentResultDto
{
    /// <summary>
    /// Normalized or bank-labelled payment status.
    /// </summary>
    /// <example>Submitted</example>
    public string? Status { get; set; }

    /// <summary>Echo of the client payment reference.</summary>
    public string? Reference { get; set; }

    /// <summary>Amount associated with the payment.</summary>
    public decimal? Amount { get; set; }

    /// <summary>Currency of the amount (ISO 4217).</summary>
    public string? Currency { get; set; }

    /// <summary>Bank API or message reference.</summary>
    public string? ApiReference { get; set; }

    /// <summary>Bank or scheme transaction reference.</summary>
    public string? TransactionReference { get; set; }

    /// <summary>Source statement reference, when returned by the bank.</summary>
    public string? SourceStatementRef { get; set; }

    /// <summary>Target statement reference, when returned by the bank.</summary>
    public string? TargetStatementRef { get; set; }

    /// <summary>Bank-native request message identifier, needed for Capitec status enquiry.</summary>
    public string? BankMessageIdentification { get; set; }

    /// <summary>Bank-native instruction UETR, needed for Capitec status enquiry.</summary>
    public string? BankInstructionUetr { get; set; }

    /// <summary>Bank-native numeric/string status code when supplied.</summary>
    public string? BankStatusCode { get; set; }

    /// <summary>Bank-specific payment rail used.</summary>
    public string? PaymentRail { get; set; }

    /// <summary>Human-readable bank / connector description.</summary>
    public string? ResultDescription { get; set; }

    /// <summary>Server-allocated correlation id for this bank call.</summary>
    public string? CorrelationId { get; set; }

    /// <summary>Normalized or bank error code when unsuccessful.</summary>
    public string? ErrorCode { get; set; }

    /// <summary>Error detail when <see cref="Success"/> is <c>false</c>.</summary>
    public string? ErrorMessage { get; set; }

    /// <summary>Bank-neutral normalized error details, when a bank error catalog is available.</summary>
    public BankErrorInfo? BankError { get; set; }

    /// <summary>
    /// Legacy Absa catalog entry retained for existing API clients. Use <see cref="BankError"/> for all banks.
    /// </summary>
    public AbsaErrorInfo? AbsaError { get; set; }

    /// <summary>
    /// <c>true</c> when the bank call completed and returned payment data
    /// (does not imply the funds have cleared).
    /// </summary>
    public bool Success { get; set; }
}
