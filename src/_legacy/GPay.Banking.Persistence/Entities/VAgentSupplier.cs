using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VAgentSupplier
{
    public Guid PkEntityId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public Guid FkSupplierId { get; set; }
}
