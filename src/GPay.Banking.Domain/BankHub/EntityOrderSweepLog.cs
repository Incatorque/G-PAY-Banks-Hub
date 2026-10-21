using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityOrderSweepLog")]
public partial class EntityOrderSweepLog : Entity<int>
{
    [Key]
    [Column("PkEntityOrderSweepLogID")]
    public override int Id { get; protected set; }

    [Column("FkEntityOrderSweepID")]
    public int FkEntityOrderSweepId { get; set; }

    [Column("FkOrderID")]
    public Guid FkOrderId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal Amount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime SweepDate { get; set; }

    public bool? Processed { get; set; }

    [ForeignKey("FkEntityOrderSweepId")]
    [InverseProperty("EntityOrderSweepLogs")]
    public virtual EntityOrderSweep FkEntityOrderSweep { get; set; } = null!;

    [ForeignKey("FkOrderId")]
    [InverseProperty("EntityOrderSweepLogs")]
    public virtual Order FkOrder { get; set; } = null!;
}


