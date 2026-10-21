using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VOrderItemListSupplier
{
    [Column("PkOrderItemID")]
    public Guid PkOrderItemId { get; set; }

    [Column("FkOrderID")]
    public Guid FkOrderId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal Quantity { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ShortName { get; set; }

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal DeliveredQuantity { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Price { get; set; }

    [Column(TypeName = "decimal(38, 7)")]
    public decimal Total { get; set; }

    [StringLength(2500)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("VAT", TypeName = "decimal(38, 6)")]
    public decimal Vat { get; set; }

    [Column(TypeName = "decimal(38, 6)")]
    public decimal InclVat { get; set; }

    public bool? Active { get; set; }

    [Column("PkProductID")]
    public Guid PkProductId { get; set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    [Column("FkOrderStatusID")]
    public Guid FkOrderStatusId { get; set; }

    [Column("TAName")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Taname { get; set; }

    public int? RoundingDecimals { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal OverridgeQty { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? OverridgeTerms { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? ImageLocation { get; set; }

    [Column("FkLinkedOrderItemID")]
    public Guid? FkLinkedOrderItemId { get; set; }

    [StringLength(255)]
    public string? Reference { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? ContribValue { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ContribType { get; set; }

    public bool? ContributionInclVat { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? MarkupValue { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? MarkupType { get; set; }

    public bool? MarkupInclVat { get; set; }

    [Column("VATIncl")]
    public bool? Vatincl { get; set; }

    [Column("VATRule")]
    public bool? Vatrule { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    public Guid? ProductCategory { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string ServiceType { get; set; } = null!;

    [Column("BOPCode")]
    [Unicode(false)]
    public string Bopcode { get; set; } = null!;
}


