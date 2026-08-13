using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class SupplierCommission
{
    public Guid PkSupplierCommissionId { get; set; }

    public Guid FkSupplierId { get; set; }

    public Guid FkAgentId { get; set; }

    public decimal? Value { get; set; }

    public Guid FkCommissionTypeId { get; set; }

    public int Order { get; set; }

    public Guid? FkClientId { get; set; }

    public virtual Entity FkAgent { get; set; } = null!;

    public virtual Entity? FkClient { get; set; }

    public virtual CommissionType FkCommissionType { get; set; } = null!;

    public virtual Entity FkSupplier { get; set; } = null!;
}
