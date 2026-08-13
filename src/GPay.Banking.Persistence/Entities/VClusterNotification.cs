using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VClusterNotification
{
    public Guid PkClusterId { get; set; }

    public string Name { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string? Destination { get; set; }

    public bool? Active { get; set; }
}
