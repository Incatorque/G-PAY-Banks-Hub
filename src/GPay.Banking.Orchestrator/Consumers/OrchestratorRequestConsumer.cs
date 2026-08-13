using System.Text.Json;
using GPay.Banking.Contracts.Dtos.AccountVerification;
using GPay.Banking.Contracts.Dtos.Balances;
using GPay.Banking.Contracts.Dtos.InstantPayment;
using GPay.Banking.Contracts.Dtos.Notifications;
using GPay.Banking.Contracts.Dtos.Statements;
using GPay.Banking.Contracts.Enums;
using GPay.Banking.Contracts.Messaging;
using GPay.Banking.Infrastructure.Messaging;
using GPay.Banking.Orchestrator.Services;

namespace GPay.Banking.Orchestrator.Consumers;

/// <summary>
/// Consumes GPay-originated RabbitMQ requests using the same routing path as HTTP.
/// </summary>
public sealed class OrchestratorRequestConsumer : BackgroundService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly IMessageBus _messageBus;
    private readonly IBankRoutingService _routingService;
    private readonly ILogger<OrchestratorRequestConsumer> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrchestratorRequestConsumer"/> class.
    /// </summary>
    public OrchestratorRequestConsumer(
        IMessageBus messageBus,
        IBankRoutingService routingService,
        ILogger<OrchestratorRequestConsumer> logger)
    {
        _messageBus = messageBus;
        _routingService = routingService;
        _logger = logger;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _messageBus.DeclareQueueAsync(QueueNames.OrchestratorRequests, stoppingToken);
        await _messageBus.SubscribeAsync<BankRequestMessage>(
            QueueNames.OrchestratorRequests,
            HandleAsync,
            stoppingToken);

        _logger.LogInformation("Orchestrator request consumer started on {Queue}", QueueNames.OrchestratorRequests);

        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (OperationCanceledException)
        {
            // shutdown
        }
    }

    private async Task HandleAsync(BankRequestMessage message, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "MQ ingress {Operation} for {Bank} CorrelationId={CorrelationId}",
            message.Operation,
            message.BankCode,
            message.CorrelationId);

        switch (message.Operation)
        {
            case BankOperation.AccountVerification:
                await RouteAsync<AccountVerificationRequest, AccountVerificationResponse>(message, cancellationToken);
                break;
            case BankOperation.InstantPayment:
                await RouteAsync<InstantPaymentRequest, InstantPaymentResponse>(message, cancellationToken);
                break;
            case BankOperation.Balance:
                await RouteAsync<BalanceRequest, BalanceResponse>(message, cancellationToken);
                break;
            case BankOperation.Statement:
                await RouteAsync<StatementRequest, StatementResponse>(message, cancellationToken);
                break;
            case BankOperation.Notification:
                await RouteAsync<NotificationRequest, NotificationResponse>(message, cancellationToken);
                break;
            default:
                _logger.LogWarning("Unsupported operation {Operation}", message.Operation);
                break;
        }
    }

    private async Task RouteAsync<TRequest, TResponse>(BankRequestMessage message, CancellationToken cancellationToken)
    {
        var request = JsonSerializer.Deserialize<TRequest>(message.Payload, JsonOptions)
            ?? throw new InvalidOperationException($"Invalid payload for {message.Operation}.");

        await _routingService.SendAsync<TRequest, TResponse>(
            message.BankCode,
            message.Operation,
            request,
            message.CorrelationId,
            cancellationToken);
    }
}
