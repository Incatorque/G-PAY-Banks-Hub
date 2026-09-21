using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityAccountChargeReversal
{
    public Guid PkEntityAccountChargeReversalId { get; set; }

    public Guid FkEntityAccountChargeId { get; set; }

    public string? Mask { get; set; }

    public decimal? Value { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual EntityAccountCharge FkEntityAccountCharge { get; set; } = null!;
}
