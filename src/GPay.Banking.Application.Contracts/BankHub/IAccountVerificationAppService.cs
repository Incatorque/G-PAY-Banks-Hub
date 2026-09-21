using Volo.Abp.Application.Services;

namespace GPay.Banking.BankHub;

public interface IAccountVerificationAppService : IApplicationService
{
    Task<AccountVerificationResultDto> VerifyAsync(VerifyAccountRequestDto input);
    Task<UploadAvsBatchResultDto> SubmitBatchAsync(SubmitAvsBatchRequestDto input);
    Task<BankHubAvsBatchDto> GetBatchAsync(Guid id);
    Task<List<BankHubAvsRecordDto>> GetBatchRecordsAsync(Guid batchId);
}
