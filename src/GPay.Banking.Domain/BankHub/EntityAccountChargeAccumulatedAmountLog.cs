using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityAccountChargeAccumulatedAmountLog")]
public partial class EntityAccountChargeAccumulatedAmountLog : Entity<Guid>
{
    [Key]
    [Column("PkEntityAccountChargeAccumulatedAmountLogId")]
    public override Guid Id { get; protected set; }

    public Guid FkEntityAccountChargeId { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal ChargeAdjustmentAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ChargeDate { get; set; }

    [ForeignKey("FkEntityAccountChargeId")]
    [InverseProperty("EntityAccountChargeAccumulatedAmountLogs")]
    public virtual EntityAccountCharge FkEntityAccountCharge { get; set; } = null!;
}


