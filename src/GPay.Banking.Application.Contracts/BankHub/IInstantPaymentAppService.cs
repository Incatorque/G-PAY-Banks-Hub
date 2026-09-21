using Volo.Abp.Application.Services;

namespace GPay.Banking.BankHub;

public interface IInstantPaymentAppService : IApplicationService
{
    Task<PaymentResultDto> InitiateAsync(InitiatePaymentRequestDto input);
    Task<PaymentResultDto> GetStatusAsync(PaymentStatusRequestDto input);
    Task<PaymentRecordDto> GetAsync(Guid id);
}
