using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class BaseProductPrice
{
    public Guid PkBaseProductPriceId { get; set; }

    public decimal? Price { get; set; }

    public DateTime EffectiveDate { get; set; }

    public Guid FkBaseProductId { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<BaseProductZone> BaseProductZones { get; set; } = new List<BaseProductZone>();

    public virtual BaseProduct FkBaseProduct { get; set; } = null!;
}
