using System.ComponentModel.DataAnnotations;
using GPay.Banking.Contracts.Enums;

namespace GPay.Banking.Contracts.Dtos.AccountVerification;

/// <summary>
/// Submit a mixed-bank AVS batch (max 20 000 items).
/// </summary>
public sealed class AvsBatchSubmitRequest
{
    /// <summary>Optional client/ABP batch id for reconciliation.</summary>
    public string? ExternalBatchId { get; init; }

    /// <summary>Optional client reference / file name.</summary>
    public string? Reference { get; init; }

    /// <summary>Original upload file name when submitted from ABP.</summary>
    public string? SourceFileName { get; init; }

    [Required]
    [MinLength(1)]
    public required IReadOnlyList<AvsBatchSubmitItem> Items { get; init; }
}

/// <summary>
/// One row in an AVS batch submit payload.
/// </summary>
public sealed class AvsBatchSubmitItem
{
    /// <summary>Optional client/ABP record id for reconciliation.</summary>
    public string? ExternalRecordId { get; init; }

    public int? RowNumber { get; init; }

    [Required]
    public required AccountVerificationRequest Request { get; init; }
}

/// <summary>
/// Response after accepting a batch (HTTP 202).
/// </summary>
public sealed class AvsBatchSubmitResponse
{
    public Guid BatchId { get; init; }

    public string? ExternalBatchId { get; init; }

    public int TotalItems { get; init; }

    public int SegmentCount { get; init; }

    public AvsBatchStatus Status { get; init; }

    public string CorrelationId { get; init; } = string.Empty;
}

/// <summary>
/// Batch progress snapshot for polling.
/// </summary>
public sealed class AvsBatchProgressDto
{
    public Guid BatchId { get; init; }

    public string? ExternalBatchId { get; init; }

    public string? Reference { get; init; }

    public string? SourceFileName { get; init; }

    public AvsBatchStatus Status { get; init; }

    public int TotalItems { get; init; }

    public int ProcessedCount { get; init; }

    public int SucceededCount { get; init; }

    public int FailedCount { get; init; }

    public int PendingCount { get; init; }

    public int SegmentCount { get; init; }

    public double PercentComplete { get; init; }

    public DateTimeOffset CreatedAtUtc { get; init; }

    public DateTimeOffset? StartedAtUtc { get; init; }

    public DateTimeOffset? CompletedAtUtc { get; init; }

    public IReadOnlyList<AvsBatchSegmentProgressDto> Segments { get; init; } = [];

    public IReadOnlyList<AvsBatchBankBreakdownDto> BankBreakdown { get; init; } = [];
}

/// <summary>
/// Per-segment progress.
/// </summary>
public sealed class AvsBatchSegmentProgressDto
{
    public Guid SegmentId { get; init; }

    public BankCode BankCode { get; init; }

    public int SegmentIndex { get; init; }

    public int ItemCount { get; init; }

    public AvsBatchStatus Status { get; init; }

    public int ProcessedCount { get; init; }

    public DateTimeOffset? StartedAtUtc { get; init; }

    public DateTimeOffset? CompletedAtUtc { get; init; }
}

/// <summary>
/// Per-bank rollup within a batch.
/// </summary>
public sealed class AvsBatchBankBreakdownDto
{
    public BankCode BankCode { get; init; }

    public int TotalItems { get; init; }

    public int ProcessedCount { get; init; }

    public int SucceededCount { get; init; }

    public int FailedCount { get; init; }

    public int PendingCount { get; init; }
}

/// <summary>
/// Single batch item result for paging / ABP sync.
/// </summary>
public sealed class AvsBatchItemDto
{
    public Guid ItemId { get; init; }

    public Guid BatchId { get; init; }

    public Guid SegmentId { get; init; }

    public string? ExternalRecordId { get; init; }

    public int RowNumber { get; init; }

    public BankCode BankCode { get; init; }

    public AvsBatchItemStatus Status { get; init; }

    public AccountVerificationResponse? Result { get; init; }

    public string? ErrorCode { get; init; }

    public string? ErrorMessage { get; init; }

    public string? BankReference { get; init; }

    public DateTimeOffset? ProcessedAtUtc { get; init; }

    public string? RequestJson { get; init; }

    public string? ResponseJson { get; init; }
}

/// <summary>
/// Paged item query.
/// </summary>
public sealed class AvsBatchItemQuery
{
    public AvsBatchItemStatus? Status { get; init; }

    public BankCode? BankCode { get; init; }

    /// <summary>When true, only items that have been processed (not Queued).</summary>
    public bool? ProcessedOnly { get; init; }

    public int Skip { get; init; }

    public int Take { get; init; } = 100;
}

/// <summary>
/// Paged items response.
/// </summary>
public sealed class AvsBatchItemPageDto
{
    public Guid BatchId { get; init; }

    public int TotalCount { get; init; }

    public IReadOnlyList<AvsBatchItemDto> Items { get; init; } = [];
}

/// <summary>
/// Shared batch size limits.
/// </summary>
public static class AvsBatchLimits
{
    public const int MaxItems = 20_000;

    public const int SegmentSize = 5_000;

    public const int DefaultDegreeOfParallelism = 5;
}
