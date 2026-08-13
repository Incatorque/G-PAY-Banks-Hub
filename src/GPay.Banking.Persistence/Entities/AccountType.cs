using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AccountType
{
    public Guid PkAccountTypeId { get; set; }

    public string Name { get; set; } = null!;

    public int? H2href { get; set; }

    public int? Fnbh2href { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<AccountPending> AccountPendings { get; set; } = new List<AccountPending>();

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<EntityBankInfoPending> EntityBankInfoPendings { get; set; } = new List<EntityBankInfoPending>();

    public virtual ICollection<EntityBankInfo> EntityBankInfos { get; set; } = new List<EntityBankInfo>();
}
