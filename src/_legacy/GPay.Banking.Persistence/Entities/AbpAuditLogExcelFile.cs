using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AbpAuditLogExcelFile
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public string? FileName { get; set; }

    public DateTime CreationTime { get; set; }

    public Guid? CreatorId { get; set; }
}
