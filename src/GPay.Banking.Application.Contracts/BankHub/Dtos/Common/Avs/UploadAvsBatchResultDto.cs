namespace GPay.Banking.BankHub;

/// <summary>
/// Immediate acknowledgement after an AVS batch is accepted and queued for processing.
/// </summary>
public class UploadAvsBatchResultDto
{
    /// <summary>
    /// Server-allocated batch id — use with get-batch / get-batch-records.
    /// </summary>
    public Guid BatchId { get; set; }

    /// <summary>Number of items accepted into the batch.</summary>
    public int TotalRows { get; set; }

    /// <summary>
    /// Number of processing segments (currently 1 for API submit; reserved for future splits).
    /// </summary>
    public int SegmentCount { get; set; }

    /// <summary>
    /// Batch lifecycle status after enqueue (typically <c>Processing</c>).
    /// </summary>
    /// <example>Processing</example>
    public string Status { get; set; } = "Processing";
}
