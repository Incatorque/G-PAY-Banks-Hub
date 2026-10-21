using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VOrderItemGbolist
{
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

    [Column("GRNReference")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Grnreference { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ReferenceNumber { get; set; }

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

    [StringLength(50)]
    [Unicode(false)]
    public string ExtRef { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Address1 { get; set; } = null!;

    public int? TotalItems { get; set; }

    [Column(TypeName = "decimal(38, 6)")]
    public decimal? SumCommissionExclVat { get; set; }

    [Column(TypeName = "decimal(38, 6)")]
    public decimal? SumExclComm { get; set; }

    [Column("SumVATIncl", TypeName = "decimal(38, 6)")]
    public decimal? SumVatincl { get; set; }

    [Column("SumVAT", TypeName = "numeric(38, 6)")]
    public decimal? SumVat { get; set; }

    [Column("FkOrderID")]
    public Guid FkOrderId { get; set; }

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    [Column("FkOrderStatusID")]
    public Guid FkOrderStatusId { get; set; }

    [Column("FkClientAddressID")]
    public Guid FkClientAddressId { get; set; }

    [Column("FkAllocatedVendorID")]
    public Guid? FkAllocatedVendorId { get; set; }

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

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? DeliveredQuantity { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string InstantPayment { get; set; } = null!;

    public Guid? FkPlanId { get; set; }

    [Column("FkOutwardBOPCategoryId")]
    public Guid? FkOutwardBopcategoryId { get; set; }
}


