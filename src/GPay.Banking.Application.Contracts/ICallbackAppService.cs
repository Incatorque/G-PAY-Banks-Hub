using Volo.Abp.Application.Services;

namespace GPay.Banking;

public interface ICallbackAppService : IApplicationService
{
    /// <summary>
    /// Processes a bank payment callback.
    /// Pass <paramref name="bank"/> (Absa/Fnb/Nedbank), or omit to resolve from source IP/domain allow-lists.
    /// </summary>
    Task<CallbackAckDto> ProcessPaymentAsync(string? bank = null);
}
