using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ProductZone
{
    public Guid PkProductZoneId { get; set; }

    public Guid? FkSupplierZoneId { get; set; }

    public Guid? FkGroupId { get; set; }

    public Guid? FkClientId { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public decimal? Differential { get; set; }

    public decimal? Discount { get; set; }

    public Guid? FkDiscountTypeId { get; set; }

    public bool? Levy { get; set; }

    public decimal? MinPurchaseQty { get; set; }

    public decimal? MaxPurchaseQty { get; set; }

    public bool? ParentCommission { get; set; }

    public Guid FkDifferentialTypeId { get; set; }

    public bool? Active { get; set; }

    public decimal? OverwritePrice { get; set; }

    public Guid FkProductSupplierId { get; set; }

    public virtual DifferentialType FkDifferentialType { get; set; } = null!;

    public virtual DiscountType? FkDiscountType { get; set; }

    public virtual ProductSupplier FkProductSupplier { get; set; } = null!;

    public virtual SupplierZone? FkSupplierZone { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ProductAllocationOverridge> ProductAllocationOverridges { get; set; } = new List<ProductAllocationOverridge>();

    public virtual ICollection<ProductZoneCommission> ProductZoneCommissions { get; set; } = new List<ProductZoneCommission>();
}
