using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("BatchBeneficiaryFile")]
public partial class BatchBeneficiaryFile : Entity<int>
{
    [Key]
    [Column("PkBatchFileId")]
    public override int Id { get; protected set; }

    public Guid FkEntityId { get; set; }

    public Guid FkUploadUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UploadDate { get; set; }

    [Unicode(false)]
    public string? OriginalFileName { get; set; }

    [Unicode(false)]
    public string? FileName { get; set; }

    [Unicode(false)]
    public string? FilePath { get; set; }

    public bool? Active { get; set; }

    [ForeignKey("FkEntityId")]
    [InverseProperty("BatchBeneficiaryFiles")]
    public virtual CustomEntity FkEntity { get; set; } = null!;

    [ForeignKey("FkUploadUserId")]
    [InverseProperty("BatchBeneficiaryFiles")]
    public virtual User FkUploadUser { get; set; } = null!;
}


