using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class MagisterialDistrictGridZone
{
    public Guid PkMagisterialDistrictGridZoneId { get; set; }

    public Guid FkMagisterialDistrictId { get; set; }

    public Guid FkGridZoneId { get; set; }

    public bool? Active { get; set; }

    public virtual GridZone FkGridZone { get; set; } = null!;

    public virtual MagisterialDistrict FkMagisterialDistrict { get; set; } = null!;
}
