using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class IpAddress
{
    public Guid PkIpAddressId { get; set; }

    public string IpAddress1 { get; set; } = null!;

    public DateTime DateAdded { get; set; }

    public bool? Active { get; set; }

    public string? Description { get; set; }

    public bool? InUse { get; set; }
}
