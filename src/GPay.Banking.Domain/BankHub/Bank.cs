using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Bank")]
public partial class Bank : Entity<Guid>
{
    [Key]
    [Column("PkBankID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    [StringLength(20)]
    public string? UniversalCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SwiftCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Currency { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? PostalCode { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Country { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? AddressLine1 { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? AddressLine2 { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? City { get; set; }

    [InverseProperty("FkBank")]
    public virtual ICollection<AccountPending> AccountPendings { get; set; } = new List<AccountPending>();

    [InverseProperty("FkBank")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [InverseProperty("FkBank")]
    public virtual BankChannel? BankChannel { get; set; }

    [InverseProperty("FkBank")]
    public virtual ICollection<BankTime> BankTimes { get; set; } = new List<BankTime>();

    [InverseProperty("Bank")]
    public virtual ICollection<CountryBank> CountryBanks { get; set; } = new List<CountryBank>();

    [InverseProperty("FkBank")]
    public virtual ICollection<EntityBankInfoPending> EntityBankInfoPendings { get; set; } = new List<EntityBankInfoPending>();

    [InverseProperty("FkBank")]
    public virtual ICollection<EntityBankInfo> EntityBankInfos { get; set; } = new List<EntityBankInfo>();
}


