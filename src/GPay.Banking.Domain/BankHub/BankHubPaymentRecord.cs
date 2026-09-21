using Volo.Abp.Domain.Entities.Auditing;

namespace GPay.Banking.BankHub;

public class BankHubPaymentRecord : AuditedAggregateRoot<Guid>
{
    public string BankCode { get; set; } = "Absa";
    public string? CorrelationId { get; set; }
    public string? Reference { get; set; }
    public string? TransactionReference { get; set; }
    public string? ApiReference { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "ZAR";
    public string PaymentRail { get; set; } = "RPP";
    public string Status { get; set; } = "Submitted";
    public string? BankStatusCode { get; set; }
    public string FromAccountNumber { get; set; } = string.Empty;
    public string ToAccountNumber { get; set; } = string.Empty;
    public string? ToBranchCode { get; set; }
    public string? BeneficiaryName { get; set; }
    public string? ResultDescription { get; set; }
    public string? RequestJson { get; set; }
    public string? ResponseJson { get; set; }
    public string? ErrorMessage { get; set; }
}
