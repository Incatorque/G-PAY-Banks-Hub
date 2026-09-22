using System.ComponentModel.DataAnnotations;

namespace GPay.Banking.BankHub;

/// <summary>
/// Request to initiate an instant payment (PayShap / RTC / related rails).
/// </summary>
/// <remarks>
/// Maps to Absa CAPI <c>api/payment/initiate</c> Economics / Authorisation fields via the Absa adapter.
/// Amount must be greater than zero.
/// </remarks>
public class InitiatePaymentRequestDto
{
    /// <summary>
    /// Target bank: <c>Absa</c>, <c>Fnb</c>, or <c>Nedbank</c>. Defaults to <c>Absa</c>.
    /// </summary>
    /// <example>Absa</example>
    public string Bank { get; set; } = "Absa";

    /// <summary>
    /// Debit (from) account number.
    /// </summary>
    /// <example>4049813068</example>
    [Required]
    public string FromAccountNumber { get; set; } = string.Empty;

    /// <summary>
    /// Credit (to) account number.
    /// </summary>
    /// <example>51000716346</example>
    [Required]
    public string ToAccountNumber { get; set; } = string.Empty;

    /// <summary>
    /// Credit branch / sort code. Required for most rails; Absa may default in some scenarios.
    /// </summary>
    /// <example>678910</example>
    public string? ToBranchCode { get; set; }

    /// <summary>
    /// Payment amount in major currency units (must be &gt; 0).
    /// </summary>
    /// <example>1.00</example>
    public decimal Amount { get; set; }

    /// <summary>
    /// ISO 4217 currency code. Defaults to <c>ZAR</c>.
    /// </summary>
    /// <example>ZAR</example>
    public string Currency { get; set; } = "ZAR";

    /// <summary>
    /// Client payment reference (maps to Absa Economics.TransactionRef / statement refs when not overridden).
    /// </summary>
    /// <example>PAY20260922-001</example>
    [Required]
    public string Reference { get; set; } = string.Empty;

    /// <summary>
    /// Beneficiary / credit account holder name.
    /// </summary>
    public string? BeneficiaryName { get; set; }

    /// <summary>
    /// Payment rail / Absa Economics.Indicator:
    /// <c>RPP</c> = PayShap (default), <c>IIP</c> = RTC, <c>PAAF</c> = other.
    /// </summary>
    /// <example>RPP</example>
    public string PaymentRail { get; set; } = "RPP";
}
