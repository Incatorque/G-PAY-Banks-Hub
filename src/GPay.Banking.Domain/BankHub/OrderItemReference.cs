using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OrderItemReference")]
public partial class OrderItemReference : Entity<Guid>
{
    [Key]
    [Column("PkOrderItemReference")]
    public override Guid Id { get; protected set; }

    public Guid FkOrderItem { get; set; }

    [StringLength(255)]
    public string Reference { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    public bool? Active { get; set; }

    [ForeignKey("FkOrderItem")]
    [InverseProperty("OrderItemReferences")]
    public virtual OrderItem FkOrderItemNavigation { get; set; } = null!;
}


