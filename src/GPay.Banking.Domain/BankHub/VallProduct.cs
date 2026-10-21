using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VallProduct
{
    [StringLength(250)]
    [Unicode(false)]
    public string Category { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string GridZone { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Size { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Unit { get; set; } = null!;

    [StringLength(2500)]
    [Unicode(false)]
    public string? Specification { get; set; }

    public int? NumberInPackage { get; set; }

    [Column(TypeName = "decimal(38, 6)")]
    public decimal? Price { get; set; }

    public int RoundingDecimals { get; set; }

    public bool? AlwaysCustomPrice { get; set; }

    public bool? CustomPriceLess { get; set; }

    public bool? CustomPriceMore { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal MinOrder { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal MaxOrder { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Supplier { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Client { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Differential { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? DifferentialType { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ProductStatus { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? ShortName { get; set; }

    [Column("ProductAllocationQTY", TypeName = "decimal(19, 4)")]
    public decimal ProductAllocationQty { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ProductAllocationTerms { get; set; } = null!;

    [Column(TypeName = "decimal(19, 4)")]
    public decimal OverridgeQty { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string OverridgeTerms { get; set; } = null!;

    [Column("VATExemption")]
    public bool? Vatexemption { get; set; }

    [StringLength(11)]
    [Unicode(false)]
    public string VatText { get; set; } = null!;

    [StringLength(408)]
    [Unicode(false)]
    public string? ClientAddress { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? ImageLocation { get; set; }

    [Column("SupplierID")]
    public Guid SupplierId { get; set; }

    [Column("ClientID")]
    public Guid ClientId { get; set; }

    [Column("ClientAddressID")]
    public Guid ClientAddressId { get; set; }

    [Column("ProductSupplierID")]
    public Guid ProductSupplierId { get; set; }

    [Column("ProductID")]
    public Guid ProductId { get; set; }

    [Column("CategoryID")]
    public Guid CategoryId { get; set; }

    [Column("FkDifferentialTypeID")]
    public Guid? FkDifferentialTypeId { get; set; }

    [Column("ProductZoneID")]
    public Guid ProductZoneId { get; set; }

    [Column("PkSupplierClientID")]
    public Guid PkSupplierClientId { get; set; }

    [Column("FkEntityTypeID")]
    public Guid? FkEntityTypeId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal ContribValue { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal MarkupValue { get; set; }

    public bool ContributionInclVat { get; set; }

    public bool MarkupInclVat { get; set; }

    [Column("VATIncl")]
    public bool? Vatincl { get; set; }

    [Column("VATRule")]
    public bool? Vatrule { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ContribType { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? MarkupType { get; set; }
}


