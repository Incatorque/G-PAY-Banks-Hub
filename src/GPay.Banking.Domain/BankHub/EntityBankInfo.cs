using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityBankInfo")]
public partial class EntityBankInfo : Entity<Guid>
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

    public bool? FacilityAccount { get; set; }

    [ForeignKey("FkAccountTypeId")]
    [InverseProperty("EntityBankInfos")]
    public virtual AccountType FkAccountType { get; set; } = null!;

    [ForeignKey("FkBankId")]
    [InverseProperty("EntityBankInfos")]
    public virtual Bank FkBank { get; set; } = null!;

    [ForeignKey("FkEntityId")]
    [InverseProperty("EntityBankInfos")]
    public virtual CustomEntity FkEntity { get; set; } = null!;
}


