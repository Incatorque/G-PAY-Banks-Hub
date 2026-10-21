using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OrderItem")]
[Index("FkProductZoneId", "Active", Name = "Index_OrderItems_01")]
[Index("Active", Name = "Index_OrderItems_02")]
[Index("FkOrderId", Name = "NCI_OrderItem_FkProductZoneID")]
public partial class OrderItem : Entity<Guid>
{
    [Key]
    [Column("PkOrderItemID")]
    public override Guid Id { get; protected set; }

    [Column("FkOrderID")]
    public Guid FkOrderId { get; set; }

    [Column("FkProductZoneID")]
    public Guid? FkProductZoneId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Price { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Quantity { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Discount { get; set; }

    [Column("FkOrderStatusID")]
    public Guid FkOrderStatusId { get; set; }

    [Column("FkStatusChangeUserID")]
    public Guid? FkStatusChangeUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StatusChangeDate { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? DeliveredQuantity { get; set; }

    [Column("PODReferenceNumber")]
    public Guid? PodreferenceNumber { get; set; }

    [Column("FkLinkedOrderItemID")]
    public Guid? FkLinkedOrderItemId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? MinOrder { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? MaxOrder { get; set; }

    [Column("FkClassCategoryID")]
    public Guid? FkClassCategoryId { get; set; }

    public bool? AddedToBasketFromPlan { get; set; }

    public Guid? FkPlanId { get; set; }

    public Guid? FkFromProductId { get; set; }

    public bool? Active { get; set; }

    [StringLength(2)]
    public string? Servicetype { get; set; }

    public bool? IsVatIncluded { get; set; }

    [Column("FkOutwardBOPCategoryId")]
    public Guid? FkOutwardBopcategoryId { get; set; }

    [ForeignKey("FkOrderId")]
    [InverseProperty("OrderItems")]
    public virtual Order FkOrder { get; set; } = null!;

    [ForeignKey("FkOrderStatusId")]
    [InverseProperty("OrderItems")]
    public virtual OrderItemStatus FkOrderStatus { get; set; } = null!;

    [ForeignKey("FkOutwardBopcategoryId")]
    [InverseProperty("OrderItems")]
    public virtual OutwardBopcategory? FkOutwardBopcategory { get; set; }

    [ForeignKey("FkProductZoneId")]
    [InverseProperty("OrderItems")]
    public virtual ProductZone? FkProductZone { get; set; }

    [ForeignKey("FkStatusChangeUserId")]
    [InverseProperty("OrderItems")]
    public virtual User? FkStatusChangeUser { get; set; }

    [InverseProperty("FkOrderItem")]
    public virtual ICollection<OrderItemApproval> OrderItemApprovals { get; set; } = new List<OrderItemApproval>();

    [InverseProperty("FkOrderItem")]
    public virtual ICollection<OrderItemCommission> OrderItemCommissions { get; set; } = new List<OrderItemCommission>();

    [InverseProperty("FkOrderItemNavigation")]
    public virtual ICollection<OrderItemReference> OrderItemReferences { get; set; } = new List<OrderItemReference>();
}


