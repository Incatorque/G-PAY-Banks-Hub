using GPay.Banking.Contracts.Messaging;
using GPay.Banking.Infrastructure.Correlation;
using GPay.Banking.Infrastructure.Messaging;

namespace GPay.Banking.Orchestrator.Consumers;

/// <summary>
/// Consumes direct RabbitMQ RPC replies from bank workers and completes pending correlations.
/// </summary>
public sealed class DirectReplyConsumer : BackgroundService
{
    private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(5);

    private readonly IMessageBus _messageBus;
    private readonly IResponseCorrelator _correlator;
    private readonly ILogger<DirectReplyConsumer> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DirectReplyConsumer"/> class.
    /// </summary>
    public DirectReplyConsumer(
        IMessageBus messageBus,
        IResponseCorrelator correlator,
        ILogger<DirectReplyConsumer> logger)
    {
        _messageBus = messageBus;
        _correlator = correlator;
        _logger = logger;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _messageBus.SubscribeToDirectRepliesAsync(HandleAsync, stoppingToken);
                _logger.LogInformation("Direct reply consumer ready on {Queue}", QueueNames.DirectReplyTo);
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Direct reply consumer failed to start. Retrying in {DelaySeconds}s. Is RabbitMQ running on localhost:5672?",
                    RetryDelay.TotalSeconds);
                await Task.Delay(RetryDelay, stoppingToken);
            }
        }
    }

    private Task HandleAsync(ConsumedMessage<BankResponseMessage> message, CancellationToken cancellationToken)
    {
        var response = message.Payload;
        var correlationId = message.CorrelationId ?? response.CorrelationId;

        _logger.LogInformation(
            "Received direct reply {Operation} from {Bank} CorrelationId={CorrelationId}",
            response.Operation,
            response.BankCode,
            correlationId);

        if (!_correlator.TryComplete(correlationId, response.Payload))
        {
            _logger.LogWarning(
                "No pending correlation for {CorrelationId}",
                correlationId);
        }

        return Task.CompletedTask;
    }
}
