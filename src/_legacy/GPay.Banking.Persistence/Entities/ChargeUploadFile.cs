using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ChargeUploadFile
{
    public int PkFileId { get; set; }

    public Guid FkEntityId { get; set; }

    public Guid FkUploadUserId { get; set; }

    public DateTime UploadDate { get; set; }

    public string? OriginalFileName { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public bool? Active { get; set; }

    public virtual Entity FkEntity { get; set; } = null!;

    public virtual User FkUploadUser { get; set; } = null!;
}
