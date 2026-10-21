using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Index("CreatedAtUtc", Name = "IX_GpayAvsBatches_CreatedAtUtc")]
[Index("ExternalBatchId", Name = "IX_GpayAvsBatches_ExternalBatchId")]
public partial class GpayAvsBatch : Entity<Guid>
{
    [Key]
    public override Guid Id { get; protected set; }

    [StringLength(64)]
    public string? ExternalBatchId { get; set; }

    [StringLength(128)]
    public string? Reference { get; set; }

    [StringLength(260)]
    public string? SourceFileName { get; set; }

    [StringLength(64)]
    public string CorrelationId { get; set; } = null!;

    [StringLength(32)]
    public string Status { get; set; } = null!;

    public int TotalItems { get; set; }

    public int ProcessedCount { get; set; }

    public int SucceededCount { get; set; }

    public int FailedCount { get; set; }

    public int PendingCount { get; set; }

    public int SegmentCount { get; set; }

    public DateTimeOffset? StartedAtUtc { get; set; }

    public DateTimeOffset? CompletedAtUtc { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }

    public DateTimeOffset? UpdatedAtUtc { get; set; }

    [InverseProperty("Batch")]
    public virtual ICollection<GpayAvsBatchItem> GpayAvsBatchItems { get; set; } = new List<GpayAvsBatchItem>();

    [InverseProperty("Batch")]
    public virtual ICollection<GpayAvsBatchSegment> GpayAvsBatchSegments { get; set; } = new List<GpayAvsBatchSegment>();
}


