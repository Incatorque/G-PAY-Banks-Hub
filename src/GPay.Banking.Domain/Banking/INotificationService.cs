using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;

using GPay.Banking;

namespace GPay.Banking.Domain;

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




