using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VProductSupplierZone
{
    public Guid PkSupplierZoneId { get; set; }

    public string Name { get; set; } = null!;

    public Guid FkProductId { get; set; }

    public bool? Active { get; set; }

    public Guid FkSupplierId { get; set; }
}
