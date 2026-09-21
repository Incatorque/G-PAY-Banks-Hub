using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VTownProvince
{
    public Guid PkTownId { get; set; }

    public string Name { get; set; } = null!;

    public string ProvinceName { get; set; } = null!;

    public bool? Active { get; set; }
}
