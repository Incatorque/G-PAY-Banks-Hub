namespace GPay.Banking.BankHub;

public class PaymentRecordDto
{
    public Guid Id { get; set; }
    public string BankCode { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? Reference { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "ZAR";
    public string? TransactionReference { get; set; }
    public string? ApiReference { get; set; }
    public DateTime CreationTime { get; set; }
}
