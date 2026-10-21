using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("RewardsUploadFile")]
public partial class RewardsUploadFile : Entity<int>
{
    [Key]
    [Column("PKRewardsUploadFileId")]
    public override int Id { get; protected set; }

    [StringLength(500)]
    [Unicode(false)]
    public string FileName { get; set; } = null!;

    [Unicode(false)]
    public string FilePath { get; set; } = null!;

    [Column("FKEntityId")]
    public Guid FkentityId { get; set; }

    [Column("FKUploadedUserId")]
    public Guid FkuploadedUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UploadDate { get; set; }

    public byte[] UploadedData { get; set; } = null!;

    public bool? Active { get; set; }

    [ForeignKey("FkentityId")]
    [InverseProperty("RewardsUploadFiles")]
    public virtual CustomEntity Fkentity { get; set; } = null!;

    [ForeignKey("FkuploadedUserId")]
    [InverseProperty("RewardsUploadFiles")]
    public virtual User FkuploadedUser { get; set; } = null!;
}


