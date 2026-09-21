using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VPlanning
{
    public Guid PkPlanId { get; set; }

    public Guid FkAccountId { get; set; }

    public Guid FkEntityId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime ExpirationDate { get; set; }

    public string? Description { get; set; }

    public string PlanStatus { get; set; } = null!;

    public DateTime? LastRequisitionDate { get; set; }

    public decimal? PlanValue { get; set; }

    public Guid FkPlanStatusId { get; set; }

    public DateTime? CreationDate { get; set; }

    public bool? AddedToBasketFromPlan { get; set; }

    public Guid FkOrderStatusId { get; set; }

    public string? PlanNumber { get; set; }

    public bool? Active { get; set; }

    public Guid PkOrderId { get; set; }
}
