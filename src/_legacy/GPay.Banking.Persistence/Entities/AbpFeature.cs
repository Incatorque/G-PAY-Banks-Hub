using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AbpFeature
{
    public Guid Id { get; set; }

    public string GroupName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ParentName { get; set; }

    public string DisplayName { get; set; } = null!;

    public string? Description { get; set; }

    public string? DefaultValue { get; set; }

    public bool IsVisibleToClients { get; set; }

    public bool IsAvailableToHost { get; set; }

    public string? AllowedProviders { get; set; }

    public string? ValueType { get; set; }

    public string? ExtraProperties { get; set; }
}
