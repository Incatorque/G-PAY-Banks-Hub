using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Grid
{
    public Guid PkGridId { get; set; }

    public string Description { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<BaseProduct> BaseProducts { get; set; } = new List<BaseProduct>();

    public virtual ICollection<GridZone> GridZones { get; set; } = new List<GridZone>();
}
