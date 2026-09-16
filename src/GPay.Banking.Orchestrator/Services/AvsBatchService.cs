using GPay.Banking.Contracts.Configuration;
using GPay.Banking.Contracts.Dtos.AccountVerification;
using GPay.Banking.Contracts.Enums;
using GPay.Banking.Contracts.Messaging;
using GPay.Banking.Contracts.Persistence;
using GPay.Banking.Infrastructure.Messaging;
using Microsoft.Extensions.Options;

namespace GPay.Banking.Orchestrator.Services;

/// <summary>
/// Accepts AVS batches, persists segments, and publishes segment messages to bank queues.
/// </summary>
public interface IAvsBatchService
{
    Task<AvsBatchSubmitResponse> SubmitAsync(
        AvsBatchSubmitRequest request,
        string correlationId,
        CancellationToken cancellationToken = default);

    Task<AvsBatchProgressDto?> GetProgressAsync(Guid batchId, CancellationToken cancellationToken = default);

    Task<AvsBatchItemPageDto> GetItemsAsync(
        Guid batchId,
        AvsBatchItemQuery query,
        CancellationToken cancellationToken = default);
}

/// <inheritdoc />
public sealed class AvsBatchService : IAvsBatchService
{
    private readonly IAvsBatchStore _store;
    private readonly IMessageBus _messageBus;
    private readonly AvsBatchOptions _options;
    private readonly ILogger<AvsBatchService> _logger;

    public AvsBatchService(
        IAvsBatchStore store,
        IMessageBus messageBus,
        IOptions<AvsBatchOptions> options,
        ILogger<AvsBatchService> logger)
    {
        _store = store;
        _messageBus = messageBus;
        _options = options.Value;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<AvsBatchSubmitResponse> SubmitAsync(
        AvsBatchSubmitRequest request,
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        Validate(request);

        var batch = await _store.CreateAsync(request, correlationId, cancellationToken);

        _logger.LogInformation(
            "AVS batch created BatchId={BatchId} ExternalBatchId={ExternalBatchId} Items={Items} Segments={Segments} CorrelationId={CorrelationId}",
            batch.Id,
            batch.ExternalBatchId,
            batch.TotalItems,
            batch.SegmentCount,
            correlationId);

        foreach (var segment in batch.Segments.OrderBy(s => s.SegmentIndex))
        {
            var queue = QueueNames.BankAvsBatchSegments(segment.BankCode);
            var message = new AvsBatchSegmentMessage
            {
                BatchId = batch.Id,
                SegmentId = segment.Id,
                BankCode = segment.BankCode,
                SegmentIndex = segment.SegmentIndex,
                CreatedAtUtc = DateTimeOffset.UtcNow
            };

            await _messageBus.PublishAsync(queue, message, cancellationToken: cancellationToken);

            _logger.LogInformation(
                "AVS segment queued BatchId={BatchId} SegmentId={SegmentId} Bank={Bank} Index={Index} Items={Items} Queue={Queue}",
                batch.Id,
                segment.Id,
                segment.BankCode,
                segment.SegmentIndex,
                segment.ItemCount,
                queue);
        }

        return new AvsBatchSubmitResponse
        {
            BatchId = batch.Id,
            ExternalBatchId = batch.ExternalBatchId,
            TotalItems = batch.TotalItems,
            SegmentCount = batch.SegmentCount,
            Status = AvsBatchStatus.Queued,
            CorrelationId = correlationId
        };
    }

    /// <inheritdoc />
    public Task<AvsBatchProgressDto?> GetProgressAsync(Guid batchId, CancellationToken cancellationToken = default) =>
        _store.GetProgressAsync(batchId, cancellationToken);

    /// <inheritdoc />
    public Task<AvsBatchItemPageDto> GetItemsAsync(
        Guid batchId,
        AvsBatchItemQuery query,
        CancellationToken cancellationToken = default) =>
        _store.GetItemsAsync(batchId, query, cancellationToken);

    private void Validate(AvsBatchSubmitRequest request)
    {
        var max = Math.Max(1, _options.MaxItems);
        if (request.Items is null || request.Items.Count == 0)
        {
            throw new ArgumentException("Batch must contain at least one item.", nameof(request));
        }

        if (request.Items.Count > max)
        {
            throw new ArgumentException($"Batch exceeds maximum of {max} items.", nameof(request));
        }

        for (var i = 0; i < request.Items.Count; i++)
        {
            var item = request.Items[i];
            if (item.Request is null)
            {
                throw new ArgumentException($"Item[{i}].Request is required.", nameof(request));
            }

            if (item.Request.Bank is null)
            {
                throw new ArgumentException(
                    $"Item[{i}] (row {item.RowNumber ?? i + 1}) requires Bank for mixed-bank routing.",
                    nameof(request));
            }

            if (string.IsNullOrWhiteSpace(item.Request.AccountNumber))
            {
                throw new ArgumentException($"Item[{i}] AccountNumber is required.", nameof(request));
            }

            if (string.IsNullOrWhiteSpace(item.Request.BranchCode))
            {
                throw new ArgumentException($"Item[{i}] BranchCode is required.", nameof(request));
            }
        }
    }
}
