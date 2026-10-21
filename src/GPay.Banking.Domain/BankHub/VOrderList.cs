using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VOrderList
{
    [Column("PkOrderID")]
    public Guid PkOrderId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ClientName { get; set; }

    [Column("ClientTA")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ClientTa { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? OrderNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string HasAttachment { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [Column("FkOrderStatusID")]
    public Guid FkOrderStatusId { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string InstantPayment { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? SupplierName { get; set; }

    [Column("PkSupplierID")]
    public Guid PkSupplierId { get; set; }

    [Column("PkClientID")]
    public Guid PkClientId { get; set; }

    [Column("SumVATIncl", TypeName = "decimal(38, 6)")]
    public decimal? SumVatincl { get; set; }

    public bool? ForwardGuarantee { get; set; }

    [Column("PkForwardedID")]
    public Guid? PkForwardedId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? AllocatedName { get; set; }

    [Column("FkLinkedOrderID")]
    public Guid? FkLinkedOrderId { get; set; }

    [Column("GroupID")]
    public Guid? GroupId { get; set; }

    [Column("FkOrderTypeID")]
    public Guid? FkOrderTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? InvoiceNumber { get; set; }

    [Column("FkFromAccountID")]
    public Guid? FkFromAccountId { get; set; }

    [Column("FkToAccountID")]
    public Guid? FkToAccountId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Receipt { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ReferenceNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SupplierBankReference { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? AllocatedNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeliveryDate { get; set; }

    [Column("GRNReference")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Grnreference { get; set; }

    public string? ClientTypes { get; set; }

    public string? SupplierTypes { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string DeliveryAddress { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ExtRef { get; set; } = null!;

    public Guid? FkPlanId { get; set; }

    [Column("BOPCode")]
    [Unicode(false)]
    public string Bopcode { get; set; } = null!;
}


