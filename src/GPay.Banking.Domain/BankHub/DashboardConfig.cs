using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("DashboardConfig")]
public partial class DashboardConfig : Entity<Guid>
{
    [Key]
    [Column("PKDashboardConfigID")]
    public override Guid Id { get; protected set; }

    [Column("FkDashboardTypeID")]
    public int FkDashboardTypeId { get; set; }

    [Column("FKEntityID")]
    public Guid FkentityId { get; set; }

    [Column("FkAccountID")]
    public Guid FkAccountId { get; set; }

    [Column("FkDashboardColumnVisibilityID")]
    public Guid? FkDashboardColumnVisibilityId { get; set; }

    public bool? CreditTransaction { get; set; }

    public bool? DebitTransaction { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Interval { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? MinVal { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? MaxVal { get; set; }

    [Column("X-Axis")]
    [StringLength(200)]
    [Unicode(false)]
    public string? XAxis { get; set; }

    [Column("Y-Axis")]
    [StringLength(200)]
    [Unicode(false)]
    public string? YAxis { get; set; }

    [Unicode(false)]
    public string? Reference { get; set; }

    [Column("FkUnitOfMeasureID")]
    public int? FkUnitOfMeasureId { get; set; }

    [Column("FkFrequencyID")]
    public int? FkFrequencyId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Description { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Heading { get; set; }

    public bool? Active { get; set; }

    public bool? ShowSummaryData { get; set; }

    [InverseProperty("FkDashboardConfig")]
    public virtual ICollection<DashboardLedgerLink> DashboardLedgerLinks { get; set; } = new List<DashboardLedgerLink>();

    [InverseProperty("FkDashboardConfig")]
    public virtual ICollection<DashboardTransactionCodeLink> DashboardTransactionCodeLinks { get; set; } = new List<DashboardTransactionCodeLink>();

    [ForeignKey("FkAccountId")]
    [InverseProperty("DashboardConfigs")]
    public virtual Account FkAccount { get; set; } = null!;

    [ForeignKey("FkDashboardColumnVisibilityId")]
    [InverseProperty("DashboardConfigs")]
    public virtual DashboardColumnVisibility? FkDashboardColumnVisibility { get; set; }

    [ForeignKey("FkDashboardTypeId")]
    [InverseProperty("DashboardConfigs")]
    public virtual DashboardType FkDashboardType { get; set; } = null!;

    [ForeignKey("FkFrequencyId")]
    [InverseProperty("DashboardConfigs")]
    public virtual DashboardFrequency? FkFrequency { get; set; }

    [ForeignKey("FkUnitOfMeasureId")]
    [InverseProperty("DashboardConfigs")]
    public virtual DashboardUnitOfMeasure? FkUnitOfMeasure { get; set; }

    [ForeignKey("FkentityId")]
    [InverseProperty("DashboardConfigs")]
    public virtual CustomEntity Fkentity { get; set; } = null!;
}


