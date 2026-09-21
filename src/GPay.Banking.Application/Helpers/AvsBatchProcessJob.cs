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

public class AvsBatchProcessJob : AsyncBackgroundJob<AvsBatchProcessJobArgs>, ITransientDependency
{
    private readonly IRepository<BankHubAvsBatch, Guid> _batchRepository;
    private readonly IRepository<BankHubAvsRecord, Guid> _recordRepository;
    private readonly IBankCapabilityResolver _resolver;
    private readonly AvsBatchOptions _options;
    private readonly IUnitOfWorkManager _uowManager;

    public AvsBatchProcessJob(
        IRepository<BankHubAvsBatch, Guid> batchRepository,
        IRepository<BankHubAvsRecord, Guid> recordRepository,
        IBankCapabilityResolver resolver,
        IOptions<AvsBatchOptions> options,
        IUnitOfWorkManager uowManager)
    {
        _batchRepository = batchRepository;
        _recordRepository = recordRepository;
        _resolver = resolver;
        _options = options.Value;
        _uowManager = uowManager;
    }

    public override async Task ExecuteAsync(AvsBatchProcessJobArgs args)
    {
        using var uow = _uowManager.Begin(requiresNew: true, isTransactional: true);
        var batch = await _batchRepository.FindAsync(args.BatchId);
        if (batch is null)
        {
            Logger.LogWarning("AVS batch {BatchId} not found", args.BatchId);
            return;
        }

        batch.Status = "Processing";
        await _batchRepository.UpdateAsync(batch, autoSave: true);

        var records = (await _recordRepository.GetListAsync(x =>
                x.BatchId == args.BatchId && (x.Status == "Queued" || x.Status == "Pending")))
            .OrderBy(x => x.RowNumber)
            .ToList();

        var parallelism = Math.Max(1, _options.DegreeOfParallelism);
        using var gate = new SemaphoreSlim(parallelism);
        var tasks = records.Select(async record =>
        {
            await gate.WaitAsync();
            try { await ProcessRecordAsync(record.Id); }
            finally { gate.Release(); }
        });
        await Task.WhenAll(tasks);

        await RefreshCountsAsync(args.BatchId);
        await uow.CompleteAsync();
    }

    private async Task ProcessRecordAsync(Guid recordId)
    {
        using var uow = _uowManager.Begin(requiresNew: true, isTransactional: true);
        var record = await _recordRepository.GetAsync(recordId);
        record.Status = "Processing";
        await _recordRepository.UpdateAsync(record, autoSave: true);

        var request = BuildRequest(record);
        var correlationId = record.Id.ToString("N");
        try
        {
            var bank = request.Bank ?? BankCode.Absa;
            var service = _resolver.GetAccountVerification(bank);
            var result = await service.VerifyAsync(request, correlationId);
            Apply(record, result, correlationId);
        }
        catch (Exception ex)
        {
            record.Status = "Error";
            record.ErrorMessage = ex.Message;
            record.CorrelationId = correlationId;
            Logger.LogError(ex, "Batch AVS failed for record {RecordId}", recordId);
        }

        await _recordRepository.UpdateAsync(record, autoSave: true);
        await uow.CompleteAsync();
    }

    private static AccountVerificationRequest BuildRequest(BankHubAvsRecord record)
    {
        VerifyAccountRequestDto? dto = null;
        if (!string.IsNullOrWhiteSpace(record.RequestBodyJson))
        {
            try { dto = JsonSerializer.Deserialize<VerifyAccountRequestDto>(record.RequestBodyJson); }
            catch { /* ignore */ }
        }

        return new AccountVerificationRequest
        {
            Bank = BankCodeParser.Parse(record.BankCode),
            AccountNumber = dto?.AccountNumber ?? record.AccountNumber,
            BranchCode = dto?.BranchCode ?? record.BranchCode,
            IssuingBankCode = dto?.IssuingBankCode,
            IdentityNumber = dto?.IdentityNumber ?? record.IdentityNumber,
            IdentityType = dto?.IdentityType,
            AccountHolderName = dto?.AccountHolderName ?? record.AccountHolderName,
            Initials = dto?.Initials,
            LastName = dto?.LastName ?? record.AccountHolderName,
            AccountType = dto?.AccountType,
            Email = dto?.Email,
            PhoneNumber = dto?.PhoneNumber,
            Reference = dto?.Reference ?? record.Reference
        };
    }

    private async Task RefreshCountsAsync(Guid batchId)
    {
        var batch = await _batchRepository.GetAsync(batchId);
        var records = await _recordRepository.GetListAsync(x => x.BatchId == batchId);
        batch.ProcessedCount = records.Count(x => x.Status is "Verified" or "NotVerified" or "Error");
        batch.VerifiedCount = records.Count(x => x.IsVerified || x.Status == "Verified");
        batch.FailedCount = records.Count(x => x.Status == "Error" || (x.Status == "NotVerified" && !x.IsVerified));
        batch.PendingCount = records.Count(x => x.Status is "Queued" or "Processing" or "Pending");
        batch.Status = batch.PendingCount == 0 ? "Completed" : "Processing";
        await _batchRepository.UpdateAsync(batch, autoSave: true);
    }

    private static void Apply(BankHubAvsRecord record, ApiResult<AccountVerificationResponse> result, string correlationId)
    {
        record.CorrelationId = correlationId;
        record.ResponseBodyJson = JsonSerializer.Serialize(result);
        if (!result.Success || result.Data is null)
        {
            record.Status = "Error";
            record.ResultCode = result.Error?.Code;
            record.ResultDescription = result.Error?.Message;
            record.ErrorMessage = result.Error?.Message;
            return;
        }

        var d = result.Data;
        record.IsVerified = d.IsVerified;
        record.ResultCode = d.ResultCode;
        record.ResultDescription = d.ResultDescription;
        record.BankResultCode = d.BankResultCode;
        record.BankReference = d.BankReference;
        record.AccountFound = d.AccountFound;
        record.AccountOpen = d.AccountOpen;
        record.AccountActive = d.AccountActive;
        record.IdentityMatch = d.IdentityMatch;
        record.NameMatch = d.NameMatch;
        record.InitialsMatch = d.InitialsMatch;
        record.EmailMatch = d.EmailMatch;
        record.PhoneMatch = d.PhoneMatch;
        record.AccountTypeMatch = d.AccountTypeMatch;
        record.AccountOpenLongerThan3Months = d.AccountOpenLongerThan3Months;
        record.AllowsCredit = d.AllowsCredit;
        record.AcceptsCredit = d.AcceptsCredit;
        record.AllowsDebit = d.AllowsDebit;
        record.AcceptsDebit = d.AcceptsDebit;
        record.MatchingCriteriaJson = d.MatchingCriteria is null ? null : JsonSerializer.Serialize(d.MatchingCriteria);
        record.Status = d.IsVerified ? "Verified" : "NotVerified";
        record.ErrorMessage = null;
    }
}
