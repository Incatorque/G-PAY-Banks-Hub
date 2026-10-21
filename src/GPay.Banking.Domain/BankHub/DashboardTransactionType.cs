using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("DashboardTransactionType")]
public partial class DashboardTransactionType : Entity<int>
{
    [Key]
    [Column("PKDashboardTransactionTypeId")]
    public override int Id { get; protected set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }
}


