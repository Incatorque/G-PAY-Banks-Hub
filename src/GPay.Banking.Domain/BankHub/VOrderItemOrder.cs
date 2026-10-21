using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VOrderItemOrder
{
    [Column("PkOrderItemID")]
    public Guid PkOrderItemId { get; set; }

    [Column("FkOrderID")]
    public Guid FkOrderId { get; set; }

    [Column("FkProductZoneID")]
    public Guid? FkProductZoneId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Price { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Quantity { get; set; }

    [Column("FkOrderStatusID")]
    public Guid FkOrderStatusId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Discount { get; set; }

    [Column("FkStatusChangeUserID")]
    public Guid? FkStatusChangeUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StatusChangeDate { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? DeliveredQuantity { get; set; }

    [Column("PODReferenceNumber")]
    public Guid? PodreferenceNumber { get; set; }

    public Guid OrderStatus { get; set; }
}


