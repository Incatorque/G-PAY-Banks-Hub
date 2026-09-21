using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityOrderSweepLog
{
    public int PkEntityOrderSweepLogId { get; set; }

    public int FkEntityOrderSweepId { get; set; }

    public Guid FkOrderId { get; set; }

    public decimal Amount { get; set; }

    public DateTime SweepDate { get; set; }

    public bool? Processed { get; set; }

    public virtual EntityOrderSweep FkEntityOrderSweep { get; set; } = null!;

    public virtual Order FkOrder { get; set; } = null!;
}
