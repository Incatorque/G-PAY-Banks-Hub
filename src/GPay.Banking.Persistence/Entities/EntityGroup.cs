using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityGroup
{
    public Guid PkEntityGroupId { get; set; }

    public Guid FkEntityId { get; set; }

    public Guid FkGroupId { get; set; }

    public virtual Entity FkEntity { get; set; } = null!;

    public virtual Group FkGroup { get; set; } = null!;
}
