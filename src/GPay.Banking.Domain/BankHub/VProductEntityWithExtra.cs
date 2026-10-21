using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VProductEntityWithExtra
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

    [StringLength(250)]
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

    [StringLength(20)]
    [Unicode(false)]
    public string? ContribType { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? MarkupValue { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? ContribValue { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Unit { get; set; } = null!;

    public int? RoundingDecimals { get; set; }

    [Column("VATIncl")]
    public bool? Vatincl { get; set; }

    [Column("VATRule")]
    public bool? Vatrule { get; set; }

    public bool? AlwaysCustomPrice { get; set; }

    public bool? CustomPriceMore { get; set; }

    public bool? CustomPriceLess { get; set; }

    public bool? ContributionInclVat { get; set; }

    public bool? MarkupInclVat { get; set; }
}


