using System.Text.Json;
using GPay.Banking.Contracts.Configuration;
using GPay.Banking.Contracts.Dtos.AccountVerification;
using GPay.Banking.Contracts.Enums;
using GPay.Banking.Contracts.Interfaces;
using GPay.Banking.Contracts.Messaging;
using GPay.Banking.Contracts.Persistence;
using GPay.Banking.Infrastructure.Messaging;
using Microsoft.Extensions.Options;

namespace GPay.Banking.Absa.Consumers;

/// <summary>
/// Consumes AVS batch segment messages and streams verifications with controlled parallelism.
/// </summary>
public sealed class AvsBatchSegmentConsumer : BackgroundService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMessageBus _messageBus;
    private readonly AvsBatchOptions _options;
    private readonly ILogger<AvsBatchSegmentConsumer> _logger;

    public AvsBatchSegmentConsumer(
        IServiceScopeFactory scopeFactory,
        IMessageBus messageBus,
        IOptions<AvsBatchOptions> options,
        ILogger<AvsBatchSegmentConsumer> logger)
    {
        _scopeFactory = scopeFactory;
        _messageBus = messageBus;
        _options = options.Value;
        _logger = logger;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queue = QueueNames.BankAvsBatchSegments(BankCode.Absa);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _messageBus.DeclareQueueAsync(queue, stoppingToken);
                await _messageBus.SubscribeAsync<AvsBatchSegmentMessage>(queue, HandleAsync, stoppingToken);
                _logger.LogInformation("Absa AVS batch segment consumer started on {Queue}", queue);
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
                    "Absa AVS batch segment consumer failed to start. Retrying in 5s.");
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }

    private async Task HandleAsync(ConsumedMessage<AvsBatchSegmentMessage> consumed, CancellationToken cancellationToken)
    {
        var message = consumed.Payload;
        if (message.BankCode != BankCode.Absa)
        {
            _logger.LogWarning(
                "Ignoring non-Absa segment BatchId={BatchId} SegmentId={SegmentId} Bank={Bank}",
                message.BatchId,
                message.SegmentId,
                message.BankCode);
            return;
        }

        _logger.LogInformation(
            "AVS segment start BatchId={BatchId} SegmentId={SegmentId} Index={Index}",
            message.BatchId,
            message.SegmentId,
            message.SegmentIndex);

        await using (var scope = _scopeFactory.CreateAsyncScope())
        {
            var store = scope.ServiceProvider.GetRequiredService<IAvsBatchStore>();
            await store.MarkSegmentStartedAsync(message.SegmentId, cancellationToken);
            var items = await store.ClaimSegmentItemsAsync(message.SegmentId, cancellationToken);

            var parallelism = Math.Clamp(_options.DegreeOfParallelism, 1, 32);
            using var gate = new SemaphoreSlim(parallelism, parallelism);
            var tasks = items.Select(item => ProcessItemAsync(item.Id, message.BatchId, gate, cancellationToken));
            await Task.WhenAll(tasks);

            await store.MarkSegmentCompletedAsync(message.SegmentId, cancellationToken);

            _logger.LogInformation(
                "AVS segment completed BatchId={BatchId} SegmentId={SegmentId} Items={Items}",
                message.BatchId,
                message.SegmentId,
                items.Count);
        }
    }

    private async Task ProcessItemAsync(
        Guid itemId,
        Guid batchId,
        SemaphoreSlim gate,
        CancellationToken cancellationToken)
    {
        await gate.WaitAsync(cancellationToken);
        try
        {
            await using var scope = _scopeFactory.CreateAsyncScope();
            var store = scope.ServiceProvider.GetRequiredService<IAvsBatchStore>();
            var avs = scope.ServiceProvider.GetRequiredService<IAccountVerificationService>();

            var item = await store.GetItemAsync(itemId, cancellationToken);
            if (item is null || string.IsNullOrWhiteSpace(item.RequestJson))
            {
                await store.ApplyItemResultAsync(
                    itemId,
                    AvsBatchItemStatus.Error,
                    null,
                    "GPAY_AVS_BATCH_ITEM_MISSING",
                    "Batch item request payload was not found.",
                    null,
                    cancellationToken);
                return;
            }

            AccountVerificationRequest? request;
            try
            {
                request = JsonSerializer.Deserialize<AccountVerificationRequest>(item.RequestJson, JsonOptions);
            }
            catch (JsonException ex)
            {
                await store.ApplyItemResultAsync(
                    itemId,
                    AvsBatchItemStatus.Error,
                    null,
                    "GPAY_AVS_BATCH_ITEM_INVALID",
                    ex.Message,
                    null,
                    cancellationToken);
                return;
            }

            if (request is null)
            {
                await store.ApplyItemResultAsync(
                    itemId,
                    AvsBatchItemStatus.Error,
                    null,
                    "GPAY_AVS_BATCH_ITEM_INVALID",
                    "Request JSON deserialized to null.",
                    null,
                    cancellationToken);
                return;
            }

            request = new AccountVerificationRequest
            {
                Bank = BankCode.Absa,
                AccountNumber = request.AccountNumber,
                BranchCode = request.BranchCode,
                IssuingBankCode = request.IssuingBankCode,
                IdentityNumber = request.IdentityNumber,
                IdentityType = request.IdentityType,
                AccountHolderName = request.AccountHolderName,
                Initials = request.Initials,
                LastName = request.LastName,
                AccountType = request.AccountType,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Reference = request.Reference,
                AdditionalData = request.AdditionalData
            };

            var correlationId = $"{batchId:N}:{itemId:N}";
            try
            {
                var result = await avs.VerifyAsync(request, correlationId, cancellationToken);
                var responseJson = JsonSerializer.Serialize(result.Data, JsonOptions);

                if (!result.Success || result.Data is null)
                {
                    await store.ApplyItemResultAsync(
                        itemId,
                        AvsBatchItemStatus.Error,
                        responseJson,
                        result.Error?.Code ?? "AVS_FAILED",
                        result.Error?.Message ?? "AVS failed.",
                        result.Data?.BankReference,
                        cancellationToken);
                    return;
                }

                var status = result.Data.ResultCode switch
                {
                    "VERIFIED" => AvsBatchItemStatus.Verified,
                    "PENDING" => AvsBatchItemStatus.Pending,
                    "ERROR" => AvsBatchItemStatus.Error,
                    _ => AvsBatchItemStatus.NotVerified
                };

                await store.ApplyItemResultAsync(
                    itemId,
                    status,
                    responseJson,
                    result.Data.ResultCode,
                    result.Data.ResultDescription,
                    result.Data.BankReference,
                    cancellationToken);

                _logger.LogInformation(
                    "AVS batch item done BatchId={BatchId} ItemId={ItemId} Status={Status} Result={Result}",
                    batchId,
                    itemId,
                    status,
                    result.Data.ResultCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AVS batch item failed BatchId={BatchId} ItemId={ItemId}", batchId, itemId);
                await store.ApplyItemResultAsync(
                    itemId,
                    AvsBatchItemStatus.Error,
                    null,
                    "AVS_EXCEPTION",
                    ex.Message,
                    null,
                    cancellationToken);
            }
        }
        finally
        {
            gate.Release();
        }
    }
}
