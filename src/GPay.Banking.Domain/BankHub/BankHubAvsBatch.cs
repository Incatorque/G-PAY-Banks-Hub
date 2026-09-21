using Volo.Abp.Domain.Entities.Auditing;

namespace GPay.Banking.BankHub;

public class BankHubAvsBatch : AuditedAggregateRoot<Guid>
{
    public string FileName { get; set; } = string.Empty;
    public int TotalRows { get; set; }
    public int ProcessedCount { get; set; }
    public int VerifiedCount { get; set; }
    public int FailedCount { get; set; }
    public int PendingCount { get; set; }
    public int SegmentCount { get; set; }
    public string Status { get; set; } = "Queued";
    public string? BankCode { get; set; }
    public Guid? ExternalBatchId { get; set; }
    public Guid? ApiClientId { get; set; }
}
