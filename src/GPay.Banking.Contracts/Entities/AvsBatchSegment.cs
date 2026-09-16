using GPay.Banking.Contracts.Enums;

namespace GPay.Banking.Contracts.Entities;

/// <summary>
/// Bank-homogeneous chunk of an AVS batch (≤ 5 000 items).
/// </summary>
public sealed class AvsBatchSegment : EntityBase
{
    public Guid BatchId { get; set; }

    public AvsBatch? Batch { get; set; }

    public BankCode BankCode { get; set; }

    public int SegmentIndex { get; set; }

    public int ItemCount { get; set; }

    public int ProcessedCount { get; set; }

    public AvsBatchStatus Status { get; set; } = AvsBatchStatus.Queued;

    public DateTimeOffset? QueuedAtUtc { get; set; }

    public DateTimeOffset? StartedAtUtc { get; set; }

    public DateTimeOffset? CompletedAtUtc { get; set; }

    public ICollection<AvsBatchItem> Items { get; set; } = new List<AvsBatchItem>();
}
