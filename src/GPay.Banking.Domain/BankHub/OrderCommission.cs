using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OrderCommission")]
public partial class OrderCommission : Entity<Guid>
{
    [Key]
    [Column("PkOrderCommissionID")]
    public override Guid Id { get; protected set; }

    [Column("FkOrderID")]
    public Guid FkOrderId { get; set; }

    [Column("FkAgentID")]
    public Guid FkAgentId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Value { get; set; }

    [Column("FkCommissionTypeID")]
    public Guid FkCommissionTypeId { get; set; }

    public int Order { get; set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    public bool? VatRegistered { get; set; }

    [ForeignKey("FkAgentId")]
    [InverseProperty("OrderCommissionFkAgents")]
    public virtual CustomEntity FkAgent { get; set; } = null!;

    [ForeignKey("FkClientId")]
    [InverseProperty("OrderCommissionFkClients")]
    public virtual CustomEntity FkClient { get; set; } = null!;

    [ForeignKey("FkCommissionTypeId")]
    [InverseProperty("OrderCommissions")]
    public virtual CommissionType FkCommissionType { get; set; } = null!;

    [ForeignKey("FkOrderId")]
    [InverseProperty("OrderCommissions")]
    public virtual Order FkOrder { get; set; } = null!;
}


