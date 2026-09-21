using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class MobileMartUtilityUnit
{
    public Guid PkMobileMartUtilityUnitId { get; set; }

    public int? Quantity { get; set; }

    public string Unit { get; set; } = null!;

    public bool? Free { get; set; }

    public string Tariff { get; set; } = null!;

    public Guid FkMobileMartUtilityTokenId { get; set; }

    public virtual MobileMartUtilityToken FkMobileMartUtilityToken { get; set; } = null!;
}
