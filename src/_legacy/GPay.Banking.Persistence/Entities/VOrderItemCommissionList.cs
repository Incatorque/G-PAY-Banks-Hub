using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VOrderItemCommissionList
{
    public Guid PkOrderId { get; set; }

    public Guid PkOrderItemId { get; set; }

    public Guid FkAgentId { get; set; }

    public decimal? CommissionSum { get; set; }
}
