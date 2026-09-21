using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VOrderSupplier
{
    public Guid FkOrderId { get; set; }

    public Guid FkSupplierId { get; set; }

    public int Payment { get; set; }
}
