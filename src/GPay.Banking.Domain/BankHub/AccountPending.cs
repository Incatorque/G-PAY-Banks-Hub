using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("AccountPending")]
public partial class AccountPending : Entity<Guid>
{
    [Key]
    [Column("PkAccountPendingID")]
    public override Guid Id { get; protected set; }

    [Column("FkBankID")]
    public Guid FkBankId { get; set; }

    [Column("FkAccountTypeID")]
    public Guid FkAccountTypeId { get; set; }

    [StringLength(30)]
    public string AccountNumber { get; set; } = null!;

    [StringLength(50)]
    public string BranchName { get; set; } = null!;

    [StringLength(20)]
    public string BranchCode { get; set; } = null!;

    [StringLength(20)]
    public string? ResourceType { get; set; }

    [Column("FkEntityID")]
    public Guid? FkEntityId { get; set; }

    public bool? Primarary { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? AccountLabel { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BankUserCode { get; set; }

    [Column("FkAccountStatusID")]
    public Guid? FkAccountStatusId { get; set; }

    public bool? MapAccount { get; set; }

    [Column("FkControllingEntityID")]
    public Guid? FkControllingEntityId { get; set; }

    [Column("CountryCurrencyID")]
    public int? CountryCurrencyId { get; set; }

    public bool? ToDelete { get; set; }

    [ForeignKey("FkAccountTypeId")]
    [InverseProperty("AccountPendings")]
    public virtual AccountType FkAccountType { get; set; } = null!;

    [ForeignKey("FkBankId")]
    [InverseProperty("AccountPendings")]
    public virtual Bank FkBank { get; set; } = null!;
}


