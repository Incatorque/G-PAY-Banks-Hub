using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VEntityOrderSweep
{
    [Column("PkEntityOrderSweepID")]
    public int PkEntityOrderSweepId { get; set; }

    [Column("PkEntityID")]
    public Guid PkEntityId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Entity { get; set; } = null!;

    public Guid FromAccountId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? FromAccount { get; set; }

    [StringLength(30)]
    public string FromAccountNo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string OrderStatus { get; set; } = null!;

    public int Days { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastProcessed { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpiryDate { get; set; }

    public bool? Active { get; set; }

    public bool? Deleted { get; set; }

    [Column("Deleted Entiy sweep")]
    public bool? DeletedEntiySweep { get; set; }
}


