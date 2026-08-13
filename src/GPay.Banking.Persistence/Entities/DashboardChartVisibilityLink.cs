using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class DashboardChartVisibilityLink
{
    public int PkDashboardChartVisibilityLinkId { get; set; }

    public int DashboardTypeId { get; set; }

    public Guid EntityId { get; set; }

    public Guid AccountId { get; set; }

    public bool? Visible { get; set; }
}
