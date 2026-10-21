using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ClientProductApproval")]
public partial class ClientProductApproval : Entity<Guid>
{
    [Key]
    [Column("PkClientProductApprovalID")]
    public override Guid Id { get; protected set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [Column("FkProductID")]
    public Guid? FkProductId { get; set; }

    [Column("FkOrderStatusID")]
    public Guid? FkOrderStatusId { get; set; }

    [Column("iOrder")]
    public int IOrder { get; set; }

    [ForeignKey("FkClientId")]
    [InverseProperty("ClientProductApprovals")]
    public virtual CustomEntity FkClient { get; set; } = null!;

    [ForeignKey("FkOrderStatusId")]
    [InverseProperty("ClientProductApprovals")]
    public virtual OrderStatus? FkOrderStatus { get; set; }

    [ForeignKey("FkProductId")]
    [InverseProperty("ClientProductApprovals")]
    public virtual Product? FkProduct { get; set; }

    [ForeignKey("FkUserId")]
    [InverseProperty("ClientProductApprovals")]
    public virtual User FkUser { get; set; } = null!;
}


