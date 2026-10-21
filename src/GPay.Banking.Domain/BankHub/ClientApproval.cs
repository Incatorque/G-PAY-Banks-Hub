using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ClientApproval")]
public partial class ClientApproval : Entity<Guid>
{
    [Key]
    [Column("PkClientApprovalID")]
    public override Guid Id { get; protected set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [Column("FkOrderStatusID")]
    public Guid? FkOrderStatusId { get; set; }

    [Column("FkOrderUserID")]
    public Guid? FkOrderUserId { get; set; }

    [Column("iOrder")]
    public int IOrder { get; set; }

    [ForeignKey("FkClientId")]
    [InverseProperty("ClientApprovals")]
    public virtual CustomEntity FkClient { get; set; } = null!;

    [ForeignKey("FkOrderStatusId")]
    [InverseProperty("ClientApprovals")]
    public virtual OrderStatus? FkOrderStatus { get; set; }

    [ForeignKey("FkOrderUserId")]
    [InverseProperty("ClientApprovalFkOrderUsers")]
    public virtual User? FkOrderUser { get; set; }

    [ForeignKey("FkUserId")]
    [InverseProperty("ClientApprovalFkUsers")]
    public virtual User FkUser { get; set; } = null!;
}


