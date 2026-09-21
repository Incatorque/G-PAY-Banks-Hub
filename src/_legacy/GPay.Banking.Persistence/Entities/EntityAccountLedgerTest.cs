using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityAccountLedgerTest
{
    public long LedgerId { get; set; }

    public Guid FkEntityId { get; set; }

    public Guid FkAccountId { get; set; }

    public decimal Amount { get; set; }

    public string SourceType { get; set; } = null!;

    public Guid? SourceId { get; set; }

    public DateTime CreatedOn { get; set; }
}
