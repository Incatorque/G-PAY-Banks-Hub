using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VSweepRecord
{
    [Column("PkEntityID")]
    public Guid PkEntityId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public Guid ToAccountId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string ToAccount { get; set; } = null!;

    [StringLength(30)]
    public string ToAccountNo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ToBank { get; set; } = null!;

    public Guid FromAccountId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? FromAccount { get; set; }

    [StringLength(30)]
    public string FromAccountNo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string FromBank { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? LastProcessed { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SuccessfullProcessed { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpiryDate { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal MinimumRequiredBalance { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? PaymentType { get; set; }

    [Column(TypeName = "decimal(16, 2)")]
    public decimal? PaymentValue { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    public int Sequence { get; set; }

    [Column("PkSupplierClientID")]
    public Guid PkSupplierClientId { get; set; }

    [Column("PkSweepID")]
    public Guid PkSweepId { get; set; }

    public bool? UseExpiryAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpiryAmountResetDate { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? ExpiryAmount { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? TotalSweepAmount { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal RequiredRemainingBalance { get; set; }

    public bool InstantPayment { get; set; }
}


