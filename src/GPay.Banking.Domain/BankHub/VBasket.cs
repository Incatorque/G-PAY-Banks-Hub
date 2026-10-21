using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VBasket
{
    [Column("PkOrderItemID")]
    public Guid PkOrderItemId { get; set; }

    [Column("FkOrderID")]
    public Guid FkOrderId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Quantity { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ProductName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string SupplierName { get; set; } = null!;

    [Column("PkProductID")]
    public Guid PkProductId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Price { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Discount { get; set; }

    [Column("FkOrderStatusID")]
    public Guid FkOrderStatusId { get; set; }

    public int OrderNumber { get; set; }

    public bool? Active { get; set; }
}


