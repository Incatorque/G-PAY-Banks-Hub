using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Index("BatchId", Name = "IX_GpayAvsBatchItems_BatchId")]
[Index("BatchId", "RowNumber", Name = "IX_GpayAvsBatchItems_Batch_Row")]
[Index("ExternalRecordId", Name = "IX_GpayAvsBatchItems_ExternalRecordId")]
[Index("SegmentId", Name = "IX_GpayAvsBatchItems_SegmentId")]
public partial class GpayAvsBatchItem : Entity<Guid>
{
    [Key]
    public override Guid Id { get; protected set; }

    public Guid BatchId { get; set; }

    public Guid SegmentId { get; set; }

    [StringLength(64)]
    public string? ExternalRecordId { get; set; }

    public int RowNumber { get; set; }

    [StringLength(32)]
    public string BankCode { get; set; } = null!;

    [StringLength(32)]
    public string Status { get; set; } = null!;

    public string RequestJson { get; set; } = null!;

    public string? ResponseJson { get; set; }

    [StringLength(64)]
    public string? ErrorCode { get; set; }

    [StringLength(1024)]
    public string? ErrorMessage { get; set; }

    [StringLength(128)]
    public string? BankReference { get; set; }

    public DateTimeOffset? ProcessedAtUtc { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }

    public DateTimeOffset? UpdatedAtUtc { get; set; }

    [ForeignKey("BatchId")]
    [InverseProperty("GpayAvsBatchItems")]
    public virtual GpayAvsBatch Batch { get; set; } = null!;

    [ForeignKey("SegmentId")]
    [InverseProperty("GpayAvsBatchItems")]
    public virtual GpayAvsBatchSegment Segment { get; set; } = null!;
}


