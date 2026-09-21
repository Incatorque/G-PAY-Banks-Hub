using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OrderSweepFrequency
{
    public int PkOrderSweepFrequencyId { get; set; }

    public int Days { get; set; }

    public bool? Active { get; set; }

    public bool? Deleted { get; set; }

    public virtual ICollection<EntityOrderSweep> EntityOrderSweeps { get; set; } = new List<EntityOrderSweep>();
}
