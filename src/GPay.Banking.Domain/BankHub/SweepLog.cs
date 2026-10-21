using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("SweepLog")]
public partial class SweepLog : Entity<long>
{
    [Key]
    [Column("ID")]
    public override long Id { get; protected set; }

    [Column(TypeName = "datetime")]
    public DateTime DateSwept { get; set; }

    [Column("EntityBankStatementID")]
    public Guid EntityBankStatementId { get; set; }

    [Column("SweepID")]
    public Guid SweepId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? SweepAmount { get; set; }

    public bool? MainTransaction { get; set; }

    [ForeignKey("EntityBankStatementId")]
    [InverseProperty("SweepLogs")]
    public virtual EntityBankStatement EntityBankStatement { get; set; } = null!;

    [ForeignKey("SweepId")]
    [InverseProperty("SweepLogs")]
    public virtual Sweep Sweep { get; set; } = null!;
}


