using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AbpBackgroundJob
{
    public Guid Id { get; set; }

    public string? ApplicationName { get; set; }

    public string JobName { get; set; } = null!;

    public string JobArgs { get; set; } = null!;

    public short TryCount { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime NextTryTime { get; set; }

    public DateTime? LastTryTime { get; set; }

    public bool? IsAbandoned { get; set; }

    public byte Priority { get; set; }

    public string ExtraProperties { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}
