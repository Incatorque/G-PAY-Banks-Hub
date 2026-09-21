using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VRewardsUploadFile
{
    public int PkrewardsUploadFileId { get; set; }

    public string Filename { get; set; } = null!;

    public string Filepath { get; set; } = null!;

    public Guid FkentityId { get; set; }

    public Guid FkuploadedUserId { get; set; }

    public string? UploadedUser { get; set; }

    public string? UploadedEntity { get; set; }

    public DateTime UploadDate { get; set; }

    public bool? Active { get; set; }

    public byte[] UploadedData { get; set; } = null!;
}
