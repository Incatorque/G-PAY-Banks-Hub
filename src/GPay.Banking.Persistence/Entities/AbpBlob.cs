using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AbpBlob
{
    public Guid Id { get; set; }

    public Guid ContainerId { get; set; }

    public Guid? TenantId { get; set; }

    public string Name { get; set; } = null!;

    public byte[]? Content { get; set; }

    public string ExtraProperties { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;

    public virtual AbpBlobContainer Container { get; set; } = null!;
}
