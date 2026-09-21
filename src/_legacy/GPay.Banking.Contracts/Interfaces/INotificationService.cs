using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Dtos.Notifications;

namespace GPay.Banking.Contracts.Interfaces;

/// <summary>
/// Notifications capability implemented by each bank adapter.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Processes a notification subscribe, unsubscribe, or query request.
    /// </summary>
    Task<ApiResult<NotificationResponse>> ProcessAsync(
        NotificationRequest request,
        string correlationId,
        CancellationToken cancellationToken = default);
}
