using System.Text.Json;
using GPay.Banking.Contracts.Dtos.AccountVerification;
using GPay.Banking.Contracts.Entities;
using GPay.Banking.Contracts.Enums;
using GPay.Banking.Contracts.Persistence;
using GPay.Banking.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.Persistence.Avs;

/// <summary>
/// EF Core store for orchestrator AVS batch tables.
/// </summary>
public sealed class AvsBatchStore : IAvsBatchStore
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly BankingDbContext _db;

    public AvsBatchStore(BankingDbContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task<AvsBatch> CreateAsync(
        AvsBatchSubmitRequest request,
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        var batch = new AvsBatch
        {
            Id = Guid.NewGuid(),
            ExternalBatchId = request.ExternalBatchId,
            Reference = request.Reference,
            SourceFileName = request.SourceFileName,
            CorrelationId = correlationId,
            Status = AvsBatchStatus.Queued,
            TotalItems = request.Items.Count,
            PendingCount = request.Items.Count,
            CreatedAtUtc = now
        };

        var byBank = request.Items
            .Select((item, index) => new { Item = item, Index = index })
            .GroupBy(x =>
            {
                var bank = x.Item.Request.Bank
                    ?? throw new InvalidOperationException($"Item at row {x.Item.RowNumber ?? x.Index + 1} is missing Bank.");
                return bank;
            })
            .OrderBy(g => g.Key);

        var segments = new List<AvsBatchSegment>();
        var entities = new List<AvsBatchItem>();
        var segmentIndex = 0;

        foreach (var bankGroup in byBank)
        {
            var bankItems = bankGroup.ToList();
            for (var offset = 0; offset < bankItems.Count; offset += AvsBatchLimits.SegmentSize)
            {
                var chunk = bankItems.Skip(offset).Take(AvsBatchLimits.SegmentSize).ToList();
                var segment = new AvsBatchSegment
                {
                    Id = Guid.NewGuid(),
                    BatchId = batch.Id,
                    BankCode = bankGroup.Key,
                    SegmentIndex = segmentIndex++,
                    ItemCount = chunk.Count,
                    Status = AvsBatchStatus.Queued,
                    QueuedAtUtc = now,
                    CreatedAtUtc = now
                };
                segments.Add(segment);

                foreach (var entry in chunk)
                {
                    entities.Add(new AvsBatchItem
                    {
                        Id = Guid.NewGuid(),
                        BatchId = batch.Id,
                        SegmentId = segment.Id,
                        ExternalRecordId = entry.Item.ExternalRecordId,
                        RowNumber = entry.Item.RowNumber ?? entry.Index + 1,
                        BankCode = bankGroup.Key,
                        Status = AvsBatchItemStatus.Queued,
                        RequestJson = JsonSerializer.Serialize(entry.Item.Request, JsonOptions),
                        CreatedAtUtc = now
                    });
                }
            }
        }

        batch.SegmentCount = segments.Count;
        batch.Segments = segments;
        batch.Items = entities;

        await _db.AvsBatches.AddAsync(batch, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return batch;
    }

    /// <inheritdoc />
    public Task<AvsBatch?> GetBatchAsync(Guid batchId, CancellationToken cancellationToken = default) =>
        _db.AvsBatches.AsNoTracking().FirstOrDefaultAsync(x => x.Id == batchId, cancellationToken);

    /// <inheritdoc />
    public async Task<AvsBatchProgressDto?> GetProgressAsync(Guid batchId, CancellationToken cancellationToken = default)
    {
        var batch = await _db.AvsBatches.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == batchId, cancellationToken);
        if (batch is null)
        {
            return null;
        }

        var segments = await _db.AvsBatchSegments.AsNoTracking()
            .Where(x => x.BatchId == batchId)
            .OrderBy(x => x.SegmentIndex)
            .ToListAsync(cancellationToken);

        var bankRows = await _db.AvsBatchItems.AsNoTracking()
            .Where(x => x.BatchId == batchId)
            .GroupBy(x => x.BankCode)
            .Select(g => new
            {
                BankCode = g.Key,
                Total = g.Count(),
                Processed = g.Count(i => i.Status != AvsBatchItemStatus.Queued && i.Status != AvsBatchItemStatus.Processing),
                Succeeded = g.Count(i => i.Status == AvsBatchItemStatus.Verified),
                Failed = g.Count(i => i.Status == AvsBatchItemStatus.Error || i.Status == AvsBatchItemStatus.NotVerified),
                Pending = g.Count(i =>
                    i.Status == AvsBatchItemStatus.Queued ||
                    i.Status == AvsBatchItemStatus.Processing ||
                    i.Status == AvsBatchItemStatus.Pending)
            })
            .ToListAsync(cancellationToken);

        var percent = batch.TotalItems == 0
            ? 100d
            : Math.Round(100d * batch.ProcessedCount / batch.TotalItems, 2);

        return new AvsBatchProgressDto
        {
            BatchId = batch.Id,
            ExternalBatchId = batch.ExternalBatchId,
            Reference = batch.Reference,
            SourceFileName = batch.SourceFileName,
            Status = batch.Status,
            TotalItems = batch.TotalItems,
            ProcessedCount = batch.ProcessedCount,
            SucceededCount = batch.SucceededCount,
            FailedCount = batch.FailedCount,
            PendingCount = batch.PendingCount,
            SegmentCount = batch.SegmentCount,
            PercentComplete = percent,
            CreatedAtUtc = batch.CreatedAtUtc,
            StartedAtUtc = batch.StartedAtUtc,
            CompletedAtUtc = batch.CompletedAtUtc,
            Segments = segments.Select(s => new AvsBatchSegmentProgressDto
            {
                SegmentId = s.Id,
                BankCode = s.BankCode,
                SegmentIndex = s.SegmentIndex,
                ItemCount = s.ItemCount,
                Status = s.Status,
                ProcessedCount = s.ProcessedCount,
                StartedAtUtc = s.StartedAtUtc,
                CompletedAtUtc = s.CompletedAtUtc
            }).ToList(),
            BankBreakdown = bankRows.Select(b => new AvsBatchBankBreakdownDto
            {
                BankCode = b.BankCode,
                TotalItems = b.Total,
                ProcessedCount = b.Processed,
                SucceededCount = b.Succeeded,
                FailedCount = b.Failed,
                PendingCount = b.Pending
            }).ToList()
        };
    }

    /// <inheritdoc />
    public async Task<AvsBatchItemPageDto> GetItemsAsync(
        Guid batchId,
        AvsBatchItemQuery query,
        CancellationToken cancellationToken = default)
    {
        var take = Math.Clamp(query.Take <= 0 ? 100 : query.Take, 1, 1000);
        var skip = Math.Max(0, query.Skip);

        IQueryable<AvsBatchItem> q = _db.AvsBatchItems.AsNoTracking().Where(x => x.BatchId == batchId);
        if (query.Status is not null)
        {
            q = q.Where(x => x.Status == query.Status);
        }

        if (query.BankCode is not null)
        {
            q = q.Where(x => x.BankCode == query.BankCode);
        }

        if (query.ProcessedOnly == true)
        {
            q = q.Where(x => x.Status != AvsBatchItemStatus.Queued && x.Status != AvsBatchItemStatus.Processing);
        }

        var total = await q.CountAsync(cancellationToken);
        var rows = await q.OrderBy(x => x.RowNumber).Skip(skip).Take(take).ToListAsync(cancellationToken);

        return new AvsBatchItemPageDto
        {
            BatchId = batchId,
            TotalCount = total,
            Items = rows.Select(MapItem).ToList()
        };
    }

    /// <inheritdoc />
    public Task<AvsBatchItem?> GetItemAsync(Guid itemId, CancellationToken cancellationToken = default) =>
        _db.AvsBatchItems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == itemId, cancellationToken);

    /// <inheritdoc />
    public async Task<IReadOnlyList<AvsBatchItem>> ClaimSegmentItemsAsync(
        Guid segmentId,
        CancellationToken cancellationToken = default)
    {
        var items = await _db.AvsBatchItems
            .Where(x => x.SegmentId == segmentId && x.Status == AvsBatchItemStatus.Queued)
            .OrderBy(x => x.RowNumber)
            .ToListAsync(cancellationToken);

        foreach (var item in items)
        {
            item.Status = AvsBatchItemStatus.Processing;
            item.UpdatedAtUtc = DateTimeOffset.UtcNow;
        }

        if (items.Count > 0)
        {
            await _db.SaveChangesAsync(cancellationToken);
        }

        return items;
    }

    /// <inheritdoc />
    public async Task MarkSegmentStartedAsync(Guid segmentId, CancellationToken cancellationToken = default)
    {
        var segment = await _db.AvsBatchSegments.FirstOrDefaultAsync(x => x.Id == segmentId, cancellationToken);
        if (segment is null)
        {
            return;
        }

        var now = DateTimeOffset.UtcNow;
        segment.Status = AvsBatchStatus.Processing;
        segment.StartedAtUtc ??= now;
        segment.UpdatedAtUtc = now;

        var batch = await _db.AvsBatches.FirstOrDefaultAsync(x => x.Id == segment.BatchId, cancellationToken);
        if (batch is not null && batch.Status == AvsBatchStatus.Queued)
        {
            batch.Status = AvsBatchStatus.Processing;
            batch.StartedAtUtc ??= now;
            batch.UpdatedAtUtc = now;
        }

        await _db.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task MarkSegmentCompletedAsync(Guid segmentId, CancellationToken cancellationToken = default)
    {
        var segment = await _db.AvsBatchSegments.FirstOrDefaultAsync(x => x.Id == segmentId, cancellationToken);
        if (segment is null)
        {
            return;
        }

        var now = DateTimeOffset.UtcNow;
        segment.Status = AvsBatchStatus.Completed;
        segment.CompletedAtUtc = now;
        segment.UpdatedAtUtc = now;
        await _db.SaveChangesAsync(cancellationToken);
        await TryCompleteBatchAsync(segment.BatchId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task ApplyItemResultAsync(
        Guid itemId,
        AvsBatchItemStatus status,
        string? responseJson,
        string? errorCode,
        string? errorMessage,
        string? bankReference,
        CancellationToken cancellationToken = default)
    {
        var item = await _db.AvsBatchItems.FirstOrDefaultAsync(x => x.Id == itemId, cancellationToken);
        if (item is null)
        {
            return;
        }

        var wasTerminal = IsTerminal(item.Status);
        var now = DateTimeOffset.UtcNow;
        item.Status = status;
        item.ResponseJson = responseJson;
        item.ErrorCode = errorCode;
        item.ErrorMessage = errorMessage;
        item.BankReference = bankReference;
        item.ProcessedAtUtc = now;
        item.UpdatedAtUtc = now;

        var segment = await _db.AvsBatchSegments.FirstOrDefaultAsync(x => x.Id == item.SegmentId, cancellationToken);
        var batch = await _db.AvsBatches.FirstOrDefaultAsync(x => x.Id == item.BatchId, cancellationToken);

        if (!wasTerminal && IsTerminal(status))
        {
            if (segment is not null)
            {
                segment.ProcessedCount++;
                segment.UpdatedAtUtc = now;
            }

            if (batch is not null)
            {
                batch.ProcessedCount++;
                batch.PendingCount = Math.Max(0, batch.TotalItems - batch.ProcessedCount);
                if (status == AvsBatchItemStatus.Verified)
                {
                    batch.SucceededCount++;
                }
                else if (status is AvsBatchItemStatus.Error or AvsBatchItemStatus.NotVerified)
                {
                    batch.FailedCount++;
                }

                batch.UpdatedAtUtc = now;
            }
        }

        await _db.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task TryCompleteBatchAsync(Guid batchId, CancellationToken cancellationToken = default)
    {
        var batch = await _db.AvsBatches.FirstOrDefaultAsync(x => x.Id == batchId, cancellationToken);
        if (batch is null || batch.Status == AvsBatchStatus.Completed)
        {
            return;
        }

        var incomplete = await _db.AvsBatchSegments.AnyAsync(
            x => x.BatchId == batchId && x.Status != AvsBatchStatus.Completed && x.Status != AvsBatchStatus.Failed,
            cancellationToken);
        if (incomplete)
        {
            return;
        }

        var now = DateTimeOffset.UtcNow;
        batch.Status = AvsBatchStatus.Completed;
        batch.CompletedAtUtc = now;
        batch.PendingCount = Math.Max(0, batch.TotalItems - batch.ProcessedCount);
        batch.UpdatedAtUtc = now;
        await _db.SaveChangesAsync(cancellationToken);
    }

    private static bool IsTerminal(AvsBatchItemStatus status) =>
        status is AvsBatchItemStatus.Verified
            or AvsBatchItemStatus.NotVerified
            or AvsBatchItemStatus.Error
            or AvsBatchItemStatus.Pending;

    private static AvsBatchItemDto MapItem(AvsBatchItem item)
    {
        AccountVerificationResponse? result = null;
        if (!string.IsNullOrWhiteSpace(item.ResponseJson))
        {
            try
            {
                result = JsonSerializer.Deserialize<AccountVerificationResponse>(item.ResponseJson, JsonOptions);
            }
            catch (JsonException)
            {
                // leave null
            }
        }

        return new AvsBatchItemDto
        {
            ItemId = item.Id,
            BatchId = item.BatchId,
            SegmentId = item.SegmentId,
            ExternalRecordId = item.ExternalRecordId,
            RowNumber = item.RowNumber,
            BankCode = item.BankCode,
            Status = item.Status,
            Result = result,
            ErrorCode = item.ErrorCode,
            ErrorMessage = item.ErrorMessage,
            BankReference = item.BankReference,
            ProcessedAtUtc = item.ProcessedAtUtc,
            RequestJson = item.RequestJson,
            ResponseJson = item.ResponseJson
        };
    }
}
