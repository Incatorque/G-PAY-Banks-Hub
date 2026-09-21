namespace GPay.Banking.BankHub;

public class PaymentResultDto
{
    public Guid? RecordId { get; set; }
    public string? Status { get; set; }
    public string? Reference { get; set; }
    public decimal? Amount { get; set; }
    public string? Currency { get; set; }
    public string? ApiReference { get; set; }
    public string? TransactionReference { get; set; }
    public string? BankStatusCode { get; set; }
    public string? PaymentRail { get; set; }
    public string? ResultDescription { get; set; }
    public string? CorrelationId { get; set; }
    public string? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
    public bool Success { get; set; }
}
