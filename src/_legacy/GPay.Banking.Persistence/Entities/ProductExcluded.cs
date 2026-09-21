using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ProductExcluded
{
    public Guid PkProductExludedId { get; set; }

    public Guid FkSupplierClientId { get; set; }

    public Guid FkProductId { get; set; }

    public virtual Product FkProduct { get; set; } = null!;

    public virtual SupplierClient FkSupplierClient { get; set; } = null!;
}
