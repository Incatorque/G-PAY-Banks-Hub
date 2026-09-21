using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityAccountChargeExlusionLog
{
    public Guid PkEntityAccountChargeExclusionLogId { get; set; }

    public Guid FkEntityAccountChargeId { get; set; }

    public DateTime LogDate { get; set; }

    public Guid FkuserId { get; set; }

    public string LogDescription { get; set; } = null!;

    public virtual EntityAccountCharge FkEntityAccountCharge { get; set; } = null!;

    public virtual User Fkuser { get; set; } = null!;
}
