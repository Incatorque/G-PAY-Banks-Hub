using System.ComponentModel.DataAnnotations;
using GPay.Banking.Contracts.Enums;

namespace GPay.Banking.Contracts.Dtos.AccountVerification;

/// <summary>
/// Bank-agnostic GPay account verification (AVS) request.
/// Same payload for every bank; route using <see cref="Bank"/> (body) and/or URL <c>{bank}</c>.
/// </summary>
public sealed class AccountVerificationRequest
{
    /// <summary>
    /// Target bank. Required when calling the bank-agnostic <c>/api/avs</c> endpoint.
    /// Ignored when the bank is already specified in the route (<c>/api/{bank}/account-verification</c>).
    /// </summary>
    public BankCode? Bank { get; init; }

    /// <summary>
    /// Bank account number to verify.
    /// </summary>
    [Required]
    [MinLength(5)]
    public required string AccountNumber { get; init; }

    /// <summary>
    /// Branch / sort code.
    /// </summary>
    [Required]
    [MinLength(4)]
    public required string BranchCode { get; init; }

    /// <summary>
    /// Issuing bank code for the account (Absa MIG: 000005=FNB, 000016=ABSA, 000018=Standard, 000021=Nedbank, 000036=African).
    /// When omitted, the bank connector may default (Absa connector defaults to Absa 000016).
    /// </summary>
    public string? IssuingBankCode { get; init; }

    /// <summary>
    /// Account holder identity number (SA ID, passport, or company registration).
    /// </summary>
    public string? IdentityNumber { get; init; }

    /// <summary>
    /// Identity type: <c>SAID</c>, <c>Passport</c>, or <c>CompanyRegistration</c>.
    /// </summary>
    public string? IdentityType { get; init; }

    /// <summary>
    /// Full account holder name (used when initials/last name are not split).
    /// </summary>
    public string? AccountHolderName { get; init; }

    /// <summary>
    /// Account holder initials.
    /// </summary>
    public string? Initials { get; init; }

    /// <summary>
    /// Account holder surname / last name / registered business name.
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// Account type hint (e.g. Current, Savings, Transmission).
    /// </summary>
    public string? AccountType { get; init; }

    /// <summary>
    /// Optional email to verify against bank records.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// Optional phone number to verify against bank records.
    /// </summary>
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Client reference for the enquiry.
    /// </summary>
    public string? Reference { get; init; }

    /// <summary>
    /// Bank-specific extra fields when a bank requires values beyond the common contract.
    /// </summary>
    public Dictionary<string, string>? AdditionalData { get; init; }
}
