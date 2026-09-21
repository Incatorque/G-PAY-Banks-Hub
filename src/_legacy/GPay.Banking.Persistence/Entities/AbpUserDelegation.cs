using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AbpUserDelegation
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public Guid SourceUserId { get; set; }

    public Guid TargetUserId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
