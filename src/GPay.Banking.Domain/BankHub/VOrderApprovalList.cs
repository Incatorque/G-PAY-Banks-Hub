using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VOrderApprovalList
{
    [Column("PkOrderApprovalID")]
    public Guid PkOrderApprovalId { get; set; }

    [Column("PkOrderID")]
    public Guid PkOrderId { get; set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    [Column("FkClientAddressID")]
    public Guid FkClientAddressId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [StringLength(9)]
    [Unicode(false)]
    public string? OrderNumber { get; set; }

    [Column("FkOrderStatusID")]
    public Guid FkOrderStatusId { get; set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ClientName { get; set; } = null!;

    [Column("ClientTAName")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ClientTaname { get; set; }

    public bool? Active { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string SupplierName { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal? RequisitionAmount { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ReferenceNumber { get; set; }

    [StringLength(183)]
    public string? BeneficiaryAccountInfo { get; set; }

    [Column("FkOwnerEntityID")]
    public Guid? FkOwnerEntityId { get; set; }

    [Column("PkGroupID")]
    public Guid? PkGroupId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SupplierBankReference { get; set; }

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }
}


