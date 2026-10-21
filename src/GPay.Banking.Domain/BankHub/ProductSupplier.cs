using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ProductSupplier")]
[Index("FkSupplierId", "FkProductId", "Active", "Deleted", Name = "IX_Product_Search_ProductSupplier_Lookup")]
[Index("Active", Name = "Index_ProductSupplier_01")]
[Index("FkSupplierId", "Active", Name = "Index_ProductSupplier_02")]
public partial class ProductSupplier : Entity<Guid>
{
    [Key]
    [Column("PkProductSupplierID")]
    public override Guid Id { get; protected set; }

    [Column("FkProductID")]
    public Guid FkProductId { get; set; }

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    public bool? Active { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column("FkProductStatusID")]
    public Guid FkProductStatusId { get; set; }

    [Column("FkAccountID")]
    public Guid? FkAccountId { get; set; }

    public bool? Deleted { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? ImageLocation { get; set; }

    [ForeignKey("FkProductId")]
    [InverseProperty("ProductSuppliers")]
    public virtual Product FkProduct { get; set; } = null!;

    [ForeignKey("FkSupplierId")]
    [InverseProperty("ProductSuppliers")]
    public virtual CustomEntity FkSupplier { get; set; } = null!;

    [InverseProperty("FkProductSupplier")]
    public virtual ICollection<ProductCommission> ProductCommissions { get; set; } = new List<ProductCommission>();

    [InverseProperty("FkProductSupplier")]
    public virtual ICollection<ProductZone> ProductZones { get; set; } = new List<ProductZone>();
}


