using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AbpSettingDefinition
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string? Description { get; set; }

    public string? DefaultValue { get; set; }

    public bool IsVisibleToClients { get; set; }

    public string? Providers { get; set; }

    public bool IsInherited { get; set; }

    public bool IsEncrypted { get; set; }

    public string? ExtraProperties { get; set; }
}
