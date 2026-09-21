using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VBasket
{
    public Guid PkOrderItemId { get; set; }

    public Guid FkOrderId { get; set; }

    public decimal? Quantity { get; set; }

    public string? ProductName { get; set; }

    public string SupplierName { get; set; } = null!;

    public Guid PkProductId { get; set; }

    public decimal? Price { get; set; }

    public decimal? Discount { get; set; }

    public Guid FkOrderStatusId { get; set; }

    public int OrderNumber { get; set; }

    public bool? Active { get; set; }
}
