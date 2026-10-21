using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityAccountCharge")]
public partial class EntityAccountCharge : Entity<Guid>
{
    [Key]
    [Column("PkEntityAccountChargeID")]
    public override Guid Id { get; protected set; }

    [Column("FkEntityAccountID")]
    public Guid FkEntityAccountId { get; set; }

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

    [Column("MainEntityAccountID")]
    public Guid MainEntityAccountId { get; set; }

    [Column("TransactionCodeID")]
    public int TransactionCodeId { get; set; }

    public bool? ApplyOnDebit { get; set; }

    public bool? Active { get; set; }

    public bool? FilterOnRange { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? MaximumAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? MinimumAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ChargeStartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ChargeExpiryDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ChargeExpiryAmount { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? ChargeAccumulatedAmount { get; set; }

    [InverseProperty("FkEntityAccountCharge")]
    public virtual ICollection<AccountChargeReferenceMask> AccountChargeReferenceMasks { get; set; } = new List<AccountChargeReferenceMask>();

    [InverseProperty("FkEntityAccountCharge")]
    public virtual ICollection<EntityAccountChargeAccumulatedAmountLog> EntityAccountChargeAccumulatedAmountLogs { get; set; } = new List<EntityAccountChargeAccumulatedAmountLog>();

    [InverseProperty("FkEntityAccountCharge")]
    public virtual ICollection<EntityAccountChargeExclusion> EntityAccountChargeExclusions { get; set; } = new List<EntityAccountChargeExclusion>();

    [InverseProperty("FkEntityAccountCharge")]
    public virtual ICollection<EntityAccountChargeExlusionLog> EntityAccountChargeExlusionLogs { get; set; } = new List<EntityAccountChargeExlusionLog>();

    [InverseProperty("FkEntityAccountCharge")]
    public virtual ICollection<EntityAccountChargeLog> EntityAccountChargeLogs { get; set; } = new List<EntityAccountChargeLog>();

    [InverseProperty("FkEntityAccountCharge")]
    public virtual ICollection<EntityAccountChargeMapping> EntityAccountChargeMappings { get; set; } = new List<EntityAccountChargeMapping>();

    [InverseProperty("FkEntityAccountCharge")]
    public virtual ICollection<EntityAccountChargeReversalLog> EntityAccountChargeReversalLogs { get; set; } = new List<EntityAccountChargeReversalLog>();

    [InverseProperty("FkEntityAccountCharge")]
    public virtual ICollection<EntityAccountChargeReversal> EntityAccountChargeReversals { get; set; } = new List<EntityAccountChargeReversal>();

    [ForeignKey("FkEntityAccountId")]
    [InverseProperty("EntityAccountCharges")]
    public virtual EntityAccount FkEntityAccount { get; set; } = null!;

    [ForeignKey("TransactionCodeId")]
    [InverseProperty("EntityAccountCharges")]
    public virtual TransactionCode TransactionCode { get; set; } = null!;
}


