using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class TransactionCode
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public bool? ApplyOnDebit { get; set; }

    public bool? AppliesCharges { get; set; }

    public virtual ICollection<AccountCharge> AccountCharges { get; set; } = new List<AccountCharge>();

    public virtual ICollection<DashboardTransactionCodeLink> DashboardTransactionCodeLinks { get; set; } = new List<DashboardTransactionCodeLink>();

    public virtual ICollection<EntityAccountCharge> EntityAccountCharges { get; set; } = new List<EntityAccountCharge>();

    public virtual ICollection<EntityBankStatement> EntityBankStatements { get; set; } = new List<EntityBankStatement>();

    public virtual ICollection<TransactionCodeMap> TransactionCodeMaps { get; set; } = new List<TransactionCodeMap>();
}
