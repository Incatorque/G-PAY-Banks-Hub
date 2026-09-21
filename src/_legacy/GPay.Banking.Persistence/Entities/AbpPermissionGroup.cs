using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AbpPermissionGroup
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string? ExtraProperties { get; set; }
}
