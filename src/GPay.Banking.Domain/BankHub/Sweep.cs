using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Sweep")]
public partial class Sweep : Entity<Guid>
{
    [Key]
    [Column("PkSweepID")]
    public override Guid Id { get; protected set; }

    [Column(TypeName = "decimal(16, 2)")]
    public decimal? PaymentValue { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string PaymentType { get; set; } = null!;

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? MinimumRequiredBalance { get; set; }

    public int Sequence { get; set; }

    [Column("FkFrequencyID")]
    public int FkFrequencyId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SuccessfullProcessed { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastProcessed { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpiryDate { get; set; }

    public Guid? FkAccountId { get; set; }

    public bool? Active { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProcTime { get; set; }

    public bool? UseExpiryAmount { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? ExpiryAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpiryAmountResetDate { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? TotalSweepAmount { get; set; }

    public Guid? FkToAccountId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? RequiredRemainingBalance { get; set; }

    [Column("FKSupplierClientID")]
    public Guid? FksupplierClientId { get; set; }

    public bool InstantPayment { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? SweepClientReference { get; set; }

    [ForeignKey("FkFrequencyId")]
    [InverseProperty("Sweeps")]
    public virtual Frequency FkFrequency { get; set; } = null!;

    [InverseProperty("Sweep")]
    public virtual ICollection<SweepLog> SweepLogs { get; set; } = new List<SweepLog>();
}


