using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("TransactionCode")]
public partial class TransactionCode : Entity<int>
{
    [Key]
    [Column("ID")]
    public override int Id { get; protected set; }

    [StringLength(30)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public bool? ApplyOnDebit { get; set; }

    public bool? AppliesCharges { get; set; }

    [InverseProperty("TransactionCode")]
    public virtual ICollection<AccountCharge> AccountCharges { get; set; } = new List<AccountCharge>();

    [InverseProperty("FktransactionCode")]
    public virtual ICollection<DashboardTransactionCodeLink> DashboardTransactionCodeLinks { get; set; } = new List<DashboardTransactionCodeLink>();

    [InverseProperty("TransactionCode")]
    public virtual ICollection<EntityAccountCharge> EntityAccountCharges { get; set; } = new List<EntityAccountCharge>();

    [InverseProperty("TransactionCode")]
    public virtual ICollection<EntityBankStatement> EntityBankStatements { get; set; } = new List<EntityBankStatement>();

    [InverseProperty("TransactionCode")]
    public virtual ICollection<TransactionCodeMap> TransactionCodeMaps { get; set; } = new List<TransactionCodeMap>();
}


