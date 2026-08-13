using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class BalanceTable
{
    public Guid AccountId { get; set; }

    public DateOnly BalanceDate { get; set; }

    public decimal OpeningBalance { get; set; }

    public decimal ClosingBalance { get; set; }

    public decimal? CumulativeDebits { get; set; }

    public decimal? CumulativeCredits { get; set; }

    public DateTime? LastUpdated { get; set; }

    public string? Source { get; set; }
}
