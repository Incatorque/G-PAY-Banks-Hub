using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("DashboardLedgerLink")]
public partial class DashboardLedgerLink : Entity<Guid>
{
    [Key]
    [Column("PkDashboardLedgerLinkID")]
    public override Guid Id { get; protected set; }

    [Column("FkDashboardConfigID")]
    public Guid FkDashboardConfigId { get; set; }

    [Column("FKLedgerEntityId")]
    public Guid FkledgerEntityId { get; set; }

    public bool? Active { get; set; }

    [ForeignKey("FkDashboardConfigId")]
    [InverseProperty("DashboardLedgerLinks")]
    public virtual DashboardConfig FkDashboardConfig { get; set; } = null!;

    [ForeignKey("FkledgerEntityId")]
    [InverseProperty("DashboardLedgerLinks")]
    public virtual CustomEntity FkledgerEntity { get; set; } = null!;
}


