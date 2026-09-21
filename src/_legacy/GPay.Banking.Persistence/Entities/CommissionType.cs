using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class CommissionType
{
    public Guid PkCommissionTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<OrderCommission> OrderCommissions { get; set; } = new List<OrderCommission>();

    public virtual ICollection<OrderItemCommission> OrderItemCommissions { get; set; } = new List<OrderItemCommission>();

    public virtual ICollection<ProductCommission> ProductCommissions { get; set; } = new List<ProductCommission>();

    public virtual ICollection<ProductZoneCommission> ProductZoneCommissions { get; set; } = new List<ProductZoneCommission>();

    public virtual ICollection<SupplierCommission> SupplierCommissions { get; set; } = new List<SupplierCommission>();
}
