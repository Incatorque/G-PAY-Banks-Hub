using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Fnb3PimImportWatermark
{
    public Guid Id { get; set; }

    public string AccountId { get; set; } = null!;

    public string InvestmentAccountNumber { get; set; } = null!;

    public DateTimeOffset? LastSuccessfulImportFromUtc { get; set; }

    public DateTimeOffset? LastSuccessfulImportToUtc { get; set; }

    public DateTimeOffset? LastSuccessfulImportUtc { get; set; }

    public DateTimeOffset? LastAttemptUtc { get; set; }

    public string? LastError { get; set; }

    public DateTimeOffset CreatedUtc { get; set; }

    public DateTimeOffset UpdatedUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;
}
