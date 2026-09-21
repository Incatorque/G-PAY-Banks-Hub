using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AccountChargeReferenceMask
{
    public Guid PkAccountChargeReferenceMask { get; set; }

    public Guid FkEntityAccountChargeId { get; set; }

    public string Mask { get; set; } = null!;

    public int MaskCondition { get; set; }

    public DateTime CreateDate { get; set; }

    public bool? SendNotification { get; set; }

    public string? Email { get; set; }

    public bool? ApplyCharge { get; set; }

    public virtual EntityAccountCharge FkEntityAccountCharge { get; set; } = null!;
}
