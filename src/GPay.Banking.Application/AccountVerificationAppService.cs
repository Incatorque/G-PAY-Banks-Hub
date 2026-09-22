using System.Text.Json;
using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Helpers;
using GPay.Banking.Permissions;
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
/// Routes under <c>/api/app/account-verification/*</c>. Requires JWT and <c>Banking.Avs.*</c> permissions.
/// </remarks>
[Authorize(AvsPermissions.Default)]
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
    /// <returns>Normalized match flags, result codes, <c>correlationId</c>, and persisted <c>recordId</c>.</returns>
    /// <remarks>Permission: <c>Banking.Avs.Verify</c>. Persists a <c>BankHubAvsRecord</c>.</remarks>
    [Authorize(AvsPermissions.Verify)]
    public async Task<AccountVerificationResultDto> VerifyAsync(VerifyAccountRequestDto input)
    {
        var correlationId = Guid.NewGuid().ToString("N");
        var bank = BankCodeParser.Parse(input.Bank);
        var domainRequest = MapRequest(input);
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
            IsVerified = dto.IsVerified,
            ResultCode = dto.ResultCode,
            ResultDescription = dto.ResultDescription,
            BankResultCode = dto.BankResultCode,
            BankReference = dto.BankReference,
            AccountFound = dto.AccountFound,
            AccountOpen = dto.AccountOpen,
            AccountActive = dto.AccountActive,
            IdentityMatch = dto.IdentityMatch,
            NameMatch = dto.NameMatch,
            InitialsMatch = dto.InitialsMatch,
            EmailMatch = dto.EmailMatch,
            PhoneMatch = dto.PhoneMatch,
            AccountTypeMatch = dto.AccountTypeMatch,
            AccountOpenLongerThan3Months = dto.AccountOpenLongerThan3Months,
            AllowsCredit = dto.AllowsCredit,
            AcceptsCredit = dto.AcceptsCredit,
            AllowsDebit = dto.AllowsDebit,
            AcceptsDebit = dto.AcceptsDebit,
            SuccessRate = dto.SuccessRate,
            MatchingCriteriaJson = dto.MatchingCriteria is null ? null : JsonSerializer.Serialize(dto.MatchingCriteria),
            RequestBodyJson = JsonSerializer.Serialize(domainRequest),
            ResponseBodyJson = JsonSerializer.Serialize(result),
            ErrorMessage = dto.ErrorMessage,
            Status = dto.IsVerified ? "Verified" : (result.Success ? "NotVerified" : "Error")
        };
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
    /// Permission: <c>Banking.Avs.Upload</c>.
    /// Max items: <c>AvsBatch:MaxItems</c> (default 20 000). Processed by background job; poll get-batch for progress.
    /// </remarks>
    [Authorize(AvsPermissions.Upload)]
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
        batch.SetIdForInsert(batchId);

        await _batchRepository.InsertAsync(batch, autoSave: true);

        foreach (var item in input.Items.OrderBy(x => x.RowNumber))
        {
            var req = item.Request;
            var record = new BankHubAvsRecord
            {
                BatchId = batchId,
                ApiClientId = input.ApiClientId,
                RowNumber = item.RowNumber,
                BankCode = string.IsNullOrWhiteSpace(req.Bank) ? "Absa" : req.Bank,
                Reference = req.Reference,
                AccountNumber = req.AccountNumber,
                BranchCode = req.BranchCode,
                IdentityNumber = req.IdentityNumber,
                AccountHolderName = req.AccountHolderName ?? req.LastName,
                ResultCode = "Queued",
                Status = "Queued",
                RequestBodyJson = JsonSerializer.Serialize(req)
            };
            await _recordRepository.InsertAsync(record, autoSave: false);
        }

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
    /// <remarks>Permission: <c>Banking.Avs.View</c>.</remarks>
    [Authorize(AvsPermissions.View)]
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
    /// <remarks>Permission: <c>Banking.Avs.View</c>.</remarks>
    [Authorize(AvsPermissions.View)]
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

    private static AccountVerificationRequest MapRequest(VerifyAccountRequestDto input) => new()
    {
        Bank = BankCodeParser.Parse(input.Bank),
        AccountNumber = input.AccountNumber,
        BranchCode = input.BranchCode,
        IssuingBankCode = input.IssuingBankCode,
        IdentityNumber = input.IdentityNumber,
        IdentityType = input.IdentityType,
        AccountHolderName = input.AccountHolderName,
        Initials = input.Initials,
        LastName = input.LastName,
        AccountType = input.AccountType,
        Email = input.Email,
        PhoneNumber = input.PhoneNumber,
        Reference = input.Reference
    };

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
                ErrorMessage = result.Error?.Message
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
            SuccessRate = CalculateSuccessRate(d)
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
