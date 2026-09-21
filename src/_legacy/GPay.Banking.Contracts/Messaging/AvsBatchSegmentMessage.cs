using GPay.Banking.Contracts.Enums;

namespace GPay.Banking.Contracts.Messaging;

/// <summary>
/// Queue message instructing a bank worker to process one AVS batch segment (≤ 5 000 items).
/// Payload lives in the database; this message carries identifiers only.
/// </summary>
public sealed class AvsBatchSegmentMessage
{
    public Guid BatchId { get; init; }

    public Guid SegmentId { get; init; }

    public BankCode BankCode { get; init; }

    public int SegmentIndex { get; init; }

    public DateTimeOffset CreatedAtUtc { get; init; } = DateTimeOffset.UtcNow;
}
