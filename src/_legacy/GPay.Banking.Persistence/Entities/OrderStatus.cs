using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OrderStatus
{
    public Guid PkOrderStatusId { get; set; }

    public string Name { get; set; } = null!;

    public int? IOrder { get; set; }

    public bool? ApprovalStatus { get; set; }

    public bool? SysStatus { get; set; }

    public int OrderStatusId { get; set; }

    public virtual ICollection<ClientApproval> ClientApprovals { get; set; } = new List<ClientApproval>();

    public virtual ICollection<ClientProductApproval> ClientProductApprovals { get; set; } = new List<ClientProductApproval>();

    public virtual ICollection<EntityOrderSweep> EntityOrderSweeps { get; set; } = new List<EntityOrderSweep>();

    public virtual ICollection<OrderApproval> OrderApprovals { get; set; } = new List<OrderApproval>();

    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
