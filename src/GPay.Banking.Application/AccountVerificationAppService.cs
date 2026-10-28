using System.Text.Json;
using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Uow;

namespace GPay.Banking;

/// <summary>
/// Account verification (AVS) AppService — single enquiry and async batch APIs.
/// </summary>
/// <remarks>
/// Routes under <c>/api/app/account-verification/*</c>.
/// </remarks>
[Authorize]
public class AccountVerificationAppService : ApplicationService, IAccountVerificationAppService
{
    private readonly IBankCapabilityResolver _resolver;
    private readonly IRepository<BankHubAvsBatch, Guid> _batchRepository;
    private readonly IRepository<BankHubAvsRecord, Guid> _recordRepository;
    private readonly IBackgroundJobManager _backgroundJobs;
    private readonly AvsBatchOptions _batchOptions;
    private readonly IGuidGenerator _guidGenerator;

    public AccountVerificationAppService(
        IBankCapabilityResolver resolver,
        IRepository<BankHubAvsBatch, Guid> batchRepository,
        IRepository<BankHubAvsRecord, Guid> recordRepository,
        IBackgroundJobManager backgroundJobs,
        IOptions<AvsBatchOptions> batchOptions,
        IGuidGenerator guidGenerator)
    {
        _resolver = resolver;
        _batchRepository = batchRepository;
        _recordRepository = recordRepository;
        _backgroundJobs = backgroundJobs;
        _batchOptions = batchOptions.Value;
        _guidGenerator = guidGenerator;
    }

    /// <summary>
    /// Verifies a single bank account (AVS) via the bank named in the request body.
    /// </summary>
    /// <param name="input">Account, branch, identity/name fields, and target <c>Bank</c>.</param>
    /// <returns>Normalized match flags, result codes, <c>correlationId</c>, and persisted AVS <c>recordId</c>.</returns>
    /// <remarks>Persists a <c>BankHubAvsRecord</c>.</remarks>
    public async Task<AccountVerificationResultDto> VerifyAsync(VerifyAccountRequestDto input)
    {
        var correlationId = Guid.NewGuid().ToString("N");
        var bank = BankCodeParser.Parse(input.Bank);
        var domainRequest = AccountVerificationRequestMapper.From(input, bank);
        var service = _resolver.GetAccountVerification(bank);
        var result = await service.VerifyAsync(domainRequest, correlationId);

        var dto = MapResult(result, correlationId);
        var record = new BankHubAvsRecord
        {
            ApiClientId = input.ApiClientId,
            BankCode = input.Bank,
            Reference = input.Reference,
            CorrelationId = correlationId,
            AccountNumber = input.AccountNumber,
            BranchCode = input.BranchCode,
            IdentityNumber = input.IdentityNumber,
            AccountHolderName = input.AccountHolderName ?? input.LastName,
            SuccessRate = dto.SuccessRate,
            RequestBodyJson = JsonSerializer.Serialize(domainRequest),
            ResponseBodyJson = JsonSerializer.Serialize(result),
            ErrorMessage = dto.ErrorMessage,
        };
        if (result.Success && result.Data is { } response)
        {
            BankHubAvsRecordMapper.Apply(record, response);
        }
        else
        {
            record.Status = dto.IsVerified ? "Verified" : result.Success ? "NotVerified" : "Error";
        }

        await _recordRepository.InsertAsync(record, autoSave: true);
        dto.RecordId = record.Id;
        return dto;
    }

    /// <summary>
    /// Accepts an AVS batch from an external API client (not the Angular dashboard).
    /// </summary>
    /// <param name="input">Batch metadata and ordered verification items.</param>
    /// <returns>Queued batch id and initial processing status.</returns>
    /// <remarks>
    /// Max items: <c>AvsBatch:MaxItems</c> (default 20 000). Processed by background job; poll get-batch for progress.
    /// </remarks>
    public async Task<UploadAvsBatchResultDto> SubmitBatchAsync(SubmitAvsBatchRequestDto input)
    {
        if (input.Items is null || input.Items.Count == 0)
        {
            throw new UserFriendlyException("Batch contains no items.");
        }

        if (input.Items.Count > _batchOptions.MaxItems)
        {
            throw new UserFriendlyException($"Batch exceeds maximum of {_batchOptions.MaxItems:N0} items.");
        }

        var batchId = _guidGenerator.Create();
        var banks = input.Items.Select(i => string.IsNullOrWhiteSpace(i.Request.Bank) ? "Absa" : i.Request.Bank)
            .Distinct(StringComparer.OrdinalIgnoreCase).ToList();

        var batch = new BankHubAvsBatch
        {
            FileName = input.FileName ?? $"api-batch-{batchId:N}.json",
            TotalRows = input.Items.Count,
            PendingCount = input.Items.Count,
            ProcessedCount = 0,
            VerifiedCount = 0,
            FailedCount = 0,
            SegmentCount = 1,
            BankCode = banks.Count == 1 ? banks[0] : "Mixed",
            Status = "Queued",
            ExternalBatchId = batchId,
            ApiClientId = input.ApiClientId
        };
        Volo.Abp.Domain.Entities.EntityHelper.TrySetId(batch, () => batchId);

        await _batchRepository.InsertAsync(batch, autoSave: true);

        var records = input.Items.OrderBy(item => item.RowNumber)
            .Select(item =>
            {
                var request = item.Request;
                return new BankHubAvsRecord
                {
                    BatchId = batchId,
                    ApiClientId = input.ApiClientId,
                    RowNumber = item.RowNumber,
                    BankCode = string.IsNullOrWhiteSpace(request.Bank) ? "Absa" : request.Bank,
                    Reference = request.Reference,
                    AccountNumber = request.AccountNumber,
                    BranchCode = request.BranchCode,
                    IdentityNumber = request.IdentityNumber,
                    AccountHolderName = request.AccountHolderName ?? request.LastName,
                    ResultCode = "Queued",
                    Status = "Queued",
                    RequestBodyJson = JsonSerializer.Serialize(request)
                };
            })
            .ToList();
        await _recordRepository.InsertManyAsync(records, autoSave: false);

        await CurrentUnitOfWork!.SaveChangesAsync();

        await _backgroundJobs.EnqueueAsync(new AvsBatchProcessJobArgs { BatchId = batchId });

        batch.Status = "Processing";
        await _batchRepository.UpdateAsync(batch, autoSave: true);

        return new UploadAvsBatchResultDto
        {
            BatchId = batchId,
            TotalRows = batch.TotalRows,
            SegmentCount = batch.SegmentCount,
            Status = batch.Status
        };
    }

