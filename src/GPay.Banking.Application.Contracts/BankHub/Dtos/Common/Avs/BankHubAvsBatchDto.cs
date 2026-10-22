namespace GPay.Banking.BankHub;

/// <summary>
/// AVS batch header / progress snapshot for dashboards and polling clients.
/// </summary>
public class BankHubAvsBatchDto
{
    /// <summary>Batch primary key.</summary>
    public Guid Id { get; set; }

    /// <summary>Original or generated file name.</summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>Total items in the batch.</summary>
    public int TotalRows { get; set; }

    /// <summary>Items that have finished processing (verified or failed).</summary>
    public int ProcessedCount { get; set; }

    /// <summary>Items that completed with a positive verification outcome.</summary>
    public int VerifiedCount { get; set; }

    /// <summary>Items that completed with failure / not-verified / error.</summary>
    public int FailedCount { get; set; }

    /// <summary>Items still queued or in flight.</summary>
    public int PendingCount { get; set; }

    /// <summary>Processing segment count.</summary>
    public int SegmentCount { get; set; }

    /// <summary>Percent complete: <c>100 * ProcessedCount / TotalRows</c> (100 when empty).</summary>
    public double PercentComplete { get; set; }

    /// <summary>
    /// Lifecycle status: <c>Queued</c>, <c>Processing</c>, <c>Completed</c>, <c>Failed</c>, etc.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Dominant bank code, or <c>Mixed</c> when items target more than one bank.
    /// </summary>
    public string? BankCode { get; set; }

    /// <summary>Optional API client id from the submit payload.</summary>
    public Guid? ApiClientId { get; set; }

    /// <summary>UTC creation timestamp of the batch aggregate.</summary>
    public DateTime CreationTime { get; set; }
}
