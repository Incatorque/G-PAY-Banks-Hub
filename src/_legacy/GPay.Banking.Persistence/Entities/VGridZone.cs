using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VGridZone
{
    public Guid PkGridZoneId { get; set; }

    public string GridZone { get; set; } = null!;

    public string Grid { get; set; } = null!;

    public Guid PkGridId { get; set; }

    public string Status { get; set; } = null!;
}
