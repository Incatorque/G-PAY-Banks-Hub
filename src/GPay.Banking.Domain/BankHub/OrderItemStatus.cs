using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OrderItemStatus")]
public partial class OrderItemStatus : Entity<Guid>
{
    [Key]
    [Column("PkOrderItemStatusID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("FkOrderStatus")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}


