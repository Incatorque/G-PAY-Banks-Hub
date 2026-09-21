using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OrderItemReference
{
    public Guid PkOrderItemReference { get; set; }

    public Guid FkOrderItem { get; set; }

    public string Reference { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public bool? Active { get; set; }

    public virtual OrderItem FkOrderItemNavigation { get; set; } = null!;
}
