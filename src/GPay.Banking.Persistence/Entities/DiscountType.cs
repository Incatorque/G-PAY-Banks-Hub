using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class DiscountType
{
    public Guid PkDiscountTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductZone> ProductZones { get; set; } = new List<ProductZone>();
}
