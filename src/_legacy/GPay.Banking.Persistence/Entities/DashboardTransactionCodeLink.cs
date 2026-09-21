using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class DashboardTransactionCodeLink
{
    public Guid PkDashboardTransactionCodeLinkId { get; set; }

    public Guid FkDashboardConfigId { get; set; }

    public int FktransactionCodeId { get; set; }

    public bool? Active { get; set; }

    public virtual DashboardConfig FkDashboardConfig { get; set; } = null!;

    public virtual TransactionCode FktransactionCode { get; set; } = null!;
}
