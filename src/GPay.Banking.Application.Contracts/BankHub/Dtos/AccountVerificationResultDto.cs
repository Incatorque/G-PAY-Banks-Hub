namespace GPay.Banking.BankHub;

public class AccountVerificationResultDto
{
    public Guid? RecordId { get; set; }
    public bool IsVerified { get; set; }
    public bool? AccountFound { get; set; }
    public bool? AccountOpen { get; set; }
    public bool? AccountActive { get; set; }
    public string? IdentityMatch { get; set; }
    public string? NameMatch { get; set; }
    public string? InitialsMatch { get; set; }
    public string? EmailMatch { get; set; }
    public string? PhoneMatch { get; set; }
    public string? AccountTypeMatch { get; set; }
    public bool? AccountOpenLongerThan3Months { get; set; }
    public bool? AllowsCredit { get; set; }
    public bool? AcceptsCredit { get; set; }
    public bool? AllowsDebit { get; set; }
    public bool? AcceptsDebit { get; set; }
    public string? ResultCode { get; set; }
    public string? ResultDescription { get; set; }
    public string? BankResultCode { get; set; }
    public string? BankReference { get; set; }
    public string? Reference { get; set; }
    public decimal SuccessRate { get; set; }
    public Dictionary<string, string>? MatchingCriteria { get; set; }
    public string? CorrelationId { get; set; }
    public string? ErrorMessage { get; set; }
    public bool Success { get; set; }
}
