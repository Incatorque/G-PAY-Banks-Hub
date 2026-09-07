using System.Text.Json;
using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Dtos.AccountVerification;
using GPay.Banking.Contracts.Dtos.Balances;
using GPay.Banking.Contracts.Dtos.InstantPayment;
using GPay.Banking.Contracts.Dtos.Notifications;
using GPay.Banking.Contracts.Dtos.Statements;
using GPay.Banking.Contracts.Enums;
using GPay.Banking.Contracts.Interfaces;
using GPay.Banking.Contracts.Messaging;
using GPay.Banking.Infrastructure.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace GPay.Banking.Absa.Consumers;

/// <summary>
/// Background worker that consumes Absa request queue messages, calls Absa CAPI, and replies directly to the orchestrator.
/// </summary>
public sealed class AbsaRequestConsumer : BackgroundService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMessageBus _messageBus;
    private readonly ILogger<AbsaRequestConsumer> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbsaRequestConsumer"/> class.
    /// </summary>
    public AbsaRequestConsumer(
        IServiceScopeFactory scopeFactory,
        IMessageBus messageBus,
        ILogger<AbsaRequestConsumer> logger)
    {
        _scopeFactory = scopeFactory;
        _messageBus = messageBus;
        _logger = logger;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queue = QueueNames.BankRequests(BankCode.Absa);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _messageBus.DeclareQueueAsync(queue, stoppingToken);
                await _messageBus.SubscribeAsync<BankRequestMessage>(queue, HandleAsync, stoppingToken);
                _logger.LogInformation("Absa request consumer started on {Queue}", queue);
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
                    "Absa request consumer failed to start. Retrying in 5s. Is RabbitMQ running on localhost:5672?");
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }

    private async Task HandleAsync(ConsumedMessage<BankRequestMessage> consumed, CancellationToken cancellationToken)
    {
        var message = consumed.Payload;

        _logger.LogInformation(
            "Processing Absa {Operation} CorrelationId={CorrelationId}",
            message.Operation,
            message.CorrelationId);

        if (string.IsNullOrWhiteSpace(consumed.ReplyTo))
        {
            _logger.LogError(
                "Missing ReplyTo for Absa {Operation} CorrelationId={CorrelationId}",
                message.Operation,
                message.CorrelationId);
            return;
        }

        await using var scope = _scopeFactory.CreateAsyncScope();
        var payload = message.Operation switch
        {
            BankOperation.AccountVerification => await ExecuteAsync<AccountVerificationRequest, AccountVerificationResponse>(
                scope, message, (sp, req, id, ct) => sp.GetRequiredService<IAccountVerificationService>().VerifyAsync(req, id, ct), cancellationToken),
            BankOperation.InstantPayment => await ExecuteAsync<InstantPaymentRequest, InstantPaymentResponse>(
                scope, message, (sp, req, id, ct) => sp.GetRequiredService<IInstantPaymentService>().PayAsync(req, id, ct), cancellationToken),
            BankOperation.Balance => await ExecuteAsync<BalanceRequest, BalanceResponse>(
                scope, message, (sp, req, id, ct) => sp.GetRequiredService<IBalanceService>().GetBalanceAsync(req, id, ct), cancellationToken),
            BankOperation.Statement => await ExecuteAsync<StatementRequest, StatementResponse>(
                scope, message, (sp, req, id, ct) => sp.GetRequiredService<IStatementService>().GetStatementAsync(req, id, ct), cancellationToken),
            BankOperation.Notification => await ExecuteAsync<NotificationRequest, NotificationResponse>(
                scope, message, (sp, req, id, ct) => sp.GetRequiredService<INotificationService>().ProcessAsync(req, id, ct), cancellationToken),
            _ => CreateUnsupportedPayload(message)
        };

        var response = new BankResponseMessage
        {
            CorrelationId = message.CorrelationId,
            BankCode = BankCode.Absa,
            Operation = message.Operation,
            Success = !payload.Contains("\"success\":false", StringComparison.OrdinalIgnoreCase),
            Payload = payload
        };

        await _messageBus.PublishAsync(
            consumed.ReplyTo,
            response,
            new MessagePublishOptions { CorrelationId = message.CorrelationId },
            cancellationToken);
    }

    private static async Task<string> ExecuteAsync<TRequest, TResponse>(
        IServiceScope scope,
        BankRequestMessage message,
        Func<IServiceProvider, TRequest, string, CancellationToken, Task<ApiResult<TResponse>>> invoke,
        CancellationToken cancellationToken)
    {
        var request = JsonSerializer.Deserialize<TRequest>(message.Payload, JsonOptions)
            ?? throw new InvalidOperationException($"Invalid {typeof(TRequest).Name} payload.");

        var result = await invoke(scope.ServiceProvider, request, message.CorrelationId, cancellationToken);
        return JsonSerializer.Serialize(result, JsonOptions);
    }

    private static string CreateUnsupportedPayload(BankRequestMessage message)
    {
        var result = ApiResult<object>.Fail(
            new ApiError
            {
                Code = "ABSA_UNSUPPORTED",
                Message = $"Unsupported operation {message.Operation}."
            },
            message.CorrelationId);

        return JsonSerializer.Serialize(result, JsonOptions);
    }
}
