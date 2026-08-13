using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VUnallocatedFund
{
    public Guid PkEntityBankStatementId { get; set; }

    public string? Description { get; set; }

    public string ReferenceNumber { get; set; } = null!;

    public DateTime TransactionDate { get; set; }

    public decimal Amount { get; set; }

    public Guid? FkEntityId { get; set; }

    public Guid? FkAccountId { get; set; }

    public bool? Active { get; set; }
}
