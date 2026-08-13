using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class SweepLog
{
    public long Id { get; set; }

    public DateTime DateSwept { get; set; }

    public Guid EntityBankStatementId { get; set; }

    public Guid SweepId { get; set; }

    public decimal? SweepAmount { get; set; }

    public bool? MainTransaction { get; set; }

    public virtual EntityBankStatement EntityBankStatement { get; set; } = null!;

    public virtual Sweep Sweep { get; set; } = null!;
}
