using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VGoodsFeesPerEntity
{
    public string? ShortName { get; set; }

    public decimal? Value { get; set; }

    public Guid FkSupplierId { get; set; }

    public string Agent { get; set; } = null!;

    public string CommissionType { get; set; } = null!;
}
