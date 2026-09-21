using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VSweepRecord
{
    public Guid PkEntityId { get; set; }

    public string Name { get; set; } = null!;

    public Guid ToAccountId { get; set; }

    public string ToAccount { get; set; } = null!;

    public string ToAccountNo { get; set; } = null!;

    public string ToBank { get; set; } = null!;

    public Guid FromAccountId { get; set; }

    public string? FromAccount { get; set; }

    public string FromAccountNo { get; set; } = null!;

    public string FromBank { get; set; } = null!;

    public DateTime? LastProcessed { get; set; }

    public DateTime? SuccessfullProcessed { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public decimal MinimumRequiredBalance { get; set; }

    public string? PaymentType { get; set; }

    public decimal? PaymentValue { get; set; }

    public string Description { get; set; } = null!;

    public int Sequence { get; set; }

    public Guid PkSupplierClientId { get; set; }

    public Guid PkSweepId { get; set; }

    public bool? UseExpiryAmount { get; set; }

    public DateTime? ExpiryAmountResetDate { get; set; }

    public decimal? ExpiryAmount { get; set; }

    public decimal? TotalSweepAmount { get; set; }

    public decimal RequiredRemainingBalance { get; set; }

    public bool InstantPayment { get; set; }
}
