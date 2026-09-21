namespace GPay.Banking.Contracts.Enums;

/// <summary>
/// Lifecycle status for a single AVS batch item.
/// </summary>
public enum AvsBatchItemStatus
{
    Queued = 0,
    Processing = 1,
    Verified = 2,
    NotVerified = 3,
    Pending = 4,
    Error = 5
}
