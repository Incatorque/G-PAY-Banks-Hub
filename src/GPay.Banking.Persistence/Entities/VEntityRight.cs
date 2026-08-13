using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VEntityRight
{
    public Guid PkModuleId { get; set; }

    public string Name { get; set; } = null!;

    public Guid? PkEntityId { get; set; }

    public Guid? PkGroupId { get; set; }

    public Guid? PkUserId { get; set; }
}
