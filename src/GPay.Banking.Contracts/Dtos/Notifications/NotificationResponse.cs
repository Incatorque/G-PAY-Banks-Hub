namespace GPay.Banking.Contracts.Dtos.Notifications;

/// <summary>
/// GPay bank notification response.
/// </summary>
public sealed class NotificationResponse
{
    /// <summary>
    /// Notification or subscription identifier.
    /// </summary>
    public string? NotificationId { get; init; }

    /// <summary>
    /// Status of the notification action.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// Optional message body or acknowledgement detail.
    /// </summary>
    public string? Message { get; init; }
}
