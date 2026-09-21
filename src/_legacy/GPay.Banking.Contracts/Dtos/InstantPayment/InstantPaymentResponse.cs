namespace GPay.Banking.Contracts.Dtos.InstantPayment;

/// <summary>
/// GPay instant payment (PayShap / RTC) response.
/// </summary>
public sealed class InstantPaymentResponse
{
    /// <summary>
    /// Bank or scheme transaction identifier (legacy alias of <see cref="TransactionReference"/>).
    /// </summary>
    public string? TransactionId { get; init; }

    /// <summary>
    /// Payment status (e.g. Submitted, Completed, Failed). Prefer <see cref="RawStatusLabel"/>.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// Echo of the client payment reference.
    /// </summary>
    public string? Reference { get; init; }

    /// <summary>
    /// Amount processed.
    /// </summary>
    public decimal? Amount { get; init; }

    /// <summary>
    /// Currency of the processed amount.
    /// </summary>
    public string? Currency { get; init; }

    /// <summary>
    /// Absa Correlations type 3 (ApiRef).
    /// </summary>
    public string? ApiReference { get; init; }

    /// <summary>
    /// Absa Correlations type 4 (TransactionRef).
    /// </summary>
    public string? TransactionReference { get; init; }

    /// <summary>
    /// Raw Absa Status code from the payment response.
    /// </summary>
    public int? BankStatusCode { get; init; }

    /// <summary>
    /// Payment rail used (RPP / IIP / PAAF).
    /// </summary>
    public string? PaymentRail { get; init; }

    /// <summary>
    /// Human-readable result description.
    /// </summary>
    public string? ResultDescription { get; init; }

    /// <summary>
    /// Normalized or bank error code when failed.
    /// </summary>
    public string? ErrorCode { get; init; }

    /// <summary>
    /// Normalized status label: Submitted, Completed, Failed, Duplicate, Pending.
    /// </summary>
    public string? RawStatusLabel { get; init; }
}
