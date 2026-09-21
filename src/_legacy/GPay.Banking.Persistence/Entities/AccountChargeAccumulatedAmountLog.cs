using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AccountChargeAccumulatedAmountLog
{
    public Guid PkAccountChargeAccumulatedAmountLogId { get; set; }

    public Guid FkAccountChargeId { get; set; }

    public decimal AdjustmentAmount { get; set; }

    public DateTime ChargeDate { get; set; }

    public virtual AccountCharge FkAccountCharge { get; set; } = null!;
}
