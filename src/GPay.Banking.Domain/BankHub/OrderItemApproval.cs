using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OrderItemApproval")]
public partial class OrderItemApproval : Entity<Guid>
{
    [Key]
    [Column("PkOrderItemApprovalID")]
    public override Guid Id { get; protected set; }

    [Column("FkOrderItemID")]
    public Guid FkOrderItemId { get; set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [Column("FkApprovalStatusTypeID")]
    public Guid FkApprovalStatusTypeId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? AdditionalInfo { get; set; }

    public int Order { get; set; }

    [ForeignKey("FkApprovalStatusTypeId")]
    [InverseProperty("OrderItemApprovals")]
    public virtual ApprovalStatusType FkApprovalStatusType { get; set; } = null!;

    [ForeignKey("FkOrderItemId")]
    [InverseProperty("OrderItemApprovals")]
    public virtual OrderItem FkOrderItem { get; set; } = null!;

    [ForeignKey("FkUserId")]
    [InverseProperty("OrderItemApprovals")]
    public virtual User FkUser { get; set; } = null!;
}


