using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VSubscription
{
    public Guid PkSubscriptionId { get; set; }

    public string? Taname { get; set; }

    public string Subject { get; set; } = null!;

    public string? Destination { get; set; }

    public bool? Active { get; set; }
}
