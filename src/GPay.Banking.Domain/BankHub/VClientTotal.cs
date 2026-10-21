using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VClientTotal
{
    [Column(TypeName = "decimal(38, 6)")]
    public decimal? Expr1 { get; set; }

    [Column(TypeName = "decimal(38, 6)")]
    public decimal? Expr2 { get; set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    [Column("FkOrderTypeID")]
    public Guid? FkOrderTypeId { get; set; }

    [Column("FkFromAccountID")]
    public Guid? FkFromAccountId { get; set; }

    [Column("FkOrderID")]
    public Guid FkOrderId { get; set; }

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal MarkupValue { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal ContribValue { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string? MarkupType { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ContribType { get; set; }

    public bool? MarkupInclVat { get; set; }

    public bool? ContributionInclVat { get; set; }

    public bool? InclVat { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Quantity { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? DeliveredQuantity { get; set; }

    [Column("VATExemption")]
    public bool? Vatexemption { get; set; }

    [Column("VATRule")]
    public bool? Vatrule { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? BasePrice { get; set; }
}


