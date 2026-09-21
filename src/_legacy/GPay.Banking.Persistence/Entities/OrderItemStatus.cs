using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OrderItemStatus
{
    public Guid PkOrderItemStatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
