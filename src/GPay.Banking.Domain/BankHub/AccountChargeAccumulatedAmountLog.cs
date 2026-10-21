using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("AccountChargeAccumulatedAmountLog")]
public partial class AccountChargeAccumulatedAmountLog : Entity<Guid>
{
    [Key]
    [Column("PkAccountChargeAccumulatedAmountLogId")]
    public override Guid Id { get; protected set; }

    public Guid FkAccountChargeId { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal AdjustmentAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ChargeDate { get; set; }

    [ForeignKey("FkAccountChargeId")]
    [InverseProperty("AccountChargeAccumulatedAmountLogs")]
    public virtual AccountCharge FkAccountCharge { get; set; } = null!;
}


