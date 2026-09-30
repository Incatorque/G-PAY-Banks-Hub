using Volo.Abp.Application.Services;

namespace GPay.Banking.BankHub;

/// <summary>
/// Channel callback URL administration — register, amend, and unregister with the bank.
/// </summary>
/// <remarks>
/// <para>
/// Conventional HTTP routes (ABP):
/// </para>
/// <list type="bullet">
/// <item><c>GET /api/app/callback-registration/defaults</c></item>
/// <item><c>POST /api/app/callback-registration/register</c></item>
/// <item><c>POST /api/app/callback-registration/amend</c></item>
/// <item><c>POST /api/app/callback-registration/unregister</c></item>
/// </list>
/// <para>
/// Absa Payment API v1.8: Register + UnRegister only. Amend re-registers with new Uri/Token/SupportEmail.
/// Inbound webhooks still hit <c>POST /api/app/callback/process-payment?bank=Absa</c>.
/// </para>
/// </remarks>
public interface ICallbackRegistrationAppService : IApplicationService
{
    /// <summary>
    /// Returns recommended inbound URI and configured AbsaCapi callback defaults (no bank call).
    /// </summary>
    /// <remarks>Permission: <c>Banking.Payments.View</c>.</remarks>
    Task<PaymentCallbackDefaultsDto> GetDefaultsAsync(string bank = "Absa");

    /// <summary>
    /// Registers the channel callback URL with Absa (<c>/api/PaymentCallback/Register</c>).
    /// </summary>
    /// <remarks>Permission: <c>Banking.Payments.Initiate</c>.</remarks>
    Task<PaymentCallbackRegistrationResultDto> RegisterAsync(RegisterPaymentCallbackRequestDto input);

    /// <summary>
    /// Amends the channel callback URL (Absa: re-register with updated details).
    /// </summary>
    /// <remarks>Permission: <c>Banking.Payments.Initiate</c>.</remarks>
    Task<PaymentCallbackRegistrationResultDto> AmendAsync(RegisterPaymentCallbackRequestDto input);

    /// <summary>
    /// Unregisters the channel callback URL with Absa (<c>/api/PaymentCallback/UnRegister</c>).
    /// </summary>
    /// <remarks>Permission: <c>Banking.Payments.Initiate</c>.</remarks>
    Task<PaymentCallbackRegistrationResultDto> UnregisterAsync(UnregisterPaymentCallbackRequestDto input);
}
