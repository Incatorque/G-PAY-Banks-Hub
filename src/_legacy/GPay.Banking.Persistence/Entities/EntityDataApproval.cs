using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityDataApproval
{
    public Guid PkEntityApprovalId { get; set; }

    public Guid? FkEntityId { get; set; }

    public Guid FkUserId { get; set; }

    public int IOrder { get; set; }

    public Guid? FkFromUserId { get; set; }

    public bool? New { get; set; }

    public bool? Edit { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<EntityRecordApproval> EntityRecordApprovals { get; set; } = new List<EntityRecordApproval>();

    public virtual Entity? FkEntity { get; set; }

    public virtual User? FkFromUser { get; set; }

    public virtual User FkUser { get; set; } = null!;
}
