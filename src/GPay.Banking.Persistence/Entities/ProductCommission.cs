using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ProductCommission
{
    public Guid PkProductCommissionId { get; set; }

    public Guid FkAgentId { get; set; }

    public decimal? Value { get; set; }

    public Guid FkCommissionTypeId { get; set; }

    public int Order { get; set; }

    public Guid FkProductSupplierId { get; set; }

    public virtual Entity FkAgent { get; set; } = null!;

    public virtual CommissionType FkCommissionType { get; set; } = null!;

    public virtual ProductSupplier FkProductSupplier { get; set; } = null!;
}
