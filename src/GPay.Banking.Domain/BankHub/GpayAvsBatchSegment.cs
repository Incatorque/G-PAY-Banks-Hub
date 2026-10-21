using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Index("BatchId", "SegmentIndex", Name = "IX_GpayAvsBatchSegments_Batch_Index", IsUnique = true)]
public partial class GpayAvsBatchSegment : Entity<Guid>
{
    [Key]
    public override Guid Id { get; protected set; }

    public Guid BatchId { get; set; }

    [StringLength(32)]
    public string BankCode { get; set; } = null!;

    public int SegmentIndex { get; set; }

    public int ItemCount { get; set; }

    public int ProcessedCount { get; set; }

    [StringLength(32)]
    public string Status { get; set; } = null!;

    public DateTimeOffset? QueuedAtUtc { get; set; }

    public DateTimeOffset? StartedAtUtc { get; set; }

    public DateTimeOffset? CompletedAtUtc { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }

    public DateTimeOffset? UpdatedAtUtc { get; set; }

    [ForeignKey("BatchId")]
    [InverseProperty("GpayAvsBatchSegments")]
    public virtual GpayAvsBatch Batch { get; set; } = null!;

    [InverseProperty("Segment")]
    public virtual ICollection<GpayAvsBatchItem> GpayAvsBatchItems { get; set; } = new List<GpayAvsBatchItem>();
}


