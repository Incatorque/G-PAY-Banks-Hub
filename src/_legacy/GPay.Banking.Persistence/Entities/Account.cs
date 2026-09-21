using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Account
{
    public Guid PkAccountId { get; set; }

    public Guid FkBankId { get; set; }

    public Guid FkAccountTypeId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string BranchName { get; set; } = null!;

    public string BranchCode { get; set; } = null!;

    public string? ResourceType { get; set; }

    public bool? Active { get; set; }

    public Guid? FkEntityId { get; set; }

    public bool? Primarary { get; set; }

    public string? AccountLabel { get; set; }

    public string? BankUserCode { get; set; }

    public Guid? FkAccountStatusId { get; set; }

    public bool? MapAccount { get; set; }

    public Guid? FkControllingEntityId { get; set; }

    public int? CountryCurrencyId { get; set; }

    public DateTime CreateDate { get; set; }

    public bool? HasFacility { get; set; }

    public decimal? FacilityValue { get; set; }

    public bool? HasEntityAccountFacility { get; set; }

    public string? SwiftCode { get; set; }

    public bool? Is3Pim { get; set; }

    public virtual ICollection<AccountCharge> AccountCharges { get; set; } = new List<AccountCharge>();

    public virtual ICollection<DashboardConfig> DashboardConfigs { get; set; } = new List<DashboardConfig>();

    public virtual ICollection<EntityAccount> EntityAccounts { get; set; } = new List<EntityAccount>();

    public virtual ICollection<EntityBankStatement> EntityBankStatements { get; set; } = new List<EntityBankStatement>();

    public virtual ICollection<EntityConfigAccount> EntityConfigAccounts { get; set; } = new List<EntityConfigAccount>();

    public virtual ICollection<EntityConfig> EntityConfigs { get; set; } = new List<EntityConfig>();

    public virtual ICollection<EntityOrderSweep> EntityOrderSweeps { get; set; } = new List<EntityOrderSweep>();

    public virtual AccountStatus? FkAccountStatus { get; set; }

    public virtual AccountType FkAccountType { get; set; } = null!;

    public virtual Bank FkBank { get; set; } = null!;

    public virtual Entity? FkEntity { get; set; }

    public virtual ICollection<Order> OrderFkFromAccounts { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderFkToAccounts { get; set; } = new List<Order>();

    public virtual ICollection<SupplierClientAccount> SupplierClientAccountFkClientAccounts { get; set; } = new List<SupplierClientAccount>();

    public virtual ICollection<SupplierClientAccount> SupplierClientAccountFkSupplierAccounts { get; set; } = new List<SupplierClientAccount>();

    public virtual ICollection<UserSelection> UserSelections { get; set; } = new List<UserSelection>();
}
