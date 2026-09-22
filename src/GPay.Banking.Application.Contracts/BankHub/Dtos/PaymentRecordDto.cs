namespace GPay.Banking.BankHub;

/// <summary>
/// Persisted payment record snapshot (no live bank call).
/// </summary>
public class PaymentRecordDto
{
    /// <summary>Payment record primary key.</summary>
    public Guid Id { get; set; }

    /// <summary>Storage bank code (e.g. Absa).</summary>
    public string BankCode { get; set; } = string.Empty;

    /// <summary>
    /// Last known status: <c>Submitted</c>, <c>Completed</c>, <c>Failed</c>, <c>Error</c>, etc.
    /// Updated by initiate, get-status, and inbound callbacks.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>Client payment reference.</summary>
    public string? Reference { get; set; }

    /// <summary>Payment amount.</summary>
    public decimal Amount { get; set; }

    /// <summary>ISO 4217 currency.</summary>
    public string Currency { get; set; } = "ZAR";

    /// <summary>Bank / scheme transaction reference.</summary>
    public string? TransactionReference { get; set; }

    /// <summary>Bank API reference.</summary>
    public string? ApiReference { get; set; }

    /// <summary>UTC creation timestamp of the record.</summary>
    public DateTime CreationTime { get; set; }
}
