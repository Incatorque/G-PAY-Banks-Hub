namespace GPay.Banking.Contracts.Dtos.InstantPayment;

/// <summary>
/// GPay instant payment (PayShap / RTC) response.
/// </summary>
public sealed class InstantPaymentResponse
{
    /// <summary>
    /// Bank or scheme transaction identifier.
    /// </summary>
    public string? TransactionId { get; init; }

    /// <summary>
    /// Payment status (e.g. Accepted, Completed, Failed).
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
}
