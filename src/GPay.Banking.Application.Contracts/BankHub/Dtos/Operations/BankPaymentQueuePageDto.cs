namespace GPay.Banking.BankHub;

public sealed class BankPaymentQueuePageDto
{
    public int TotalCount { get; set; }
    public List<string> Banks { get; set; } = [];
    public List<BankPaymentQueueItemDto> Items { get; set; } = [];
}

public sealed class BankPaymentQueueItemDto
{
    public Guid OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public string Bank { get; set; } = string.Empty;
    public string Route { get; set; } = string.Empty;
    public string Rail { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
}
