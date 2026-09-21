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
    /// Client payment reference (maps to Economics.TransactionRef).
    /// </summary>
    public required string Reference { get; init; }

    /// <summary>
    /// Beneficiary name.
    /// </summary>
    public string? BeneficiaryName { get; init; }

    /// <summary>
    /// Payment rail / Economics.Indicator. RPP = PayShap, IIP = RTC, PAAF = other. Default RPP.
    /// </summary>
    public string PaymentRail { get; init; } = "RPP";

    /// <summary>
    /// Authorisation.SubmittingEntityName (defaults from bank config when omitted).
    /// </summary>
    public string? SubmittingEntityName { get; init; }

    /// <summary>
    /// Authorisation.SubsidiaryEntityName (defaults from bank config when omitted).
    /// </summary>
    public string? SubsidiaryEntityName { get; init; }

    /// <summary>
    /// Authorisation.Indicator (default 0).
    /// </summary>
    public int AuthorisationIndicator { get; init; }

    /// <summary>
    /// Source account type (default 10).
    /// </summary>
    public int FromAccountType { get; init; } = 10;

    /// <summary>
    /// Source short name on statement (defaults from bank config when omitted).
    /// </summary>
    public string? FromShortName { get; init; }

    /// <summary>
    /// Source statement reference (defaults to <see cref="Reference"/>).
    /// </summary>
    public string? FromStatementRef { get; init; }

    /// <summary>
    /// Target account type (default 10).
    /// </summary>
    public int ToAccountType { get; init; } = 10;

    /// <summary>
    /// Target statement reference (defaults to <see cref="Reference"/>).
    /// </summary>
    public string? ToStatementRef { get; init; }

    /// <summary>
    /// Target trust-account flag: Y or N (default N when omitted).
    /// </summary>
    public string? IsTrustAccount { get; init; }

    /// <summary>
    /// Payment date yyyy-MM-dd (defaults to today in UTC+2 South Africa).
    /// </summary>
    public string? PaymentDate { get; init; }

    /// <summary>
    /// Optional proof-of-payment email.
    /// </summary>
    public string? ProofOfPaymentEmail { get; init; }

    /// <summary>
    /// Optional proof-of-payment mobile.
    /// </summary>
    public string? ProofOfPaymentMobile { get; init; }

    /// <summary>
    /// Optional proof-of-payment indicator.
    /// </summary>
    public int? ProofOfPaymentIndicator { get; init; }

    /// <summary>
    /// Optional per-payment callback URI (overrides bank default).
    /// </summary>
    public string? CallbackUri { get; init; }

    /// <summary>
    /// Optional per-payment callback token (overrides bank default).
    /// </summary>
    public string? CallbackToken { get; init; }

    /// <summary>
    /// Optional per-payment callback support email (overrides bank default).
    /// </summary>
    public string? CallbackSupportEmail { get; init; }
}
