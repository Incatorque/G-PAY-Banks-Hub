using System.ComponentModel.DataAnnotations;

namespace GPay.Banking.BankHub;

/// <summary>
/// Instant payment request — same body as legacy <c>InstantPaymentRequest</c>
/// (<c>POST /api/{bank}/payments/instant</c>), plus <see cref="Bank"/> for the hub route.
/// </summary>
public class InitiatePaymentRequestDto
{
    /// <summary>Target bank. Defaults to Absa for backwards compatibility.</summary>
    /// <example>Absa</example>
    public string Bank { get; set; } = "Absa";

    /// <summary>Debit account number.</summary>
    [Required]
    public string FromAccountNumber { get; set; } = string.Empty;

    /// <summary>Credit account number.</summary>
    [Required]
    public string ToAccountNumber { get; set; } = string.Empty;

    /// <summary>Credit branch / sort code.</summary>
    [Required]
    public string ToBranchCode { get; set; } = string.Empty;

    /// <summary>Optional target bank BICFI. Required for Capitec when no configured bank mapping matches.</summary>
    public string? ToBankBicfi { get; set; }

    /// <summary>Payment amount in major currency units.</summary>
    public decimal Amount { get; set; }

    /// <summary>ISO 4217 currency code (default ZAR).</summary>
    public string Currency { get; set; } = "ZAR";

    /// <summary>Client payment reference (Economics.TransactionRef).</summary>
    [Required]
    public string Reference { get; set; } = string.Empty;

    /// <summary>Beneficiary name.</summary>
    public string? BeneficiaryName { get; set; }

    /// <summary>Beneficiary bank name/code. Used by bank-specific schedule rules to identify on-us payments.</summary>
    public string? BeneficiaryBank { get; set; }

    /// <summary>Bank-specific payment rail identifier. Leave empty only when the bank adapter can derive it.</summary>
    public string PaymentRail { get; set; } = string.Empty;

    /// <summary>Legacy Absa CAPI submitting entity field; ignored by adapters that do not use it.</summary>
    public string? SubmittingEntityName { get; set; }

    /// <summary>Legacy Absa CAPI subsidiary entity field; ignored by adapters that do not use it.</summary>
    public string? SubsidiaryEntityName { get; set; }

    /// <summary>Legacy Absa CAPI authorization indicator.</summary>
    public int AuthorisationIndicator { get; set; }

    /// <summary>Source account type (default 10 = Current).</summary>
    public int FromAccountType { get; set; } = 10;

    /// <summary>Legacy Absa CAPI source short name.</summary>
    public string? FromShortName { get; set; }

    /// <summary>Source statement reference (defaults to Reference).</summary>
    public string? FromStatementRef { get; set; }

    /// <summary>Target account type (default 10).</summary>
    public int ToAccountType { get; set; } = 10;

    /// <summary>Target statement reference (defaults to Reference).</summary>
    public string? ToStatementRef { get; set; }

    /// <summary>Target trust-account flag: Y or N (default N).</summary>
    public string? IsTrustAccount { get; set; }

    /// <summary>Payment date yyyy-MM-dd (defaults to today SA time).</summary>
    public string? PaymentDate { get; set; }

    /// <summary>Proof-of-payment email.</summary>
    public string? ProofOfPaymentEmail { get; set; }

    /// <summary>Proof-of-payment mobile.</summary>
    public string? ProofOfPaymentMobile { get; set; }

    /// <summary>Proof-of-payment indicator: 1 = T (send), 0 = F. Legacy field.</summary>
    public int? ProofOfPaymentIndicator { get; set; }

    /// <summary>Optional per-payment callback URI.</summary>
    public string? CallbackUri { get; set; }

    /// <summary>Optional per-payment callback token.</summary>
    public string? CallbackToken { get; set; }

    /// <summary>Optional per-payment callback support email.</summary>
    public string? CallbackSupportEmail { get; set; }
}
