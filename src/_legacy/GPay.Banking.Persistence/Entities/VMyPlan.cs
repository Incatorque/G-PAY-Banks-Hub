using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VMyPlan
{
    public Guid PkPlanId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime ExpirationDate { get; set; }

    public Guid PkOrderId { get; set; }

    public int OrderNumber { get; set; }

    public string OrderStatus { get; set; } = null!;

    public string? Price { get; set; }

    public decimal? Quantity { get; set; }

    public string Code { get; set; } = null!;

    public string? ShortName { get; set; }

    public string? OrderTotal { get; set; }

    public Guid PkOrderItemId { get; set; }

    public bool? AddedToBasketFromPlan { get; set; }

    public string? DeliveryAddress { get; set; }

    public string? OrderDate { get; set; }

    public string Vendor { get; set; } = null!;

    public string? ExtRef { get; set; }
}
