using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class DashboardFrequency
{
    public int PkdashboardFrequencyId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<DashboardConfig> DashboardConfigs { get; set; } = new List<DashboardConfig>();
}
