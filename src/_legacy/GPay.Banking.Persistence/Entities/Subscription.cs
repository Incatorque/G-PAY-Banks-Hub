using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Subscription
{
    public Guid PkSubscriptionId { get; set; }

    public Guid? FkEntityId { get; set; }

    public Guid? FkMailtemplateId { get; set; }

    public string? Destination { get; set; }

    public bool? Active { get; set; }
}
