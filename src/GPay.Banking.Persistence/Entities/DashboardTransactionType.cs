using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class DashboardTransactionType
{
    public int PkdashboardTransactionTypeId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }
}
