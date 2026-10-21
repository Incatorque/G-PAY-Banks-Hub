using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OrderItemCommission")]
public partial class OrderItemCommission : Entity<Guid>
{
    [Key]
    [Column("PkOrderItemCommissionID")]
    public override Guid Id { get; protected set; }

    [Column("FkOrderItemID")]
    public Guid FkOrderItemId { get; set; }

    [Column("FkAgentID")]
    public Guid FkAgentId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Value { get; set; }

    [Column("FkCommissionTypeID")]
    public Guid FkCommissionTypeId { get; set; }

    public int Order { get; set; }

    public bool? VatRegistered { get; set; }

    [ForeignKey("FkAgentId")]
    [InverseProperty("OrderItemCommissions")]
    public virtual CustomEntity FkAgent { get; set; } = null!;

    [ForeignKey("FkCommissionTypeId")]
    [InverseProperty("OrderItemCommissions")]
    public virtual CommissionType FkCommissionType { get; set; } = null!;

    [ForeignKey("FkOrderItemId")]
    [InverseProperty("OrderItemCommissions")]
    public virtual OrderItem FkOrderItem { get; set; } = null!;
}


