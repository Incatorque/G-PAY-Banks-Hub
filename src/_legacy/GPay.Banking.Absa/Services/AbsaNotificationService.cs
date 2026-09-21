using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Dtos.Notifications;
using GPay.Banking.Contracts.Interfaces;
using GPay.Banking.Absa.Clients;

namespace GPay.Banking.Absa.Services;

/// <summary>
/// Absa notifications stub.
/// </summary>
public sealed class AbsaNotificationService : INotificationService
{
    private readonly IAbsaCapiClient _capiClient;
    private readonly ILogger<AbsaNotificationService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbsaNotificationService"/> class.
    /// </summary>
    public AbsaNotificationService(IAbsaCapiClient capiClient, ILogger<AbsaNotificationService> logger)
    {
        _capiClient = capiClient;
        _logger = logger;
    }

    /// <inheritdoc />
    public Task<ApiResult<NotificationResponse>> ProcessAsync(
        NotificationRequest request,
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Absa notification stub Action={Action} Account={Account} CorrelationId={CorrelationId} CapiConfigured={Configured}",
            request.Action,
            request.AccountNumber,
            correlationId,
            _capiClient.IsConfigured);

        var response = new NotificationResponse
        {
            NotificationId = request.NotificationId ?? $"STUB-{correlationId}",
            Status = "StubAccepted",
            Message = "Absa notification stub — CAPI call not yet implemented."
        };

        return Task.FromResult(ApiResult<NotificationResponse>.Ok(response, correlationId));
    }
}
