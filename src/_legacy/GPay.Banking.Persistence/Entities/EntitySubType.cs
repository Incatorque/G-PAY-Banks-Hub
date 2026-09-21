using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntitySubType
{
    public Guid PkEntitySubTypeId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<EntityPending> EntityPendings { get; set; } = new List<EntityPending>();
}
