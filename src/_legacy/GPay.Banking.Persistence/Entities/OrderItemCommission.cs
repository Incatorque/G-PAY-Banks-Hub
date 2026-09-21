using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OrderItemCommission
{
    public Guid PkOrderItemCommissionId { get; set; }

    public Guid FkOrderItemId { get; set; }

    public Guid FkAgentId { get; set; }

    public decimal? Value { get; set; }

    public Guid FkCommissionTypeId { get; set; }

    public int Order { get; set; }

    public bool? VatRegistered { get; set; }

    public virtual Entity FkAgent { get; set; } = null!;

    public virtual CommissionType FkCommissionType { get; set; } = null!;

    public virtual OrderItem FkOrderItem { get; set; } = null!;
}
