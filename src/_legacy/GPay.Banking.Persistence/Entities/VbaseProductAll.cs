using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VbaseProductAll
{
    public Guid BaseProductId { get; set; }

    public bool? BaseProductActive { get; set; }

    public string? BaseProductCode { get; set; }

    public string? BaseProductDescription { get; set; }

    public Guid? BaseProductGridId { get; set; }

    public DateTime EffectiveDate { get; set; }

    public decimal? BaseProductPrice { get; set; }

    public Guid BaseProductPriceId { get; set; }

    public decimal? Differential { get; set; }

    public string GridZoneDescription { get; set; } = null!;

    public string GridDescription { get; set; } = null!;

    public Guid MagisterialDistrictGridZoneId { get; set; }

    public string MagisterialDistrictName { get; set; } = null!;
}
