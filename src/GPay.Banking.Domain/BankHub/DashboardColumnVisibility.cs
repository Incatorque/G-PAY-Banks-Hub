using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("DashboardColumnVisibility")]
public partial class DashboardColumnVisibility : Entity<Guid>
{
    [Key]
    [Column("PkDashboardColumnVisibilityID")]
    public override Guid Id { get; protected set; }

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

    [InverseProperty("FkDashboardColumnVisibility")]
    public virtual ICollection<DashboardConfig> DashboardConfigs { get; set; } = new List<DashboardConfig>();
}


