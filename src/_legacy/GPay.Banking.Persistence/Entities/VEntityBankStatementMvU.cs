using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VEntityBankStatementMvU
{
    public int? Total { get; set; }

    public string Mapped { get; set; } = null!;

    public Guid? FkAccountId { get; set; }

    public Guid? FkEntityId { get; set; }
}
