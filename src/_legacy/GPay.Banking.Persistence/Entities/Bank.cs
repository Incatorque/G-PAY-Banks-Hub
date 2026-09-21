using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Bank
{
    public Guid PkBankId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public string? UniversalCode { get; set; }

    public string? SwiftCode { get; set; }

    public string? Currency { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public virtual ICollection<AccountPending> AccountPendings { get; set; } = new List<AccountPending>();

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<BankTime> BankTimes { get; set; } = new List<BankTime>();

    public virtual ICollection<CountryBank> CountryBanks { get; set; } = new List<CountryBank>();

    public virtual ICollection<EntityBankInfoPending> EntityBankInfoPendings { get; set; } = new List<EntityBankInfoPending>();

    public virtual ICollection<EntityBankInfo> EntityBankInfos { get; set; } = new List<EntityBankInfo>();
}
