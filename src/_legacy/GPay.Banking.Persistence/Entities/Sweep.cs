using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Sweep
{
    public Guid PkSweepId { get; set; }

    public decimal? PaymentValue { get; set; }

    public string PaymentType { get; set; } = null!;

    public decimal? MinimumRequiredBalance { get; set; }

    public int Sequence { get; set; }

    public int FkFrequencyId { get; set; }

    public DateTime? SuccessfullProcessed { get; set; }

    public DateTime? LastProcessed { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public Guid? FkAccountId { get; set; }

    public bool? Active { get; set; }

    public string? ProcTime { get; set; }

    public bool? UseExpiryAmount { get; set; }

    public decimal? ExpiryAmount { get; set; }

    public DateTime? ExpiryAmountResetDate { get; set; }

    public decimal? TotalSweepAmount { get; set; }

    public Guid? FkToAccountId { get; set; }

    public decimal? RequiredRemainingBalance { get; set; }

    public Guid? FksupplierClientId { get; set; }

    public bool InstantPayment { get; set; }

    public string? SweepClientReference { get; set; }

    public virtual Frequency FkFrequency { get; set; } = null!;

    public virtual ICollection<SweepLog> SweepLogs { get; set; } = new List<SweepLog>();
}
