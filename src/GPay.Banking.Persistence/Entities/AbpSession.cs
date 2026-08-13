using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AbpSession
{
    public Guid Id { get; set; }

    public string SessionId { get; set; } = null!;

    public string Device { get; set; } = null!;

    public string? DeviceInfo { get; set; }

    public Guid? TenantId { get; set; }

    public Guid UserId { get; set; }

    public string? ClientId { get; set; }

    public string? IpAddresses { get; set; }

    public DateTime SignedIn { get; set; }

    public DateTime? LastAccessed { get; set; }

    public string? ExtraProperties { get; set; }
}
