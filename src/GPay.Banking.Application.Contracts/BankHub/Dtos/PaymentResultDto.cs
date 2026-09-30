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
    /// Persisted <c>BankHubPaymentRecord</c> id when a matching local record exists or was created.
    /// </summary>
    public Guid? RecordId { get; set; }

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

    /// <summary>Bank API reference (Absa Correlations type 3).</summary>
    public string? ApiReference { get; set; }

    /// <summary>Bank / scheme transaction reference (Absa Correlations type 4).</summary>
    public string? TransactionReference { get; set; }

    /// <summary>Source statement reference (Absa Correlations type 1).</summary>
    public string? SourceStatementRef { get; set; }

    /// <summary>Target statement reference (Absa Correlations type 2).</summary>
    public string? TargetStatementRef { get; set; }

    /// <summary>Bank-native numeric/string status code when supplied.</summary>
    public string? BankStatusCode { get; set; }

    /// <summary>Rail used: <c>RPP</c>, <c>IIP</c>, <c>PAAF</c>.</summary>
    public string? PaymentRail { get; set; }

    /// <summary>Human-readable bank / connector description.</summary>
    public string? ResultDescription { get; set; }

    /// <summary>Server-allocated correlation id for this bank call.</summary>
    public string? CorrelationId { get; set; }

    /// <summary>Normalized or bank error code when unsuccessful.</summary>
    public string? ErrorCode { get; set; }

    /// <summary>Error detail when <see cref="Success"/> is <c>false</c>.</summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Absa catalog entry for the bank code on this call. Null when Absa did not return an error code.
    /// </summary>
    public AbsaErrorInfo? AbsaError { get; set; }

    /// <summary>
    /// <c>true</c> when the bank call completed and returned payment data
    /// (does not imply the funds have cleared).
    /// </summary>
    public bool Success { get; set; }
}
