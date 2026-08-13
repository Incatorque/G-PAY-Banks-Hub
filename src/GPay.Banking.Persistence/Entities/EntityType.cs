using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityType
{
    public Guid PkEntityTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string? DefaultLink { get; set; }

    public virtual ICollection<EntityEntityType> EntityEntityTypes { get; set; } = new List<EntityEntityType>();

    public virtual ICollection<EtguuserType> EtguuserTypes { get; set; } = new List<EtguuserType>();

    public virtual ICollection<SupplierClient> SupplierClients { get; set; } = new List<SupplierClient>();
}
