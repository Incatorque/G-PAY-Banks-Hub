using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AccountChargeMapping
{
    public Guid PkAccountChargeMapping { get; set; }

    public Guid FkAccountChargeId { get; set; }

    public string? Mask { get; set; }

    public int MaskCondition { get; set; }

    public DateTime CreateDate { get; set; }

    public bool? SendNotification { get; set; }

    public string? Email { get; set; }

    public bool? ApplyCharge { get; set; }

    public int? TransactionCodeId { get; set; }

    public virtual AccountCharge FkAccountCharge { get; set; } = null!;
}
