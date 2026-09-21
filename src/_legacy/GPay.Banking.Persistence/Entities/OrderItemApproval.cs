using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OrderItemApproval
{
    public Guid PkOrderItemApprovalId { get; set; }

    public Guid FkOrderItemId { get; set; }

    public Guid FkUserId { get; set; }

    public Guid FkApprovalStatusTypeId { get; set; }

    public string? AdditionalInfo { get; set; }

    public int Order { get; set; }

    public virtual ApprovalStatusType FkApprovalStatusType { get; set; } = null!;

    public virtual OrderItem FkOrderItem { get; set; } = null!;

    public virtual User FkUser { get; set; } = null!;
}
