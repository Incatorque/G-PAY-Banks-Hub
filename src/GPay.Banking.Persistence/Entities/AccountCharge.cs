using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AccountCharge
{
    public Guid PkAccountChargeId { get; set; }

    public Guid FkaccountId { get; set; }

    public Guid FkentityAccountId { get; set; }

    public string ChargeType { get; set; } = null!;

    public decimal? Value { get; set; }

    public string Reference { get; set; } = null!;

    public int Sequence { get; set; }

    public DateTime CreateDate { get; set; }

    public int TransactionCodeId { get; set; }

    public bool? ApplyOnDebit { get; set; }

    public bool? Active { get; set; }

    public DateTime? ChargeStartDate { get; set; }

    public DateTime? ChargeExpiryDate { get; set; }

    public decimal? ChargeExpiryAmount { get; set; }

    public decimal? ChargeAccumulatedAmount { get; set; }

    public virtual ICollection<AccountChargeAccumulatedAmountLog> AccountChargeAccumulatedAmountLogs { get; set; } = new List<AccountChargeAccumulatedAmountLog>();

    public virtual ICollection<AccountChargeExludedEntity> AccountChargeExludedEntities { get; set; } = new List<AccountChargeExludedEntity>();

    public virtual ICollection<AccountChargeExludedEntityLog> AccountChargeExludedEntityLogs { get; set; } = new List<AccountChargeExludedEntityLog>();

    public virtual ICollection<AccountChargeLog> AccountChargeLogs { get; set; } = new List<AccountChargeLog>();

    public virtual ICollection<AccountChargeMapping> AccountChargeMappings { get; set; } = new List<AccountChargeMapping>();

    public virtual ICollection<AccountChargeReversalLog> AccountChargeReversalLogs { get; set; } = new List<AccountChargeReversalLog>();

    public virtual ICollection<AccountChargeReversal> AccountChargeReversals { get; set; } = new List<AccountChargeReversal>();

    public virtual Account Fkaccount { get; set; } = null!;

    public virtual TransactionCode TransactionCode { get; set; } = null!;
}
