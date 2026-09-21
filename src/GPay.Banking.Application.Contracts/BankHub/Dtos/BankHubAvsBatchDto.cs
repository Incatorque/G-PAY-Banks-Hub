namespace GPay.Banking.BankHub;

public class BankHubAvsBatchDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public int TotalRows { get; set; }
    public int ProcessedCount { get; set; }
    public int VerifiedCount { get; set; }
    public int FailedCount { get; set; }
    public int PendingCount { get; set; }
    public int SegmentCount { get; set; }
    public double PercentComplete { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? BankCode { get; set; }
    public Guid? ApiClientId { get; set; }
    public DateTime CreationTime { get; set; }
}
