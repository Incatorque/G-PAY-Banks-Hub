using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VEntityOrderSweep
{
    public int PkEntityOrderSweepId { get; set; }

    public Guid PkEntityId { get; set; }

    public string Entity { get; set; } = null!;

    public Guid FromAccountId { get; set; }

    public string? FromAccount { get; set; }

    public string FromAccountNo { get; set; } = null!;

    public string OrderStatus { get; set; } = null!;

    public int Days { get; set; }

    public DateTime? LastProcessed { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public bool? Active { get; set; }

    public bool? Deleted { get; set; }

    public bool? DeletedEntiySweep { get; set; }
}
