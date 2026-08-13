using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class DashboardConfig
{
    public Guid PkdashboardConfigId { get; set; }

    public int FkDashboardTypeId { get; set; }

    public Guid FkentityId { get; set; }

    public Guid FkAccountId { get; set; }

    public Guid? FkDashboardColumnVisibilityId { get; set; }

    public bool? CreditTransaction { get; set; }

    public bool? DebitTransaction { get; set; }

    public string? Interval { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? MinVal { get; set; }

    public decimal? MaxVal { get; set; }

    public string? XAxis { get; set; }

    public string? YAxis { get; set; }

    public string? Reference { get; set; }

    public int? FkUnitOfMeasureId { get; set; }

    public int? FkFrequencyId { get; set; }

    public string? Description { get; set; }

    public string? Heading { get; set; }

    public bool? Active { get; set; }

    public bool? ShowSummaryData { get; set; }

    public virtual ICollection<DashboardLedgerLink> DashboardLedgerLinks { get; set; } = new List<DashboardLedgerLink>();

    public virtual ICollection<DashboardTransactionCodeLink> DashboardTransactionCodeLinks { get; set; } = new List<DashboardTransactionCodeLink>();

    public virtual Account FkAccount { get; set; } = null!;

    public virtual DashboardColumnVisibility? FkDashboardColumnVisibility { get; set; }

    public virtual DashboardType FkDashboardType { get; set; } = null!;

    public virtual DashboardFrequency? FkFrequency { get; set; }

    public virtual DashboardUnitOfMeasure? FkUnitOfMeasure { get; set; }

    public virtual Entity Fkentity { get; set; } = null!;
}
