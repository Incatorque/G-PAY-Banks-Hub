using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Plan
{
    public Guid PkPlanId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public Guid FkEntityId { get; set; }

    public Guid FkAccountId { get; set; }

    public DateTime ExpirationDate { get; set; }

    public Guid FkPlanStatusId { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid FkOrderId { get; set; }

    public bool? Active { get; set; }

    public DateTime? LastRequisitionDate { get; set; }
}
