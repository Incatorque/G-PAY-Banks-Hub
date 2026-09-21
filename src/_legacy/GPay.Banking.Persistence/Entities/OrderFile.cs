using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OrderFile
{
    public int PkOrderFileId { get; set; }

    public Guid FkUploadUserId { get; set; }

    public Guid FkOrderId { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public string? Description { get; set; }

    public DateTime UploadDate { get; set; }

    public DateTime LastUpdated { get; set; }

    public Guid FkLastUpdatedUserId { get; set; }

    public bool? Active { get; set; }

    public virtual User FkLastUpdatedUser { get; set; } = null!;

    public virtual Order FkOrder { get; set; } = null!;

    public virtual User FkUploadUser { get; set; } = null!;
}
