using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityAccountCharge
{
    public Guid PkEntityAccountChargeId { get; set; }

    public Guid FkEntityAccountId { get; set; }

    public string ChargeType { get; set; } = null!;

    public decimal? Value { get; set; }

    public string Reference { get; set; } = null!;

    public int Sequence { get; set; }

    public DateTime CreateDate { get; set; }

    public Guid MainEntityAccountId { get; set; }

    public int TransactionCodeId { get; set; }

    public bool? ApplyOnDebit { get; set; }

    public bool? Active { get; set; }

    public bool? FilterOnRange { get; set; }

    public decimal? MaximumAmount { get; set; }

    public decimal? MinimumAmount { get; set; }

    public DateTime? ChargeStartDate { get; set; }

    public DateTime? ChargeExpiryDate { get; set; }

    public decimal? ChargeExpiryAmount { get; set; }

    public decimal? ChargeAccumulatedAmount { get; set; }

    public virtual ICollection<AccountChargeReferenceMask> AccountChargeReferenceMasks { get; set; } = new List<AccountChargeReferenceMask>();

    public virtual ICollection<EntityAccountChargeAccumulatedAmountLog> EntityAccountChargeAccumulatedAmountLogs { get; set; } = new List<EntityAccountChargeAccumulatedAmountLog>();

    public virtual ICollection<EntityAccountChargeExclusion> EntityAccountChargeExclusions { get; set; } = new List<EntityAccountChargeExclusion>();

    public virtual ICollection<EntityAccountChargeExlusionLog> EntityAccountChargeExlusionLogs { get; set; } = new List<EntityAccountChargeExlusionLog>();

    public virtual ICollection<EntityAccountChargeLog> EntityAccountChargeLogs { get; set; } = new List<EntityAccountChargeLog>();

    public virtual ICollection<EntityAccountChargeMapping> EntityAccountChargeMappings { get; set; } = new List<EntityAccountChargeMapping>();

    public virtual ICollection<EntityAccountChargeReversalLog> EntityAccountChargeReversalLogs { get; set; } = new List<EntityAccountChargeReversalLog>();

    public virtual ICollection<EntityAccountChargeReversal> EntityAccountChargeReversals { get; set; } = new List<EntityAccountChargeReversal>();

    public virtual EntityAccount FkEntityAccount { get; set; } = null!;

    public virtual TransactionCode TransactionCode { get; set; } = null!;
}
