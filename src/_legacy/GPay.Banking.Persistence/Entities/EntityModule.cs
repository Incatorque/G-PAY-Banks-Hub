using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityModule
{
    public Guid PkEntityModuleId { get; set; }

    public Guid FkEntityId { get; set; }

    public Guid FkModuleId { get; set; }

    public virtual Entity FkEntity { get; set; } = null!;

    public virtual Module FkModule { get; set; } = null!;
}
