namespace GPay.Banking.Contracts.Dtos.AccountVerification;

/// <summary>
/// Bank-agnostic GPay account verification (AVS) response.
/// </summary>
public sealed class AccountVerificationResponse
{
    /// <summary>
    /// True when the account exists, is open/active, and required identity/name checks passed.
    /// </summary>
    public bool IsVerified { get; init; }

    /// <summary>
    /// Whether the account was found at the bank.
    /// </summary>
    public bool? AccountFound { get; init; }

    /// <summary>
    /// Whether the account is open.
    /// </summary>
    public bool? AccountOpen { get; init; }

    /// <summary>
    /// Whether the account is active.
    /// </summary>
    public bool? AccountActive { get; init; }

    /// <summary>
    /// Identity number match result: Y, N, or U (unavailable).
    /// </summary>
    public string? IdentityMatch { get; init; }

    /// <summary>
    /// Name match result: Y, N, or U.
    /// </summary>
    public string? NameMatch { get; init; }

    /// <summary>
    /// Initials match result: Y, N, or U (Absa AVS v00.5 ValueList).
    /// </summary>
    public string? InitialsMatch { get; init; }

    /// <summary>
    /// Email match result: Y, N, or U.
    /// </summary>
    public string? EmailMatch { get; init; }

    /// <summary>
    /// Phone match result: Y, N, or U.
    /// </summary>
    public string? PhoneMatch { get; init; }

    /// <summary>
    /// Account type match result: Y, N, or U.
    /// </summary>
    public string? AccountTypeMatch { get; init; }

    /// <summary>
    /// Account holder name returned by the bank when available.
    /// </summary>
    public string? AccountHolderName { get; init; }

    /// <summary>
    /// Account status returned by the bank (e.g. ACTIVE).
    /// </summary>
    public string? AccountStatus { get; init; }

    /// <summary>
    /// Whether the account has been open longer than 3 months (Absa AVS ValueList).
    /// </summary>
    public bool? AccountOpenLongerThan3Months { get; init; }

    /// <summary>
    /// Whether the account allows credits.
    /// </summary>
    public bool? AllowsCredit { get; init; }

    /// <summary>
    /// Whether the account accepts credits.
    /// </summary>
    public bool? AcceptsCredit { get; init; }

    /// <summary>
    /// Whether the account allows debits.
    /// </summary>
    public bool? AllowsDebit { get; init; }

    /// <summary>
    /// Whether the account accepts debits.
    /// </summary>
    public bool? AcceptsDebit { get; init; }

    /// <summary>
    /// Normalized GPay result code.
    /// </summary>
    public string? ResultCode { get; init; }

    /// <summary>
    /// Human-readable result description.
    /// </summary>
    public string? ResultDescription { get; init; }

    /// <summary>
    /// Bank-native result code prior to mapping (Absa Status).
    /// </summary>
    public string? BankResultCode { get; init; }

    /// <summary>
    /// Bank-allocated enquiry reference (Absa ReferenceNumber / AVS-R).
    /// </summary>
    public string? BankReference { get; init; }

    /// <summary>
    /// Echo of the client reference when supplied.
    /// </summary>
    public string? Reference { get; init; }

    /// <summary>
    /// Raw matching criteria from the bank (Absa ValueList Key→Value).
    /// </summary>
    public Dictionary<string, string>? MatchingCriteria { get; init; }
}
