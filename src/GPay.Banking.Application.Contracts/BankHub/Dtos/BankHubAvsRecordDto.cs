namespace GPay.Banking.BankHub;

public class BankHubAvsRecordDto
{
    public Guid Id { get; set; }
    public Guid? BatchId { get; set; }
    public int? RowNumber { get; set; }
    public string BankCode { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public string BranchCode { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public string? ResultCode { get; set; }
    public string? ResultDescription { get; set; }
    public decimal SuccessRate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; }
    public string? CorrelationId { get; set; }
}
