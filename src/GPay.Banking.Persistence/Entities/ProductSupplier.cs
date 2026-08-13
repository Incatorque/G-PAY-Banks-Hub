using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ProductSupplier
{
    public Guid PkProductSupplierId { get; set; }

    public Guid FkProductId { get; set; }

    public Guid FkSupplierId { get; set; }

    public bool? Active { get; set; }

    public DateTime DateCreated { get; set; }

    public Guid FkProductStatusId { get; set; }

    public Guid? FkAccountId { get; set; }

    public bool? Deleted { get; set; }

    public string? ImageLocation { get; set; }

    public virtual Product FkProduct { get; set; } = null!;

    public virtual Entity FkSupplier { get; set; } = null!;

    public virtual ICollection<ProductCommission> ProductCommissions { get; set; } = new List<ProductCommission>();

    public virtual ICollection<ProductZone> ProductZones { get; set; } = new List<ProductZone>();
}
