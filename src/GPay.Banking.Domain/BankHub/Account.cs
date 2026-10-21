using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Account")]
public partial class Account : Entity<Guid>
{
    [Key]
    [Column("PkAccountID")]
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

    public bool? Active { get; set; }

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

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    public bool? HasFacility { get; set; }

    [Column(TypeName = "money")]
    public decimal? FacilityValue { get; set; }

    public bool? HasEntityAccountFacility { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SwiftCode { get; set; }

    [Column("Is3PIM")]
    public bool? Is3Pim { get; set; }

    [InverseProperty("Fkaccount")]
    public virtual ICollection<AccountCharge> AccountCharges { get; set; } = new List<AccountCharge>();

    [InverseProperty("FkAccount")]
    public virtual ICollection<DashboardConfig> DashboardConfigs { get; set; } = new List<DashboardConfig>();

    [InverseProperty("FkAccount")]
    public virtual ICollection<EntityAccount> EntityAccounts { get; set; } = new List<EntityAccount>();

    [InverseProperty("FkAccount")]
    public virtual ICollection<EntityBankStatement> EntityBankStatements { get; set; } = new List<EntityBankStatement>();

    [InverseProperty("FkAccount")]
    public virtual ICollection<EntityConfigAccount> EntityConfigAccounts { get; set; } = new List<EntityConfigAccount>();

    [InverseProperty("FkAccount")]
    public virtual ICollection<EntityConfig> EntityConfigs { get; set; } = new List<EntityConfig>();

    [InverseProperty("FkAccount")]
    public virtual ICollection<EntityOrderSweep> EntityOrderSweeps { get; set; } = new List<EntityOrderSweep>();

    [ForeignKey("FkAccountStatusId")]
    [InverseProperty("Accounts")]
    public virtual AccountStatus? FkAccountStatus { get; set; }

    [ForeignKey("FkAccountTypeId")]
    [InverseProperty("Accounts")]
    public virtual AccountType FkAccountType { get; set; } = null!;

    [ForeignKey("FkBankId")]
    [InverseProperty("Accounts")]
    public virtual Bank FkBank { get; set; } = null!;

    [ForeignKey("FkEntityId")]
    [InverseProperty("Accounts")]
    public virtual CustomEntity? FkEntity { get; set; }

    [InverseProperty("FkFromAccount")]
    public virtual ICollection<Order> OrderFkFromAccounts { get; set; } = new List<Order>();

    [InverseProperty("FkToAccount")]
    public virtual ICollection<Order> OrderFkToAccounts { get; set; } = new List<Order>();

    [InverseProperty("FkClientAccount")]
    public virtual ICollection<SupplierClientAccount> SupplierClientAccountFkClientAccounts { get; set; } = new List<SupplierClientAccount>();

    [InverseProperty("FkSupplierAccount")]
    public virtual ICollection<SupplierClientAccount> SupplierClientAccountFkSupplierAccounts { get; set; } = new List<SupplierClientAccount>();

    [InverseProperty("FkAccount")]
    public virtual ICollection<UserSelection> UserSelections { get; set; } = new List<UserSelection>();
}


