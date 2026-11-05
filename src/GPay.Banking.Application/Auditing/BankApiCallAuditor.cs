using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace GPay.Banking.Auditing;

/// <summary>
/// Writes <see cref="BankHubApiCall"/> rows in their own unit of work.
/// </summary>
internal sealed class BankApiCallAuditor(
    IRepository<BankHubApiCall, Guid> repository,
    IUnitOfWorkManager unitOfWorkManager,
    ILogger<BankApiCallAuditor> logger) : IBankApiCallAuditor
{
    public async Task RecordAsync(BankApiCallAuditEntry entry, CancellationToken cancellationToken = default)
    {
        using var uow = unitOfWorkManager.Begin(requiresNew: true, isTransactional: true);

        var apiCall = new BankHubApiCall
        {
            GpayOrderId = entry.GpayOrderId,
            BankCode = entry.BankCode,
            Direction = entry.Direction,
            Operation = entry.Operation,
            HttpMethod = entry.HttpMethod,
            Path = entry.Path,
            HttpStatus = entry.HttpStatus,
            CorrelationId = entry.CorrelationId,
            RequestJson = entry.RequestJson,
            ResponseJson = entry.ResponseJson,
            DurationMs = entry.DurationMs,
            Success = entry.Success,
            ErrorCode = entry.ErrorCode
        };

        await repository.InsertAsync(apiCall, autoSave: true, cancellationToken: cancellationToken);

        await uow.CompleteAsync(cancellationToken);

        logger.LogInformation(
            "Stored bank API audit row {ApiCallId} for {BankCode} {Direction} {Operation}.",
            apiCall.Id,
            apiCall.BankCode,
            apiCall.Direction,
            apiCall.Operation);
    }
}
