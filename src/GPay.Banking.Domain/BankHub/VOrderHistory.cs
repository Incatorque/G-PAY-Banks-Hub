using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VOrderHistory
{
    [Column("FkOrderID")]
    public Guid FkOrderId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [StringLength(500)]
    public string? Note { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string OrderStatus { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? UserEmail { get; set; }

    [Column("PkOrderHistoryID")]
    public Guid PkOrderHistoryId { get; set; }
}


