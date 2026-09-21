using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OrderCommission
{
    public Guid PkOrderCommissionId { get; set; }

    public Guid FkOrderId { get; set; }

    public Guid FkAgentId { get; set; }

    public decimal? Value { get; set; }

    public Guid FkCommissionTypeId { get; set; }

    public int Order { get; set; }

    public Guid FkClientId { get; set; }

    public bool? VatRegistered { get; set; }

    public virtual Entity FkAgent { get; set; } = null!;

    public virtual Entity FkClient { get; set; } = null!;

    public virtual CommissionType FkCommissionType { get; set; } = null!;

    public virtual Order FkOrder { get; set; } = null!;
}
