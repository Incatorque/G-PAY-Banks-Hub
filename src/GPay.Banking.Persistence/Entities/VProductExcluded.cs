using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VProductExcluded
{
    public Guid PkSupplierClientId { get; set; }

    public Guid PkClientId { get; set; }

    public string Name { get; set; } = null!;

    public string? Taname { get; set; }

    public Guid FkSupplierId { get; set; }

    public Guid PkProductId { get; set; }

    public string Code { get; set; } = null!;

    public string? ShortName { get; set; }

    public string Description { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public int Excluded { get; set; }
}
