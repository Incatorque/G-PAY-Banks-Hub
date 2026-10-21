using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VProductEntityAllAvailableProduct
{
    [Column("PkProductID")]
    public Guid PkProductId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? ShortName { get; set; }

    [StringLength(2500)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    public bool? AlwaysCustomPrice { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Price { get; set; }

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

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Unit { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Supplier { get; set; }

    public int? RoundingDecimals { get; set; }

    [Column("VATIncl")]
    public bool? Vatincl { get; set; }

    [Column("VATRule")]
    public bool? Vatrule { get; set; }

    public bool? CustomPriceMore { get; set; }

    public bool? CustomPriceLess { get; set; }

    public bool? ContributionInclVat { get; set; }

    public bool? MarkupInclVat { get; set; }
}


