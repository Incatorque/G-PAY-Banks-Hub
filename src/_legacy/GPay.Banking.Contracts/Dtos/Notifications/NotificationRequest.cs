namespace GPay.Banking.Contracts.Dtos.Notifications;

/// <summary>
/// GPay bank notification request (subscribe, acknowledge, or query).
/// </summary>
public sealed class NotificationRequest
{
    /// <summary>
    /// Account number associated with the notification.
    /// </summary>
    public required string AccountNumber { get; init; }

    /// <summary>
    /// Notification channel (e.g. Webhook, Email, Sms).
    /// </summary>
    public string? Channel { get; init; }

    /// <summary>
    /// Optional bank notification identifier when acknowledging.
    /// </summary>
    public string? NotificationId { get; init; }

    /// <summary>
    /// Action to perform (Subscribe, Unsubscribe, Query).
    /// </summary>
    public string Action { get; init; } = "Query";
}
