using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class SupplierZone
{
    public Guid PkSupplierZoneId { get; set; }

    public string Name { get; set; } = null!;

    public Guid FkSupplierId { get; set; }

    public bool? Active { get; set; }

    public virtual Entity FkSupplier { get; set; } = null!;

    public virtual ICollection<ProductZone> ProductZones { get; set; } = new List<ProductZone>();

    public virtual ICollection<TownSupplierZone> TownSupplierZones { get; set; } = new List<TownSupplierZone>();
}
