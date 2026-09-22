namespace GPay.Banking.BankHub;

/// <summary>
/// Compact AVS record row for batch drill-down (not the full audit JSON).
/// </summary>
public class BankHubAvsRecordDto
{
    /// <summary>Record primary key.</summary>
    public Guid Id { get; set; }

    /// <summary>Parent batch id when this row belongs to a batch; null for standalone verifies.</summary>
    public Guid? BatchId { get; set; }

    /// <summary>Row number within the batch.</summary>
    public int? RowNumber { get; set; }

    /// <summary>Bank code used for this enquiry (e.g. Absa).</summary>
    public string BankCode { get; set; } = string.Empty;

    /// <summary>Verified account number.</summary>
    public string AccountNumber { get; set; } = string.Empty;

    /// <summary>Branch / sort code used on the request.</summary>
    public string BranchCode { get; set; } = string.Empty;

    /// <summary>Whether the enquiry was considered verified.</summary>
    public bool IsVerified { get; set; }

    /// <summary>Normalized result code (or <c>Queued</c> before processing).</summary>
    public string? ResultCode { get; set; }

    /// <summary>Human-readable result description.</summary>
    public string? ResultDescription { get; set; }

    /// <summary>Heuristic success rate for the row (0–100).</summary>
    public decimal SuccessRate { get; set; }

    /// <summary>
    /// Record status: <c>Queued</c>, <c>Verified</c>, <c>NotVerified</c>, <c>Error</c>, etc.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>Error detail when processing failed.</summary>
    public string? ErrorMessage { get; set; }

    /// <summary>Correlation id of the bank call for this row (when processed).</summary>
    public string? CorrelationId { get; set; }
}
