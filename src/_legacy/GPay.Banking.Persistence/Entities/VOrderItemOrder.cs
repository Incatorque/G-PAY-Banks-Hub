using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VOrderItemOrder
{
    public Guid PkOrderItemId { get; set; }

    public Guid FkOrderId { get; set; }

    public Guid? FkProductZoneId { get; set; }

    public decimal? Price { get; set; }

    public decimal? Quantity { get; set; }

    public Guid FkOrderStatusId { get; set; }

    public decimal? Discount { get; set; }

    public Guid? FkStatusChangeUserId { get; set; }

    public DateTime StatusChangeDate { get; set; }

    public decimal? DeliveredQuantity { get; set; }

    public Guid? PodreferenceNumber { get; set; }

    public Guid OrderStatus { get; set; }
}
