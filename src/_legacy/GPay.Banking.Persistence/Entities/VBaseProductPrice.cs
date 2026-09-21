using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VBaseProductPrice
{
    public Guid PkBaseProductPriceId { get; set; }

    public string? Description { get; set; }

    public DateTime EffectiveDate { get; set; }

    public decimal? Price { get; set; }

    public bool? Active { get; set; }
}
