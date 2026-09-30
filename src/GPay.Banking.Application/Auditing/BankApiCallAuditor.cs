using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace GPay.Banking;

/// <summary>
/// Writes <see cref="BankHubApiCall"/> rows in their own unit of work.
/// </summary>
public sealed class BankApiCallAuditor : IBankApiCallAuditor
{
    private readonly IRepository<BankHubApiCall, Guid> _repository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public BankApiCallAuditor(
        IRepository<BankHubApiCall, Guid> repository,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _repository = repository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task RecordAsync(BankApiCallAuditEntry entry, CancellationToken cancellationToken = default)
    {
        using var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: true);
        await _repository.InsertAsync(new BankHubApiCall
        {
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
        }, autoSave: true, cancellationToken: cancellationToken);
        await uow.CompleteAsync(cancellationToken);
    }
}
