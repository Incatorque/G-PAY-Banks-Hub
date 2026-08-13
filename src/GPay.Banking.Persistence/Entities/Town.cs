using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Town
{
    public Guid PkTownId { get; set; }

    public string Name { get; set; } = null!;

    public Guid FkProvinceId { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<EntityAddressPending> EntityAddressPendings { get; set; } = new List<EntityAddressPending>();

    public virtual ICollection<EntityAddress> EntityAddresses { get; set; } = new List<EntityAddress>();

    public virtual Province FkProvince { get; set; } = null!;

    public virtual ICollection<TownSupplierZone> TownSupplierZones { get; set; } = new List<TownSupplierZone>();
}
