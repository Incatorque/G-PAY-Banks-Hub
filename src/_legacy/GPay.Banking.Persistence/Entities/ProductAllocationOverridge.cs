using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ProductAllocationOverridge
{
    public Guid PkAllocationOverId { get; set; }

    public decimal? ProductAllocationQty { get; set; }

    public string? ProductAllocationTerms { get; set; }

    public decimal? OverridgeQty { get; set; }

    public string? OverridgeTerms { get; set; }

    public Guid FkProductZoneId { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual ProductZone FkProductZone { get; set; } = null!;
}
