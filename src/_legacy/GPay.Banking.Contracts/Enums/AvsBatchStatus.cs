namespace GPay.Banking.Contracts.Enums;

/// <summary>
/// Lifecycle status for an AVS batch or segment.
/// </summary>
public enum AvsBatchStatus
{
    Queued = 0,
    Processing = 1,
    Completed = 2,
    Failed = 3,
    Cancelled = 4
}
