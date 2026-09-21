using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VUserClientAddress
{
    public Guid ClientId { get; set; }

    public Guid FkUserId { get; set; }

    public Guid PkEntityAddressId { get; set; }

    public string Name { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public Guid FkAddressTypeId { get; set; }
}
