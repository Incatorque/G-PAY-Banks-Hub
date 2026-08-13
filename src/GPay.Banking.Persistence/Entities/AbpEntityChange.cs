using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AbpEntityChange
{
    public Guid Id { get; set; }

    public Guid AuditLogId { get; set; }

    public Guid? TenantId { get; set; }

    public DateTime ChangeTime { get; set; }

    public byte ChangeType { get; set; }

    public Guid? EntityTenantId { get; set; }

    public string? EntityId { get; set; }

    public string EntityTypeFullName { get; set; } = null!;

    public string? ExtraProperties { get; set; }

    public virtual ICollection<AbpEntityPropertyChange> AbpEntityPropertyChanges { get; set; } = new List<AbpEntityPropertyChange>();

    public virtual AbpAuditLog AuditLog { get; set; } = null!;
}