    /// <summary>
    /// Returns batch header progress (counts, percent complete, status).
    /// </summary>
    /// <param name="id">Batch id returned by submit-batch.</param>
    public async Task<BankHubAvsBatchDto> GetBatchAsync(Guid id)
    {
        var batch = await _batchRepository.GetAsync(id);
        var percent = batch.TotalRows == 0 ? 100d : Math.Round(100d * batch.ProcessedCount / batch.TotalRows, 2);
        return new BankHubAvsBatchDto
        {
            Id = batch.Id,
            FileName = batch.FileName,
            TotalRows = batch.TotalRows,
            ProcessedCount = batch.ProcessedCount,
            VerifiedCount = batch.VerifiedCount,
            FailedCount = batch.FailedCount,
            PendingCount = batch.PendingCount,
            SegmentCount = batch.SegmentCount,
            PercentComplete = percent,
            Status = batch.Status,
            BankCode = batch.BankCode,
            ApiClientId = batch.ApiClientId,
            CreationTime = batch.CreationTime
        };
    }

    /// <summary>
    /// Returns all AVS records for a batch, ordered by row number.
    /// </summary>
    /// <param name="batchId">Batch id.</param>
    public async Task<List<BankHubAvsRecordDto>> GetBatchRecordsAsync(Guid batchId)
    {
        var records = await _recordRepository.GetListAsync(x => x.BatchId == batchId);
        return records.OrderBy(x => x.RowNumber).Select(r => new BankHubAvsRecordDto
        {
            Id = r.Id,
            BatchId = r.BatchId,
            RowNumber = r.RowNumber,
            BankCode = r.BankCode,
            AccountNumber = r.AccountNumber,
            BranchCode = r.BranchCode,
            IsVerified = r.IsVerified,
            ResultCode = r.ResultCode,
            ResultDescription = r.ResultDescription,
            SuccessRate = r.SuccessRate,
            Status = r.Status,
            ErrorMessage = r.ErrorMessage,
            CorrelationId = r.CorrelationId
        }).ToList();
    }

    private static AccountVerificationResultDto MapResult(ApiResult<AccountVerificationResponse> result, string correlationId)
    {
        if (!result.Success || result.Data is null)
        {
            return new AccountVerificationResultDto
            {
                Success = false,
                CorrelationId = correlationId,
                ResultCode = result.Error?.Code,
                ResultDescription = result.Error?.Message,
                ErrorMessage = result.Error?.Message,
                AbsaError = AbsaErrorResponses.From(result.Error)
            };
        }

        var d = result.Data;
        return new AccountVerificationResultDto
        {
            Success = true,
            IsVerified = d.IsVerified,
            AccountFound = d.AccountFound,
            AccountOpen = d.AccountOpen,
            AccountActive = d.AccountActive,
            IdentityMatch = d.IdentityMatch,
            NameMatch = d.NameMatch,
            InitialsMatch = d.InitialsMatch,
            EmailMatch = d.EmailMatch,
            PhoneMatch = d.PhoneMatch,
            AccountTypeMatch = d.AccountTypeMatch,
            AccountOpenLongerThan3Months = d.AccountOpenLongerThan3Months,
            AllowsCredit = d.AllowsCredit,
            AcceptsCredit = d.AcceptsCredit,
            AllowsDebit = d.AllowsDebit,
            AcceptsDebit = d.AcceptsDebit,
            ResultCode = d.ResultCode,
            ResultDescription = d.ResultDescription,
            BankResultCode = d.BankResultCode,
            BankReference = d.BankReference,
            Reference = d.Reference,
            MatchingCriteria = d.MatchingCriteria,
            CorrelationId = correlationId,
            SuccessRate = CalculateSuccessRate(d),
            AbsaError = AbsaErrorResponses.FromCode(d.BankResultCode, d.ResultDescription) is { Known: true } error
                ? error
                : null
        };
    }

    private static decimal CalculateSuccessRate(AccountVerificationResponse data)
    {
        static bool? Flag(string? v) => v switch
        {
            "Y" or "Yes" => true,
            "N" or "No" => false,
            _ => null
        };

        var checks = new bool?[] { data.AccountFound, data.AccountOpen, Flag(data.IdentityMatch), Flag(data.NameMatch), Flag(data.InitialsMatch) };
        var known = checks.Count(c => c.HasValue);
        if (known == 0) return data.IsVerified ? 100m : 0m;
        return Math.Round(100m * checks.Count(c => c == true) / known, 1);
    }

}
