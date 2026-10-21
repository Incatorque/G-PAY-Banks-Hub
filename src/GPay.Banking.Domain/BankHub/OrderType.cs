using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OrderType")]
public partial class OrderType : Entity<Guid>
{
    [Key]
    [Column("PkOrderTypeID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [InverseProperty("FkOrderType")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}


