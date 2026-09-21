using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class DifferentialType
{
    public Guid PkDifferentialTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductZone> ProductZones { get; set; } = new List<ProductZone>();
}
