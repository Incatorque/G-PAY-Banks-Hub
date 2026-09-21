using Volo.Abp.Domain.Entities.Auditing;

namespace GPay.Banking.BankHub;

public class BankHubAvsRecord : AuditedAggregateRoot<Guid>
{
    public Guid? BatchId { get; set; }
    public Guid? ApiClientId { get; set; }
    public int? RowNumber { get; set; }
    public string BankCode { get; set; } = "Absa";
    public string? Reference { get; set; }
    public string? CorrelationId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public string BranchCode { get; set; } = string.Empty;
    public string? IdentityNumber { get; set; }
    public string? AccountHolderName { get; set; }
    public bool IsVerified { get; set; }
    public string? ResultCode { get; set; }
    public string? ResultDescription { get; set; }
    public string? BankResultCode { get; set; }
    public string? BankReference { get; set; }
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
    public decimal SuccessRate { get; set; }
    public string? MatchingCriteriaJson { get; set; }
    public string? RequestHeadersJson { get; set; }
    public string? RequestBodyJson { get; set; }
    public string? ResponseHeadersJson { get; set; }
    public string? ResponseBodyJson { get; set; }
    public string? ErrorMessage { get; set; }
    public string Status { get; set; } = "Queued";
}
