using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityOrderSweep
{
    public int PkEntityOrderSweepId { get; set; }

    public Guid FkEntityId { get; set; }

    public Guid FkAccountId { get; set; }

    public int FkOrderSweepFrequencyId { get; set; }

    public Guid FkOrderStatusId { get; set; }

    public DateTime? LastProcessed { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public bool? Active { get; set; }

    public bool? Deleted { get; set; }

    public virtual ICollection<EntityOrderSweepLog> EntityOrderSweepLogs { get; set; } = new List<EntityOrderSweepLog>();

    public virtual Account FkAccount { get; set; } = null!;

    public virtual Entity FkEntity { get; set; } = null!;

    public virtual OrderStatus FkOrderStatus { get; set; } = null!;

    public virtual OrderSweepFrequency FkOrderSweepFrequency { get; set; } = null!;
}
