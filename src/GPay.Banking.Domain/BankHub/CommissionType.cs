using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("CommissionType")]
public partial class CommissionType : Entity<Guid>
{
    [Key]
    [Column("PkCommissionTypeID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("FkCommissionType")]
    public virtual ICollection<OrderCommission> OrderCommissions { get; set; } = new List<OrderCommission>();

    [InverseProperty("FkCommissionType")]
    public virtual ICollection<OrderItemCommission> OrderItemCommissions { get; set; } = new List<OrderItemCommission>();

    [InverseProperty("FkCommissionType")]
    public virtual ICollection<ProductCommission> ProductCommissions { get; set; } = new List<ProductCommission>();

    [InverseProperty("FkCommissionType")]
    public virtual ICollection<ProductZoneCommission> ProductZoneCommissions { get; set; } = new List<ProductZoneCommission>();

    [InverseProperty("FkCommissionType")]
    public virtual ICollection<SupplierCommission> SupplierCommissions { get; set; } = new List<SupplierCommission>();
}


