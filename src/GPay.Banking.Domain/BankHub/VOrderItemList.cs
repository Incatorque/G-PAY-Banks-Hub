using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VOrderItemList
{
    [Column(TypeName = "decimal(38, 6)")]
    public decimal? PriceExclComm { get; set; }

    [Column(TypeName = "decimal(38, 6)")]
    public decimal? TotalExclComm { get; set; }

    [Column(TypeName = "decimal(38, 6)")]
    public decimal? CommissionExclVat { get; set; }

    [Column(TypeName = "decimal(38, 6)")]
    public decimal? CommissionVat { get; set; }

    [Column(TypeName = "decimal(38, 6)")]
    public decimal? CommissionInclVat { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? OrderNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Receipt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeliveryDate { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? DeliveredQuantity { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? ShortName { get; set; }

    [StringLength(2500)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Quantity { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Price { get; set; }

    [Column("VAT", TypeName = "numeric(38, 6)")]
    public decimal? Vat { get; set; }

    [Column("VATIncl", TypeName = "decimal(38, 6)")]
    public decimal? Vatincl { get; set; }

    [Column("OrderTotalVATIncl", TypeName = "decimal(38, 6)")]
    public decimal? OrderTotalVatincl { get; set; }

    [Column(TypeName = "decimal(38, 7)")]
    public decimal? Total { get; set; }

    [Column(TypeName = "decimal(38, 4)")]
    public decimal TotalPercComm { get; set; }

    [Column(TypeName = "decimal(38, 4)")]
    public decimal TotalFixedComm { get; set; }

    public int? RoundingDecimals { get; set; }

    [Column("GRNReference")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Grnreference { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ReferenceNumber { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string InstantPayment { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? SupplierBankReference { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? InvoiceNumber { get; set; }

    public int? BankSequence { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BankUserCode { get; set; }

    [Column("ProductAllocationQTY", TypeName = "decimal(19, 4)")]
    public decimal? ProductAllocationQty { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProductAllocationTerms { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? AllocationOverridgeQty { get; set; }

    [Column(TypeName = "decimal(38, 6)")]
    public decimal? OverridgeQty { get; set; }

    [Column(TypeName = "decimal(38, 6)")]
    public decimal? TotalQuantity { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? OverridgeTerms { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ExtRef { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Address1 { get; set; } = null!;

    public bool? AddedToBasketFromPlan { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ClientName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string SupplierName { get; set; } = null!;

    [Column("PkOrderItemID")]
    public Guid PkOrderItemId { get; set; }

    [Column("FkOrderID")]
    public Guid FkOrderId { get; set; }

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    [Column("FkAllocatedVendorID")]
    public Guid? FkAllocatedVendorId { get; set; }

    [Column("PkProductID")]
    public Guid PkProductId { get; set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    [Column("FkClientAddressID")]
    public Guid FkClientAddressId { get; set; }

    [Column("FkOrderStatusID")]
    public Guid FkOrderStatusId { get; set; }

    [Column("FkLinkedOrderID")]
    public Guid? FkLinkedOrderId { get; set; }

    [Column("GroupID")]
    public Guid? GroupId { get; set; }

    [Column("FkOrderTypeID")]
    public Guid? FkOrderTypeId { get; set; }

    [Column("FkFromAccountID")]
    public Guid? FkFromAccountId { get; set; }

    [Column("FkToAccountID")]
    public Guid? FkToAccountId { get; set; }

    [Column("FkFromBankID")]
    public Guid FkFromBankId { get; set; }

    public Guid? FkPlanId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ContribType { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? MarkupType { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? ContribValue { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? MarkupValue { get; set; }

    public bool? ContributionInclVat { get; set; }

    public bool? MarkupInclVat { get; set; }

    public bool? InclVat { get; set; }

    public Guid? FkFromProductId { get; set; }

    [Column("VATExemption")]
    public bool? Vatexemption { get; set; }

    [Column("VATRule")]
    public bool? Vatrule { get; set; }

    [Column("FkOutwardBOPCategoryId")]
    public Guid? FkOutwardBopcategoryId { get; set; }
}


