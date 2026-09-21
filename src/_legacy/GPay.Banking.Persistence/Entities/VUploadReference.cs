using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VUploadReference
{
    public int Id { get; set; }

    public string Reference { get; set; } = null!;

    public string? ControllingEntity { get; set; }

    public bool? Active { get; set; }
}
