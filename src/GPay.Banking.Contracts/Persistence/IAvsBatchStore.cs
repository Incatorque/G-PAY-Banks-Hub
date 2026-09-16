using GPay.Banking.Contracts.Dtos.AccountVerification;
using GPay.Banking.Contracts.Entities;
using GPay.Banking.Contracts.Enums;

namespace GPay.Banking.Contracts.Persistence;

/// <summary>
/// Persistence operations for AVS batch processing.
/// </summary>
public interface IAvsBatchStore
{
    Task<AvsBatch> CreateAsync(
        AvsBatchSubmitRequest request,
        string correlationId,
        CancellationToken cancellationToken = default);

    Task<AvsBatch?> GetBatchAsync(Guid batchId, CancellationToken cancellationToken = default);

    Task<AvsBatchProgressDto?> GetProgressAsync(Guid batchId, CancellationToken cancellationToken = default);

    Task<AvsBatchItemPageDto> GetItemsAsync(
        Guid batchId,
        AvsBatchItemQuery query,
        CancellationToken cancellationToken = default);

    Task<AvsBatchItem?> GetItemAsync(Guid itemId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AvsBatchItem>> ClaimSegmentItemsAsync(
        Guid segmentId,
        CancellationToken cancellationToken = default);

    Task MarkSegmentStartedAsync(Guid segmentId, CancellationToken cancellationToken = default);

    Task MarkSegmentCompletedAsync(Guid segmentId, CancellationToken cancellationToken = default);

    Task ApplyItemResultAsync(
        Guid itemId,
        AvsBatchItemStatus status,
        string? responseJson,
        string? errorCode,
        string? errorMessage,
        string? bankReference,
        CancellationToken cancellationToken = default);

    Task TryCompleteBatchAsync(Guid batchId, CancellationToken cancellationToken = default);
}
