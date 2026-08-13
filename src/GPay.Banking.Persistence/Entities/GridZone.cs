using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class GridZone
{
    public Guid PkGridZoneId { get; set; }

    public string Description { get; set; } = null!;

    public Guid FkGridId { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<BaseProductZone> BaseProductZones { get; set; } = new List<BaseProductZone>();

    public virtual Grid FkGrid { get; set; } = null!;

    public virtual ICollection<MagisterialDistrictGridZone> MagisterialDistrictGridZones { get; set; } = new List<MagisterialDistrictGridZone>();
}
