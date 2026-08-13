using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityRecordApproval
{
    public Guid PkentityRecordApprovalId { get; set; }

    public Guid FkentityId { get; set; }

    public Guid FkuserId { get; set; }

    public Guid FkApprovalStatusTypeId { get; set; }

    public int Order { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public bool? Active { get; set; }

    public Guid FkentityDataApprovalId { get; set; }

    public virtual ApprovalStatusType FkApprovalStatusType { get; set; } = null!;

    public virtual Entity Fkentity { get; set; } = null!;

    public virtual EntityDataApproval FkentityDataApproval { get; set; } = null!;

    public virtual User Fkuser { get; set; } = null!;
}
