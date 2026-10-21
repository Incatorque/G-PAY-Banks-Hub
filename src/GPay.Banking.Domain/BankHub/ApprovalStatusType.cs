using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPay.Banking.BankHub;

public partial class ApprovalStatusType
{
    public Guid PkApprovalStatusTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EntityRecordApproval> EntityRecordApprovals { get; set; } = new List<EntityRecordApproval>();

    public virtual ICollection<OrderApproval> OrderApprovals { get; set; } = new List<OrderApproval>();

    public virtual ICollection<OrderItemApproval> OrderItemApprovals { get; set; } = new List<OrderItemApproval>();
}
