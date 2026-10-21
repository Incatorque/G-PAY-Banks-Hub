using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VMyPlan
{
    public Guid PkPlanId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ExpirationDate { get; set; }

    [Column("PkOrderID")]
    public Guid PkOrderId { get; set; }

    public int OrderNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string OrderStatus { get; set; } = null!;

    [StringLength(51)]
    [Unicode(false)]
    public string? Price { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Quantity { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? ShortName { get; set; }

    [StringLength(51)]
    [Unicode(false)]
    public string? OrderTotal { get; set; }

    [Column("PkOrderItemID")]
    public Guid PkOrderItemId { get; set; }

    public bool? AddedToBasketFromPlan { get; set; }

    [StringLength(560)]
    [Unicode(false)]
    public string? DeliveryAddress { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? OrderDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Vendor { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? ExtRef { get; set; }
}


