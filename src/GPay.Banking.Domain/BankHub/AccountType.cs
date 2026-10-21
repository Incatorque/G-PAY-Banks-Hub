using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("AccountType")]
public partial class AccountType : Entity<Guid>
{
    [Key]
    [Column("PkAccountTypeID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("H2HRef")]
    public int? H2href { get; set; }

    [Column("FNBH2HRef")]
    public int? Fnbh2href { get; set; }

    public bool? Active { get; set; }

    [InverseProperty("FkAccountType")]
    public virtual ICollection<AccountPending> AccountPendings { get; set; } = new List<AccountPending>();

    [InverseProperty("FkAccountType")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [InverseProperty("FkAccountType")]
    public virtual ICollection<EntityBankInfoPending> EntityBankInfoPendings { get; set; } = new List<EntityBankInfoPending>();

    [InverseProperty("FkAccountType")]
    public virtual ICollection<EntityBankInfo> EntityBankInfos { get; set; } = new List<EntityBankInfo>();
}


