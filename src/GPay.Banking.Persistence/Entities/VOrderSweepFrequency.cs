using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VOrderSweepFrequency
{
    public int PkOrderSweepFrequencyId { get; set; }

    public int Days { get; set; }

    public bool? Active { get; set; }

    public bool? Deleted { get; set; }
}
