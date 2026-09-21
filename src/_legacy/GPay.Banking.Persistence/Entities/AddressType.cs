using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AddressType
{
    public Guid PkAddressTypeId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<EntityAddressPending> EntityAddressPendings { get; set; } = new List<EntityAddressPending>();

    public virtual ICollection<EntityAddress> EntityAddresses { get; set; } = new List<EntityAddress>();
}
