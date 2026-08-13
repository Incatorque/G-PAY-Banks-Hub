using GPay.Banking.Contracts.Messaging;
using GPay.Banking.Infrastructure.Correlation;
using GPay.Banking.Infrastructure.Messaging;

namespace GPay.Banking.Orchestrator.Consumers;

/// <summary>
/// Consumes bank response messages and completes pending correlations.
/// </summary>
public sealed class BankResponseConsumer : BackgroundService
{
    private readonly IMessageBus _messageBus;
    private readonly IResponseCorrelator _correlator;
    private readonly ILogger<BankResponseConsumer> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BankResponseConsumer"/> class.
    /// </summary>
    public BankResponseConsumer(
        IMessageBus messageBus,
        IResponseCorrelator correlator,
        ILogger<BankResponseConsumer> logger)
    {
        _messageBus = messageBus;
        _correlator = correlator;
        _logger = logger;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _messageBus.DeclareQueueAsync(QueueNames.OrchestratorResponses, stoppingToken);
        await _messageBus.SubscribeAsync<BankResponseMessage>(
            QueueNames.OrchestratorResponses,
            HandleAsync,
            stoppingToken);

        _logger.LogInformation("Bank response consumer started on {Queue}", QueueNames.OrchestratorResponses);

        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (OperationCanceledException)
        {
            // shutdown
        }
    }

    private Task HandleAsync(BankResponseMessage message, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Received bank response {Operation} from {Bank} CorrelationId={CorrelationId}",
            message.Operation,
            message.BankCode,
            message.CorrelationId);

        if (!_correlator.TryComplete(message.CorrelationId, message.Payload))
        {
            _logger.LogWarning(
                "No pending correlation for {CorrelationId}",
                message.CorrelationId);
        }

        return Task.CompletedTask;
    }
}
