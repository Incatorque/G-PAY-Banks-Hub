namespace GPay.Banking.Contracts.Dtos.InstantPayment;

/// <summary>
/// GPay instant payment (PayShap / RTC) request.
/// </summary>
public sealed class InstantPaymentRequest
{
    /// <summary>
    /// Debit account number.
    /// </summary>
    public required string FromAccountNumber { get; init; }

    /// <summary>
    /// Credit account number.
    /// </summary>
    public required string ToAccountNumber { get; init; }

    /// <summary>
    /// Credit branch / sort code.
    /// </summary>
    public required string ToBranchCode { get; init; }

    /// <summary>
    /// Payment amount in major currency units.
    /// </summary>
    public required decimal Amount { get; init; }

    /// <summary>
    /// ISO 4217 currency code (default ZAR).
    /// </summary>
    public string Currency { get; init; } = "ZAR";

    /// <summary>
    /// Client payment reference.
    /// </summary>
    public required string Reference { get; init; }

    /// <summary>
    /// Beneficiary name.
    /// </summary>
    public string? BeneficiaryName { get; init; }
}
