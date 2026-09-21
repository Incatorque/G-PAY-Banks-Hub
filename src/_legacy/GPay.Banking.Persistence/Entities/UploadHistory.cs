using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class UploadHistory
{
    public int Id { get; set; }

    public DateTime UploadDate { get; set; }

    public Guid UserId { get; set; }

    public byte[] UploadedData { get; set; } = null!;

    public string? OriginalPath { get; set; }

    public string? FileName { get; set; }
}
