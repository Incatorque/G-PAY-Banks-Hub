using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("DashboardTransactionCodeLink")]
public partial class DashboardTransactionCodeLink : Entity<Guid>
{
    [Key]
    [Column("PkDashboardTransactionCodeLinkID")]
    public override Guid Id { get; protected set; }

    [Column("FkDashboardConfigID")]
    public Guid FkDashboardConfigId { get; set; }

    [Column("FKTransactionCodeId")]
    public int FktransactionCodeId { get; set; }

    public bool? Active { get; set; }

    [ForeignKey("FkDashboardConfigId")]
    [InverseProperty("DashboardTransactionCodeLinks")]
    public virtual DashboardConfig FkDashboardConfig { get; set; } = null!;

    [ForeignKey("FktransactionCodeId")]
    [InverseProperty("DashboardTransactionCodeLinks")]
    public virtual TransactionCode FktransactionCode { get; set; } = null!;
}


