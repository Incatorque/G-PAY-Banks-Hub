using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VproductAllocationOver
{
    public Guid PkProductId { get; set; }

    public string Code { get; set; } = null!;

    public string? ShortName { get; set; }

    public string Unit { get; set; } = null!;

    public int Excluded { get; set; }

    public decimal? ProductAllocationQty { get; set; }

    public string? ProductAllocationTerms { get; set; }

    public decimal? OverridgeQty { get; set; }

    public string? OverridgeTerms { get; set; }

    public Guid PkSupplierClientId { get; set; }

    public Guid? PkAllocationOverId { get; set; }

    public Guid? PkProductZoneId { get; set; }

    public DateTime? DateCreated { get; set; }

    public string? SupplierZoneName { get; set; }
}
