namespace GPay.Banking.BankHub;

/// <summary>
/// Normalized AVS result returned to GPay callers after a single verification.
/// </summary>
/// <remarks>
/// Match string fields use bank conventions: typically <c>Y</c>/<c>Yes</c>, <c>N</c>/<c>No</c>,
/// or <c>U</c>/<c>Unverified</c> when the bank could not evaluate the criterion.
/// </remarks>
public class AccountVerificationResultDto
{
    /// <summary>
    /// Persisted <c>BankHubAvsRecord</c> id for audit / dashboard lookup.
    /// </summary>
    public Guid? RecordId { get; set; }

    /// <summary>
    /// <c>true</c> when the account exists, is open/active, and required identity/name checks passed.
    /// </summary>
    public bool IsVerified { get; set; }

    /// <summary>Whether the account was found at the bank.</summary>
    public bool? AccountFound { get; set; }

    /// <summary>Whether the account is open.</summary>
    public bool? AccountOpen { get; set; }

    /// <summary>Whether the account is active.</summary>
    public bool? AccountActive { get; set; }

    /// <summary>Identity number match: Yes / No / Unverified (or Y / N / U).</summary>
    public string? IdentityMatch { get; set; }

    /// <summary>Name match: Yes / No / Unverified.</summary>
    public string? NameMatch { get; set; }

    /// <summary>Initials match (Absa AVS v00.5 ValueList): Yes / No / Unverified.</summary>
    public string? InitialsMatch { get; set; }

    /// <summary>Email match: Yes / No / Unverified.</summary>
    public string? EmailMatch { get; set; }

    /// <summary>Phone match: Yes / No / Unverified.</summary>
    public string? PhoneMatch { get; set; }

    /// <summary>Account type match: Yes / No / Unverified.</summary>
    public string? AccountTypeMatch { get; set; }

    /// <summary>Whether the account has been open longer than three months (when bank supplies it).</summary>
    public bool? AccountOpenLongerThan3Months { get; set; }

    /// <summary>Whether the account allows credits.</summary>
    public bool? AllowsCredit { get; set; }

    /// <summary>Whether the account accepts credits.</summary>
    public bool? AcceptsCredit { get; set; }

    /// <summary>Whether the account allows debits.</summary>
    public bool? AllowsDebit { get; set; }

    /// <summary>Whether the account accepts debits.</summary>
    public bool? AcceptsDebit { get; set; }

    /// <summary>Normalized GPay / connector result code.</summary>
    public string? ResultCode { get; set; }

    /// <summary>Human-readable result description.</summary>
    public string? ResultDescription { get; set; }

    /// <summary>Bank-native result code prior to mapping (e.g. Absa Status).</summary>
    public string? BankResultCode { get; set; }

    /// <summary>Bank-allocated enquiry reference (Absa ReferenceNumber / AVS-R).</summary>
    public string? BankReference { get; set; }

    /// <summary>Echo of the client reference when supplied on the request.</summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Heuristic success percentage from known match flags (0–100).
    /// Useful for batch scoring; not a bank-official score.
    /// </summary>
    public decimal SuccessRate { get; set; }

    /// <summary>Raw bank matching criteria map (e.g. Absa ValueList Key→Value).</summary>
    public Dictionary<string, string>? MatchingCriteria { get; set; }

    /// <summary>Server-allocated correlation id for this bank call (tracing / support).</summary>
    public string? CorrelationId { get; set; }

    /// <summary>Error detail when <see cref="Success"/> is <c>false</c>.</summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Absa catalog entry for the bank code on this call. Null when Absa did not return an error code.
    /// </summary>
    public AbsaErrorInfo? AbsaError { get; set; }

    /// <summary>
    /// <c>true</c> when the bank call completed and returned data (does not imply <see cref="IsVerified"/>).
    /// </summary>
    public bool Success { get; set; }
}
