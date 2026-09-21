using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AbpClaimType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Required { get; set; }

    public bool IsStatic { get; set; }

    public string? Regex { get; set; }

    public string? RegexDescription { get; set; }

    public string? Description { get; set; }

    public int ValueType { get; set; }

    public DateTime CreationTime { get; set; }

    public string ExtraProperties { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}
