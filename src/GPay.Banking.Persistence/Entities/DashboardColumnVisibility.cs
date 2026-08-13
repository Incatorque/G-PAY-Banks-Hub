using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class DashboardColumnVisibility
{
    public Guid PkDashboardColumnVisibilityId { get; set; }

    public bool? Bank { get; set; }

    public bool? AccountNumber { get; set; }

    public bool? Entity { get; set; }

    public bool? Description { get; set; }

    public bool? TransactionDate { get; set; }

    public bool? SyncDate { get; set; }

    public bool? ReferenceNumber { get; set; }

    public bool? Amount { get; set; }

    public bool? EventNumber { get; set; }

    public bool? Type { get; set; }

    public bool? Active { get; set; }

    public bool? TransactionCode { get; set; }

    public virtual ICollection<DashboardConfig> DashboardConfigs { get; set; } = new List<DashboardConfig>();
}
