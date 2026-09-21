using GPay.Banking.Contracts.Enums;

namespace GPay.Banking.Contracts.Entities;

/// <summary>
/// Orchestrator-owned AVS batch header (queue/operational state).
/// </summary>
public sealed class AvsBatch : EntityBase
{
    public string? ExternalBatchId { get; set; }

    public string? Reference { get; set; }

    public string? SourceFileName { get; set; }

    public string CorrelationId { get; set; } = string.Empty;

    public AvsBatchStatus Status { get; set; } = AvsBatchStatus.Queued;

    public int TotalItems { get; set; }

    public int ProcessedCount { get; set; }

    public int SucceededCount { get; set; }

    public int FailedCount { get; set; }

    public int PendingCount { get; set; }

    public int SegmentCount { get; set; }

    public DateTimeOffset? StartedAtUtc { get; set; }

    public DateTimeOffset? CompletedAtUtc { get; set; }

    public ICollection<AvsBatchSegment> Segments { get; set; } = new List<AvsBatchSegment>();

    public ICollection<AvsBatchItem> Items { get; set; } = new List<AvsBatchItem>();
}
