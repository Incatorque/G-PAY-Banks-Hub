namespace GPay.Banking.BankHub;

public sealed class BankApiCallDto
{
    public Guid Id { get; set; }
    public DateTime CreationTime { get; set; }
    public string BankCode { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
    public string Operation { get; set; } = string.Empty;
    public string? HttpMethod { get; set; }
    public string? Path { get; set; }
    public int? HttpStatus { get; set; }
    public string? CorrelationId { get; set; }
    public Guid? GpayOrderId { get; set; }
    public long? DurationMs { get; set; }
    public bool Success { get; set; }
    public string? ErrorCode { get; set; }
    public string? RequestJson { get; set; }
    public string? ResponseJson { get; set; }
}
