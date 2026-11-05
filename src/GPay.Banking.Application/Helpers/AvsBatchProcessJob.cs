using System.Text.Json;
using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace GPay.Banking.Helpers;

public class AvsBatchProcessJob(
    IRepository<BankHubAvsBatch, Guid> batchRepository,
    IRepository<BankHubAvsRecord, Guid> recordRepository,
    IBankCapabilityResolver resolver,
    IOptions<AvsBatchOptions> options,
    IUnitOfWorkManager uowManager)
    : AsyncBackgroundJob<AvsBatchProcessJobArgs>, ITransientDependency
{
    private readonly AvsBatchOptions _options = options.Value;

    public override async Task ExecuteAsync(AvsBatchProcessJobArgs args)
    {
        using var uow = uowManager.Begin(requiresNew: true, isTransactional: true);
        var batch = await batchRepository.FindAsync(args.BatchId);
        if (batch is null)
        {
            Logger.LogWarning("AVS batch {BatchId} not found", args.BatchId);
            return;
        }

        batch.Status = AvsStatuses.Processing;
        await batchRepository.UpdateAsync(batch, autoSave: true);

        var records = (await recordRepository.GetListAsync(x =>
                x.BatchId == args.BatchId && (x.Status == AvsStatuses.Queued || x.Status == AvsStatuses.Pending)))
            .OrderBy(x => x.RowNumber)
            .ToList();
        var parallelism = Math.Max(1, _options.DegreeOfParallelism);

        await Parallel.ForEachAsync(
            records,
            new ParallelOptions { MaxDegreeOfParallelism = parallelism },
            async (record, _) => await ProcessRecordAsync(record.Id));
        await RefreshCountsAsync(args.BatchId);
        await uow.CompleteAsync();
    }

    private async Task ProcessRecordAsync(Guid recordId)
    {
        using var uow = uowManager.Begin(requiresNew: true, isTransactional: true);
        var record = await recordRepository.GetAsync(recordId);
        record.Status = AvsStatuses.Processing;
        await recordRepository.UpdateAsync(record, autoSave: true);

        var correlationId = record.Id.ToString("N");

        try
        {
            var request = BuildRequest(record);
            var bank = request.Bank ?? BankCode.Absa;
            var service = resolver.GetAccountVerification(bank);
            var result = await service.VerifyAsync(request, correlationId);
            Apply(record, result, correlationId);
        }
        catch (Exception ex)
        {
            record.Status = AvsStatuses.Error;
            record.ErrorMessage = ex.Message;
            record.CorrelationId = correlationId;

            Logger.LogError(ex, "Batch AVS failed for record {RecordId}", recordId);
        }

        await recordRepository.UpdateAsync(record, autoSave: true);
        await uow.CompleteAsync();
    }

    private static AccountVerificationRequest BuildRequest(BankHubAvsRecord record)
    {
        VerifyAccountRequestDto? dto = null;
        if (!string.IsNullOrWhiteSpace(record.RequestBodyJson))
        {
            try
            {
                dto = JsonSerializer.Deserialize<VerifyAccountRequestDto>(record.RequestBodyJson);
            }
            catch (JsonException)
            {
                // Fall back to the persisted columns when the request snapshot is malformed.
            }
        }

        return AccountVerificationRequestMapper.From(dto, BankCodeParser.Parse(record.BankCode), record);
    }

    private async Task RefreshCountsAsync(Guid batchId)
    {
        var batch = await batchRepository.GetAsync(batchId);
        var records = await recordRepository.GetListAsync(x => x.BatchId == batchId);

        batch.ProcessedCount = records.Count(x => x.Status is AvsStatuses.Verified or AvsStatuses.NotVerified or AvsStatuses.Error);
        batch.VerifiedCount = records.Count(x => x.IsVerified || x.Status == AvsStatuses.Verified);
        batch.FailedCount = records.Count(x => x.Status == AvsStatuses.Error || (x.Status == AvsStatuses.NotVerified && !x.IsVerified));
        batch.PendingCount = records.Count(x => x.Status is AvsStatuses.Queued or AvsStatuses.Processing or AvsStatuses.Pending);
        batch.Status = batch.PendingCount == 0 ? AvsStatuses.Completed : AvsStatuses.Processing;

        await batchRepository.UpdateAsync(batch, autoSave: true);
    }

    private static void Apply(BankHubAvsRecord record, ApiResult<AccountVerificationResponse> result, string correlationId)
    {
        record.CorrelationId = correlationId;
        record.ResponseBodyJson = JsonSerializer.Serialize(result);
        if (!result.Success || result.Data is null)
        {
            record.Status = AvsStatuses.Error;
            record.ResultCode = result.Error?.Code;
            record.ResultDescription = result.Error?.Message;
            record.ErrorMessage = result.Error?.Message;
            return;
        }

        BankHubAvsRecordMapper.Apply(record, result.Data);
    }
}
