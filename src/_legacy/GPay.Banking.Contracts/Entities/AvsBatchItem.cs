using GPay.Banking.Contracts.Enums;

namespace GPay.Banking.Contracts.Entities;

/// <summary>
/// One AVS verification line within a batch segment.
/// </summary>
public sealed class AvsBatchItem : EntityBase
{
    public Guid BatchId { get; set; }

    public AvsBatch? Batch { get; set; }

    public Guid SegmentId { get; set; }

    public AvsBatchSegment? Segment { get; set; }

    public string? ExternalRecordId { get; set; }

    public int RowNumber { get; set; }

    public BankCode BankCode { get; set; }

    public AvsBatchItemStatus Status { get; set; } = AvsBatchItemStatus.Queued;

    public string RequestJson { get; set; } = "{}";

    public string? ResponseJson { get; set; }

    public string? ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }

    public string? BankReference { get; set; }

    public DateTimeOffset? ProcessedAtUtc { get; set; }
}
