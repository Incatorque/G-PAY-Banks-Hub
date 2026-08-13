using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class RewardsUploadFile
{
    public int PkrewardsUploadFileId { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public Guid FkentityId { get; set; }

    public Guid FkuploadedUserId { get; set; }

    public DateTime UploadDate { get; set; }

    public byte[] UploadedData { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual Entity Fkentity { get; set; } = null!;

    public virtual User FkuploadedUser { get; set; } = null!;
}
