using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AccountChargeReversal
{
    public Guid PkAccountChargeReversalId { get; set; }

    public Guid FkAccountChargeId { get; set; }

    public string? Mask { get; set; }

    public decimal? Value { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual AccountCharge FkAccountCharge { get; set; } = null!;
}
