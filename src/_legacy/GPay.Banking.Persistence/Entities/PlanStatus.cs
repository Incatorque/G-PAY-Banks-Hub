using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class PlanStatus
{
    public Guid PkPlanStatusId { get; set; }

    public string Name { get; set; } = null!;
}
