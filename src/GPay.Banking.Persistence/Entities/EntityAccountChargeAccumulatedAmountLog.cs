using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityAccountChargeAccumulatedAmountLog
{
    public Guid PkEntityAccountChargeAccumulatedAmountLogId { get; set; }

    public Guid FkEntityAccountChargeId { get; set; }

    public decimal ChargeAdjustmentAmount { get; set; }

    public DateTime ChargeDate { get; set; }

    public virtual EntityAccountCharge FkEntityAccountCharge { get; set; } = null!;
}
