using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("AccountCharge")]
public partial class AccountCharge : Entity<Guid>
{
    [Key]
    [Column("PkAccountChargeID")]
    public override Guid Id { get; protected set; }

    [Column("FKAccountID")]
    public Guid FkaccountId { get; set; }

    [Column("FKEntityAccountID")]
    public Guid FkentityAccountId { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string ChargeType { get; set; } = null!;

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Value { get; set; }

    [StringLength(50)]
    public string Reference { get; set; } = null!;

    public int Sequence { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column("TransactionCodeID")]
    public int TransactionCodeId { get; set; }

    public bool? ApplyOnDebit { get; set; }

    public bool? Active { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ChargeStartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ChargeExpiryDate { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? ChargeExpiryAmount { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? ChargeAccumulatedAmount { get; set; }

    [InverseProperty("FkAccountCharge")]
    public virtual ICollection<AccountChargeAccumulatedAmountLog> AccountChargeAccumulatedAmountLogs { get; set; } = new List<AccountChargeAccumulatedAmountLog>();

    [InverseProperty("FkAccountCharge")]
    public virtual ICollection<AccountChargeExludedEntity> AccountChargeExludedEntities { get; set; } = new List<AccountChargeExludedEntity>();

    [InverseProperty("FkAccountCharge")]
    public virtual ICollection<AccountChargeExludedEntityLog> AccountChargeExludedEntityLogs { get; set; } = new List<AccountChargeExludedEntityLog>();

    [InverseProperty("FkAccountCharge")]
    public virtual ICollection<AccountChargeLog> AccountChargeLogs { get; set; } = new List<AccountChargeLog>();

    [InverseProperty("FkAccountCharge")]
    public virtual ICollection<AccountChargeMapping> AccountChargeMappings { get; set; } = new List<AccountChargeMapping>();

    [InverseProperty("FkAccountCharge")]
    public virtual ICollection<AccountChargeReversalLog> AccountChargeReversalLogs { get; set; } = new List<AccountChargeReversalLog>();

    [InverseProperty("FkAccountCharge")]
    public virtual ICollection<AccountChargeReversal> AccountChargeReversals { get; set; } = new List<AccountChargeReversal>();

    [ForeignKey("FkaccountId")]
    [InverseProperty("AccountCharges")]
    public virtual Account Fkaccount { get; set; } = null!;

    [ForeignKey("TransactionCodeId")]
    [InverseProperty("AccountCharges")]
    public virtual TransactionCode TransactionCode { get; set; } = null!;
}


