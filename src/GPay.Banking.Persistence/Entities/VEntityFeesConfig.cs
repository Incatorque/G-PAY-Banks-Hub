using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VEntityFeesConfig
{
    public Guid FkSupplierId { get; set; }

    public string Name { get; set; } = null!;

    public decimal? Value { get; set; }

    public string CommissionType { get; set; } = null!;

    public string Consumer { get; set; } = null!;
}
