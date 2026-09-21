using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityAccountChargeMappingLog
{
    public Guid PkEntityAccountChargeMappingLogId { get; set; }

    public Guid FkEntityAccountChargeMappingId { get; set; }

    public Guid FkEntityAccountChargeId { get; set; }

    public DateTime LogDate { get; set; }

    public Guid FkuserId { get; set; }

    public string LogDescription { get; set; } = null!;
}
