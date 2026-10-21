using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ProductZone")]
[Index("Active", Name = "Index_ProductZone_01")]
[Index("Active", "FkProductSupplierId", Name = "Index_ProductZone_02")]
public partial class ProductZone : Entity<Guid>
{
    [Key]
    [Column("PkProductZoneID")]
    public override Guid Id { get; protected set; }

    [Column("FkSupplierZoneID")]
    public Guid? FkSupplierZoneId { get; set; }

    [Column("FkGroupID")]
    public Guid? FkGroupId { get; set; }

    [Column("FkClientID")]
    public Guid? FkClientId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EffectiveDate { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Differential { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Discount { get; set; }

    [Column("FkDiscountTypeID")]
    public Guid? FkDiscountTypeId { get; set; }

    public bool? Levy { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? MinPurchaseQty { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? MaxPurchaseQty { get; set; }

    public bool? ParentCommission { get; set; }

    [Column("FkDifferentialTypeID")]
    public Guid FkDifferentialTypeId { get; set; }

    public bool? Active { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? OverwritePrice { get; set; }

    [Column("FkProductSupplierID")]
    public Guid FkProductSupplierId { get; set; }

    [ForeignKey("FkDifferentialTypeId")]
    [InverseProperty("ProductZones")]
    public virtual DifferentialType FkDifferentialType { get; set; } = null!;

    [ForeignKey("FkDiscountTypeId")]
    [InverseProperty("ProductZones")]
    public virtual DiscountType? FkDiscountType { get; set; }

    [ForeignKey("FkProductSupplierId")]
    [InverseProperty("ProductZones")]
    public virtual ProductSupplier FkProductSupplier { get; set; } = null!;

    [ForeignKey("FkSupplierZoneId")]
    [InverseProperty("ProductZones")]
    public virtual SupplierZone? FkSupplierZone { get; set; }

    [InverseProperty("FkProductZone")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("FkProductZone")]
    public virtual ICollection<ProductAllocationOverridge> ProductAllocationOverridges { get; set; } = new List<ProductAllocationOverridge>();

    [InverseProperty("FkProductZone")]
    public virtual ICollection<ProductZoneCommission> ProductZoneCommissions { get; set; } = new List<ProductZoneCommission>();
}


