using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class UploadReference
{
    public int Id { get; set; }

    public string Reference { get; set; } = null!;

    public Guid FkEntityId { get; set; }

    public bool? Active { get; set; }
}
