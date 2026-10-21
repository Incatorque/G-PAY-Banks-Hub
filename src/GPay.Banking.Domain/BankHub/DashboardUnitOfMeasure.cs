using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("DashboardUnitOfMeasure")]
public partial class DashboardUnitOfMeasure : Entity<int>
{
    [Key]
    [Column("PKDashboardUnitOfMeasureId")]
    public override int Id { get; protected set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    [InverseProperty("FkUnitOfMeasure")]
    public virtual ICollection<DashboardConfig> DashboardConfigs { get; set; } = new List<DashboardConfig>();
}


