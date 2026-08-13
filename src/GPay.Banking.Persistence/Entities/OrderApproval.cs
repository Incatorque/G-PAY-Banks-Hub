using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OrderApproval
{
    public Guid PkOrderApprovalId { get; set; }

    public Guid FkOrderId { get; set; }

    public Guid FkUserId { get; set; }

    public Guid FkApprovalStatusTypeId { get; set; }

    public string? AdditionalInfo { get; set; }

    public int Order { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public Guid? FkOrderStatusId { get; set; }

    public virtual ApprovalStatusType FkApprovalStatusType { get; set; } = null!;

    public virtual Order FkOrder { get; set; } = null!;

    public virtual OrderStatus? FkOrderStatus { get; set; }

    public virtual User FkUser { get; set; } = null!;
}
