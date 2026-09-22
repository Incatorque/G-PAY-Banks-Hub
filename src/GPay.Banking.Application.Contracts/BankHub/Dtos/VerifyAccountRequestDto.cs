using System.ComponentModel.DataAnnotations;

namespace GPay.Banking.BankHub;

/// <summary>
/// Bank-agnostic account verification (AVS) request used by single verify and batch items.
/// </summary>
/// <remarks>
/// Same shape for every bank. Routing uses <see cref="Bank"/>.
/// Example Absa issuing bank codes: 000016=ABSA, 000005=FNB, 000018=Standard, 000021=Nedbank.
/// </remarks>
public class VerifyAccountRequestDto
{
    /// <summary>
    /// Target bank: <c>Absa</c>, <c>Fnb</c>, or <c>Nedbank</c>. Defaults to <c>Absa</c>.
    /// </summary>
    /// <example>Absa</example>
    public string Bank { get; set; } = "Absa";

    /// <summary>
    /// Bank account number to verify (minimum practical length ~5 digits).
    /// </summary>
    /// <example>4049813068</example>
    [Required]
    public string AccountNumber { get; set; } = string.Empty;

    /// <summary>
    /// Branch / sort code for the account.
    /// </summary>
    /// <example>632005</example>
    [Required]
    public string BranchCode { get; set; } = string.Empty;

    /// <summary>
    /// Issuing bank code for the account (Absa MIG ValueList).
    /// When omitted, the Absa connector defaults to Absa (<c>000016</c>).
    /// </summary>
    /// <example>000016</example>
    public string? IssuingBankCode { get; set; }

    /// <summary>
    /// Account holder identity number (SA ID, passport, or company registration).
    /// </summary>
    public string? IdentityNumber { get; set; }

    /// <summary>
    /// Identity type: <c>SAID</c>, <c>Passport</c>, or <c>CompanyRegistration</c>.
    /// </summary>
    /// <example>SAID</example>
    public string? IdentityType { get; set; }

    /// <summary>
    /// Full account holder name when initials / last name are not split.
    /// </summary>
    public string? AccountHolderName { get; set; }

    /// <summary>
    /// Account holder initials (Absa AVS v00.5 treats client initials as mandatory for name matching).
    /// </summary>
    public string? Initials { get; set; }

    /// <summary>
    /// Account holder surname / last name / registered business name.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Account type hint (e.g. Current, Savings, Transmission).
    /// </summary>
    public string? AccountType { get; set; }

    /// <summary>
    /// Optional email to match against bank records.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Optional phone number to match against bank records.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Client enquiry reference echoed in the response when supplied.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Optional calling API client / tenant id stored on the persisted AVS record.
    /// </summary>
    public Guid? ApiClientId { get; set; }
}
