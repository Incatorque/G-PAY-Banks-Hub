using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AbpBlobContainer
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string ExtraProperties { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;

    public virtual ICollection<AbpBlob> AbpBlobs { get; set; } = new List<AbpBlob>();
}
