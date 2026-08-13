using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityEntityType
{
    public Guid PkEntityEntityTypeId { get; set; }

    public Guid FkEntityId { get; set; }

    public Guid FkEntityTypeId { get; set; }

    public virtual Entity FkEntity { get; set; } = null!;

    public virtual EntityType FkEntityType { get; set; } = null!;
}
