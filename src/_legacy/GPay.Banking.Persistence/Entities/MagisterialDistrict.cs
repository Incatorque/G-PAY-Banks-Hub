using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class MagisterialDistrict
{
    public Guid PkMagisterialDistrictId { get; set; }

    public string Name { get; set; } = null!;

    public Guid FkProvinceId { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<EntityAddress> EntityAddresses { get; set; } = new List<EntityAddress>();

    public virtual Province FkProvince { get; set; } = null!;

    public virtual ICollection<MagisterialDistrictGridZone> MagisterialDistrictGridZones { get; set; } = new List<MagisterialDistrictGridZone>();
}
