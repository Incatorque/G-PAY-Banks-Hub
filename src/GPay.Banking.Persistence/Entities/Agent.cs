using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Agent
{
    public Guid PkAgentId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<ProductZoneCommission> ProductZoneCommissions { get; set; } = new List<ProductZoneCommission>();
}
