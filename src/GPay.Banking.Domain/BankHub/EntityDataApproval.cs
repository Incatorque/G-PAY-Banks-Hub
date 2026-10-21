using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityDataApproval")]
public partial class EntityDataApproval : Entity<Guid>
{
    [Key]
    [Column("PkEntityApprovalID")]
    public override Guid Id { get; protected set; }

    [Column("FkEntityID")]
    public Guid? FkEntityId { get; set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [Column("iOrder")]
    public int IOrder { get; set; }

    [Column("FkFromUserID")]
    public Guid? FkFromUserId { get; set; }

    public bool? New { get; set; }

    public bool? Edit { get; set; }

    public bool? Active { get; set; }

    [InverseProperty("FkentityDataApproval")]
    public virtual ICollection<EntityRecordApproval> EntityRecordApprovals { get; set; } = new List<EntityRecordApproval>();

    [ForeignKey("FkEntityId")]
    [InverseProperty("EntityDataApprovals")]
    public virtual CustomEntity? FkEntity { get; set; }

    [ForeignKey("FkFromUserId")]
    [InverseProperty("EntityDataApprovalFkFromUsers")]
    public virtual User? FkFromUser { get; set; }

    [ForeignKey("FkUserId")]
    [InverseProperty("EntityDataApprovalFkUsers")]
    public virtual User FkUser { get; set; } = null!;
}


