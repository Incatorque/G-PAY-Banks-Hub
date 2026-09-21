using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AccountChargeLog
{
    public Guid PkAccountChargeLogId { get; set; }

    public Guid FkAccountChargeId { get; set; }

    public DateTime LogDate { get; set; }

    public Guid FkuserId { get; set; }

    public string LogDescription { get; set; } = null!;

    public virtual AccountCharge FkAccountCharge { get; set; } = null!;

    public virtual User Fkuser { get; set; } = null!;
}
