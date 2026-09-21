using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class BaseProductZone
{
    public Guid PkBaseProductZoneId { get; set; }

    public Guid FkGridZoneId { get; set; }

    public decimal? Differential { get; set; }

    public Guid FkBaseProductPriceId { get; set; }

    public bool? Active { get; set; }

    public virtual BaseProductPrice FkBaseProductPrice { get; set; } = null!;

    public virtual GridZone FkGridZone { get; set; } = null!;
}
