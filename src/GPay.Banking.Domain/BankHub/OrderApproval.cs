using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OrderApproval")]
public partial class OrderApproval : Entity<Guid>
{
    [Key]
    [Column("PkOrderApprovalID")]
    public override Guid Id { get; protected set; }

    [Column("FkOrderID")]
    public Guid FkOrderId { get; set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [Column("FkApprovalStatusTypeID")]
    public Guid FkApprovalStatusTypeId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? AdditionalInfo { get; set; }

    public int Order { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ApprovalDate { get; set; }

    [Column("FkOrderStatusID")]
    public Guid? FkOrderStatusId { get; set; }

    [ForeignKey("FkApprovalStatusTypeId")]
    [InverseProperty("OrderApprovals")]
    public virtual ApprovalStatusType FkApprovalStatusType { get; set; } = null!;

    [ForeignKey("FkOrderId")]
    [InverseProperty("OrderApprovals")]
    public virtual Order FkOrder { get; set; } = null!;

    [ForeignKey("FkOrderStatusId")]
    [InverseProperty("OrderApprovals")]
    public virtual OrderStatus? FkOrderStatus { get; set; }

    [ForeignKey("FkUserId")]
    [InverseProperty("OrderApprovals")]
    public virtual User FkUser { get; set; } = null!;
}


