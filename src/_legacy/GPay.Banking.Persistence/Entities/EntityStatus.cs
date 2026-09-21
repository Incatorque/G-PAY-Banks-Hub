using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityStatus
{
    public Guid PkStatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EntityPending> EntityPendings { get; set; } = new List<EntityPending>();
}
