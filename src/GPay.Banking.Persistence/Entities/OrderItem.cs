using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OrderItem
{
    public Guid PkOrderItemId { get; set; }

    public Guid FkOrderId { get; set; }

    public Guid? FkProductZoneId { get; set; }

    public decimal? Price { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Discount { get; set; }

    public Guid FkOrderStatusId { get; set; }

    public Guid? FkStatusChangeUserId { get; set; }

    public DateTime StatusChangeDate { get; set; }

    public decimal? DeliveredQuantity { get; set; }

    public Guid? PodreferenceNumber { get; set; }

    public Guid? FkLinkedOrderItemId { get; set; }

    public decimal? MinOrder { get; set; }

    public decimal? MaxOrder { get; set; }

    public Guid? FkClassCategoryId { get; set; }

    public bool? AddedToBasketFromPlan { get; set; }

    public Guid? FkPlanId { get; set; }

    public Guid? FkFromProductId { get; set; }

    public bool? Active { get; set; }

    public string? Servicetype { get; set; }

    public bool? IsVatIncluded { get; set; }

    public Guid? FkOutwardBopcategoryId { get; set; }

    public virtual Order FkOrder { get; set; } = null!;

    public virtual OrderItemStatus FkOrderStatus { get; set; } = null!;

    public virtual OutwardBopcategory? FkOutwardBopcategory { get; set; }

    public virtual ProductZone? FkProductZone { get; set; }

    public virtual User? FkStatusChangeUser { get; set; }

    public virtual ICollection<OrderItemApproval> OrderItemApprovals { get; set; } = new List<OrderItemApproval>();

    public virtual ICollection<OrderItemCommission> OrderItemCommissions { get; set; } = new List<OrderItemCommission>();

    public virtual ICollection<OrderItemReference> OrderItemReferences { get; set; } = new List<OrderItemReference>();
}
