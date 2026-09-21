using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AbpTenantConnectionString
{
    public Guid TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    public virtual AbpTenant Tenant { get; set; } = null!;
}
