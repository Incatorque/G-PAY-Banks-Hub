namespace GPay.Banking.BankHub;

/// <summary>
/// External API payload for an asynchronous mixed-bank AVS batch.
/// </summary>
/// <remarks>
/// Intended for machine clients (not the Angular ops UI). Maximum item count is
/// <c>AvsBatch:MaxItems</c> (default 20 000). Empty <see cref="Items"/> is rejected.
/// </remarks>
public class SubmitAvsBatchRequestDto
{
    /// <summary>
    /// Optional logical file / upload name stored on the batch header.
    /// When omitted, a generated name <c>api-batch-{id}.json</c> is used.
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// Optional client batch reference for reconciliation (not used as the primary key).
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Optional calling API client / tenant id stamped onto the batch and its records.
    /// </summary>
    public Guid? ApiClientId { get; set; }

    /// <summary>
    /// Ordered verification items. Each item carries its own <see cref="VerifyAccountRequestDto.Bank"/>.
    /// </summary>
    public List<SubmitAvsBatchItemDto> Items { get; set; } = new();
}
