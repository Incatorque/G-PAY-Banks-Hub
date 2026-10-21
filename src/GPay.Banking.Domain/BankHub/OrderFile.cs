using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OrderFile")]
public partial class OrderFile : Entity<int>
{
    [Key]
    [Column("PkOrderFileId")]
    public override int Id { get; protected set; }

    public Guid FkUploadUserId { get; set; }

    public Guid FkOrderId { get; set; }

    [Unicode(false)]
    public string? FileName { get; set; }

    [Unicode(false)]
    public string? FilePath { get; set; }

    [Unicode(false)]
    public string? Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UploadDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastUpdated { get; set; }

    public Guid FkLastUpdatedUserId { get; set; }

    public bool? Active { get; set; }

    [ForeignKey("FkLastUpdatedUserId")]
    [InverseProperty("OrderFileFkLastUpdatedUsers")]
    public virtual User FkLastUpdatedUser { get; set; } = null!;

    [ForeignKey("FkOrderId")]
    [InverseProperty("OrderFiles")]
    public virtual Order FkOrder { get; set; } = null!;

    [ForeignKey("FkUploadUserId")]
    [InverseProperty("OrderFileFkUploadUsers")]
    public virtual User FkUploadUser { get; set; } = null!;
}


