using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityBankInfoPending")]
public partial class EntityBankInfoPending : Entity<Guid>
{
    [Key]
    [Column("PkBankInfoID")]
    public override Guid Id { get; protected set; }

    [Column("FkEntityID")]
    public Guid FkEntityId { get; set; }

    [Column("FkBankID")]
    public Guid FkBankId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string AccountNo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? BranchCode { get; set; }

    [Column("FkAccountTypeID")]
    public Guid FkAccountTypeId { get; set; }

    public bool? Primarary { get; set; }

    public bool? ProcurementAccount { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BranchName { get; set; }

    public bool? ToDelete { get; set; }

    [ForeignKey("FkAccountTypeId")]
    [InverseProperty("EntityBankInfoPendings")]
    public virtual AccountType FkAccountType { get; set; } = null!;

    [ForeignKey("FkBankId")]
    [InverseProperty("EntityBankInfoPendings")]
    public virtual Bank FkBank { get; set; } = null!;

    [ForeignKey("FkEntityId")]
    [InverseProperty("EntityBankInfoPendings")]
    public virtual EntityPending FkEntity { get; set; } = null!;
}


