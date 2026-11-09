using Volo.Abp.Application.Services;

namespace GPay.Banking.BankHub;

/// <summary>Provides authenticated read access to the Bank Hub bank-call audit log.</summary>
public interface IOperationsDashboardAppService : IApplicationService
{
    /// <summary>Returns paged call metadata with filters and aggregate counts.</summary>
    Task<BankApiCallPageDto> GetCallsAsync(BankApiCallQueryDto input);

    /// <summary>Returns one call with its redacted request and response payloads.</summary>
    Task<BankApiCallDto> GetCallAsync(Guid id);

    /// <summary>Returns GPay transactions currently queued for Bank Hub or submitted for status polling.</summary>
    Task<BankPaymentQueuePageDto> GetTransactionsAsync(BankPaymentQueueQueryDto input);
}
