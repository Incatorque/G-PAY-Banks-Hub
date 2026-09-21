using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AccountChargeMappingLog
{
    public Guid PkAccountChargeMappingLogId { get; set; }

    public Guid FkAccountChargeMappingId { get; set; }

    public Guid FkAccountChargeId { get; set; }

    public DateTime LogDate { get; set; }

    public Guid FkuserId { get; set; }

    public string LogDescription { get; set; } = null!;
}
