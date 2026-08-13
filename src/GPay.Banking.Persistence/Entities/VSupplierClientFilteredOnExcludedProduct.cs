using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VSupplierClientFilteredOnExcludedProduct
{
    public Guid FkSupplierId { get; set; }

    public string Name { get; set; } = null!;

    public Guid PkSupplierClientId { get; set; }

    public Guid FkClientId { get; set; }
}
