using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VUploadHistory
{
    public int Id { get; set; }

    public string? FileName { get; set; }

    public DateTime UploadDate { get; set; }

    public string? UploadBy { get; set; }

    public bool Active { get; set; }
}
