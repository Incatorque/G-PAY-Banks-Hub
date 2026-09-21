using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityAccountChargeLog
{
    public Guid PkEntityAccountChargeLogId { get; set; }

    public Guid FkEntityAccountChargeId { get; set; }

    public DateTime LogDate { get; set; }

    public Guid FkuserId { get; set; }

    public string LogDescription { get; set; } = null!;

    public virtual EntityAccountCharge FkEntityAccountCharge { get; set; } = null!;

    public virtual User Fkuser { get; set; } = null!;
}
