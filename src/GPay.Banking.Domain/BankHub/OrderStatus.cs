using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OrderStatus")]
public partial class OrderStatus : Entity<Guid>
{
    [Key]
    [Column("PkOrderStatusID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("iOrder")]
    public int? IOrder { get; set; }

    public bool? ApprovalStatus { get; set; }

    public bool? SysStatus { get; set; }

    [Column("OrderStatusID")]
    public int OrderStatusId { get; set; }

    [InverseProperty("FkOrderStatus")]
    public virtual ICollection<ClientApproval> ClientApprovals { get; set; } = new List<ClientApproval>();

    [InverseProperty("FkOrderStatus")]
    public virtual ICollection<ClientProductApproval> ClientProductApprovals { get; set; } = new List<ClientProductApproval>();

    [InverseProperty("FkOrderStatus")]
    public virtual ICollection<EntityOrderSweep> EntityOrderSweeps { get; set; } = new List<EntityOrderSweep>();

    [InverseProperty("FkOrderStatus")]
    public virtual ICollection<OrderApproval> OrderApprovals { get; set; } = new List<OrderApproval>();

    [InverseProperty("FkOrderStatus")]
    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

    [InverseProperty("FkOrderStatus")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}


