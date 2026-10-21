using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("DashboardChartVisibilityLink")]
public partial class DashboardChartVisibilityLink : Entity<int>
{
    [Key]
    [Column("PkDashboardChartVisibilityLinkID")]
    public override int Id { get; protected set; }

    [Column("DashboardTypeID")]
    public int DashboardTypeId { get; set; }

    [Column("EntityID")]
    public Guid EntityId { get; set; }

    [Column("AccountID")]
    public Guid AccountId { get; set; }

    public bool? Visible { get; set; }
}


