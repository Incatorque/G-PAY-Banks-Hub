using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ProductCommission")]
public partial class ProductCommission : Entity<Guid>
{
    [Key]
    [Column("PkProductCommissionID")]
    public override Guid Id { get; protected set; }

    [Column("FkAgentID")]
    public Guid FkAgentId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Value { get; set; }

    [Column("FkCommissionTypeID")]
    public Guid FkCommissionTypeId { get; set; }

    public int Order { get; set; }

    [Column("FkProductSupplierID")]
    public Guid FkProductSupplierId { get; set; }

    [ForeignKey("FkAgentId")]
    [InverseProperty("ProductCommissions")]
    public virtual CustomEntity FkAgent { get; set; } = null!;

    [ForeignKey("FkCommissionTypeId")]
    [InverseProperty("ProductCommissions")]
    public virtual CommissionType FkCommissionType { get; set; } = null!;

    [ForeignKey("FkProductSupplierId")]
    [InverseProperty("ProductCommissions")]
    public virtual ProductSupplier FkProductSupplier { get; set; } = null!;
}


