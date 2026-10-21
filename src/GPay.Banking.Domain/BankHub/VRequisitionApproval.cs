using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VRequisitionApproval
{
    [Column("PkOrderApprovalID")]
    public Guid PkOrderApprovalId { get; set; }

    [Column("PkOrderID")]
    public Guid PkOrderId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? OrderNumber { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ClientName { get; set; }

    [Column("ClientTAName")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ClientTaname { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? SupplierName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [Column(TypeName = "decimal(38, 6)")]
    public decimal? RequisitionAmount { get; set; }

    [StringLength(183)]
    public string? BeneficiaryAccountInfo { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ReferenceNumber { get; set; }

    [Column("ApproverUserID")]
    public Guid ApproverUserId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ApproverUser { get; set; }

    [Column("GroupID")]
    public Guid? GroupId { get; set; }

    [Column("PkClientID")]
    public Guid PkClientId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string OrderStatus { get; set; } = null!;

    [Column("FkOrderStatusID")]
    public Guid FkOrderStatusId { get; set; }

    [Column("PkSupplierID")]
    public Guid PkSupplierId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ApprovalDate { get; set; }

    public int Order { get; set; }

    public bool? PreviousLevelIsApproved { get; set; }
}


