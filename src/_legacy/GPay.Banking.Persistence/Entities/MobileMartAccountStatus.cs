using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class MobileMartAccountStatus
{
    public Guid PkMobileMartAccountStatusId { get; set; }

    public string Reason { get; set; } = null!;

    public int? Amount { get; set; }

    public int? TaxAmount { get; set; }

    public int? Outstanding { get; set; }

    public string Tariff { get; set; } = null!;

    public Guid FkMobileMartResponseId { get; set; }

    public virtual MobileMartResponse FkMobileMartResponse { get; set; } = null!;
}
