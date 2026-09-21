using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ApprovalStatusType
{
    public Guid PkApprovalStatusTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EntityRecordApproval> EntityRecordApprovals { get; set; } = new List<EntityRecordApproval>();

    public virtual ICollection<OrderApproval> OrderApprovals { get; set; } = new List<OrderApproval>();

    public virtual ICollection<OrderItemApproval> OrderItemApprovals { get; set; } = new List<OrderItemApproval>();
}
