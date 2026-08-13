using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class DashboardLedgerLink
{
    public Guid PkDashboardLedgerLinkId { get; set; }

    public Guid FkDashboardConfigId { get; set; }

    public Guid FkledgerEntityId { get; set; }

    public bool? Active { get; set; }

    public virtual DashboardConfig FkDashboardConfig { get; set; } = null!;

    public virtual Entity FkledgerEntity { get; set; } = null!;
}
