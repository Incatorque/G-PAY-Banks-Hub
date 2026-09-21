using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Province
{
    public Guid PkProvinceId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public int? FkCountryId { get; set; }

    public virtual ICollection<EntityAddressPending> EntityAddressPendings { get; set; } = new List<EntityAddressPending>();

    public virtual ICollection<EntityAddress> EntityAddresses { get; set; } = new List<EntityAddress>();

    public virtual ICollection<MagisterialDistrict> MagisterialDistricts { get; set; } = new List<MagisterialDistrict>();

    public virtual ICollection<Town> Towns { get; set; } = new List<Town>();
}
