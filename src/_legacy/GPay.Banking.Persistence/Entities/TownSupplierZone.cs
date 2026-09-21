using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class TownSupplierZone
{
    public Guid PkTownSupplierZoneId { get; set; }

    public Guid FkTownId { get; set; }

    public Guid FkSupplierZoneId { get; set; }

    public virtual SupplierZone FkSupplierZone { get; set; } = null!;

    public virtual Town FkTown { get; set; } = null!;
}
