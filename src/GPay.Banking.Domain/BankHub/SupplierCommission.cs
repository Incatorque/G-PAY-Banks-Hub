using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("SupplierCommission")]
public partial class SupplierCommission : Entity<Guid>
{
    [Key]
    [Column("PkSupplierCommissionID")]
    public override Guid Id { get; protected set; }

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    [Column("FkAgentID")]
    public Guid FkAgentId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Value { get; set; }

    [Column("FkCommissionTypeID")]
    public Guid FkCommissionTypeId { get; set; }

    public int Order { get; set; }

    [Column("FkClientID")]
    public Guid? FkClientId { get; set; }

    [ForeignKey("FkAgentId")]
    [InverseProperty("SupplierCommissionFkAgents")]
    public virtual CustomEntity FkAgent { get; set; } = null!;

    [ForeignKey("FkClientId")]
    [InverseProperty("SupplierCommissionFkClients")]
    public virtual CustomEntity? FkClient { get; set; }

    [ForeignKey("FkCommissionTypeId")]
    [InverseProperty("SupplierCommissions")]
    public virtual CommissionType FkCommissionType { get; set; } = null!;

    [ForeignKey("FkSupplierId")]
    [InverseProperty("SupplierCommissionFkSuppliers")]
    public virtual CustomEntity FkSupplier { get; set; } = null!;
}


