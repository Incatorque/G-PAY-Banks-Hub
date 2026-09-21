using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ProductZoneCommission
{
    public Guid PkProductZoneCommissionId { get; set; }

    public Guid FkProductZoneId { get; set; }

    public Guid FkAgentId { get; set; }

    public decimal? Value { get; set; }

    public Guid FkCommissionTypeId { get; set; }

    public int Order { get; set; }

    public virtual Agent FkAgent { get; set; } = null!;

    public virtual Entity FkAgentNavigation { get; set; } = null!;

    public virtual CommissionType FkCommissionType { get; set; } = null!;

    public virtual ProductZone FkProductZone { get; set; } = null!;
}
