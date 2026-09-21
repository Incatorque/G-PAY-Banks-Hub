using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AbpPermission
{
    public Guid Id { get; set; }

    public string GroupName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ParentName { get; set; }

    public string DisplayName { get; set; } = null!;

    public bool IsEnabled { get; set; }

    public byte MultiTenancySide { get; set; }

    public string? Providers { get; set; }

    public string? StateCheckers { get; set; }

    public string? ExtraProperties { get; set; }
}
