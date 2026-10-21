using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VProductEntity
{
    [Column("PkProductID")]
    public Guid PkProductId { get; set; }

    [Column("FkCategoryID")]
    public Guid? FkCategoryId { get; set; }

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    [Column("FkBaseProductID")]
    public Guid? FkBaseProductId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Price { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ShortName { get; set; }

    [StringLength(2500)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [StringLength(2500)]
    [Unicode(false)]
    public string? Specification { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Size { get; set; }

    public int? NumberInPackage { get; set; }

    [Column("FkUnitID")]
    public Guid? FkUnitId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? ImageLocation { get; set; }

    public bool? Active { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Supplier { get; set; }

    public Guid SupplierStatus { get; set; }

    [Column("FkProductStatusID")]
    public Guid FkProductStatusId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string? MarkupType { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? MarkupValue { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ContribType { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? ContribValue { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }
}


