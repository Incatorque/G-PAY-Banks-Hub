using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VBankStatementGbbilist
{
    public Guid? FkAccountId { get; set; }

    public Guid? FkEntityId { get; set; }

    public decimal? Deposits { get; set; }
}
