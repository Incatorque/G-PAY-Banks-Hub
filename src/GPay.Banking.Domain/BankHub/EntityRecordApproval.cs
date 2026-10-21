using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityRecordApproval")]
public partial class EntityRecordApproval : Entity<Guid>
{
    [Key]
    [Column("PKEntityRecordApprovalId")]
    public override Guid Id { get; protected set; }

    [Column("FKEntityId")]
    public Guid FkentityId { get; set; }

    [Column("FKUserId")]
    public Guid FkuserId { get; set; }

    public Guid FkApprovalStatusTypeId { get; set; }

    public int Order { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ApprovalDate { get; set; }

    public bool? Active { get; set; }

    [Column("FKEntityDataApprovalId")]
    public Guid FkentityDataApprovalId { get; set; }

    [ForeignKey("FkApprovalStatusTypeId")]
    [InverseProperty("EntityRecordApprovals")]
    public virtual ApprovalStatusType FkApprovalStatusType { get; set; } = null!;

    [ForeignKey("FkentityId")]
    [InverseProperty("EntityRecordApprovals")]
    public virtual CustomEntity Fkentity { get; set; } = null!;

    [ForeignKey("FkentityDataApprovalId")]
    [InverseProperty("EntityRecordApprovals")]
    public virtual EntityDataApproval FkentityDataApproval { get; set; } = null!;

    [ForeignKey("FkuserId")]
    [InverseProperty("EntityRecordApprovals")]
    public virtual User Fkuser { get; set; } = null!;
}


