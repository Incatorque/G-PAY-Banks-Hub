using Volo.Abp.Application.Services;

namespace GPay.Banking;

/// <summary>
/// Inbound bank payment callback (webhook) API.
/// </summary>
/// <remarks>
/// <para>
/// Conventional HTTP route:
/// <c>POST /api/app/callback/process-payment?bank={BankCode}</c>
/// </para>
/// <para>
/// Anonymous (no JWT). Security:
/// </para>
/// <list type="number">
/// <item>Optional <c>bank</c> query; when omitted, resolved from source IP / domain allow-lists.</item>
/// <item>Source allow-list check per bank (see <c>BankCallback</c> / Absa callback options).</item>
/// <item>The selected bank adapter validates its native callback format and configured authentication policy.</item>
/// </list>
/// <para>
/// Request body is the <b>bank-native</b> callback JSON (not a GPay DTO). Callback retry and support-contact
/// behavior depends on the bank's integration contract. The endpoint applies payment updates idempotently.
/// </para>
/// <para>
/// Banks that provide a callback registration API can be configured through the callback registration endpoint.
/// Other banks require callback onboarding through their own channel process.
/// </para>
/// </remarks>
public interface ICallbackAppService : IApplicationService
{
    /// <summary>
    /// Processes a bank payment status callback and updates the matching payment record when found.
    /// </summary>
    /// <param name="bank">
    /// Bank key supported by Bank Hub, for example <c>Absa</c> or <c>Capitec</c>.
    /// Omit to resolve from configured source IP / domain allow-lists.
    /// </param>
    /// <returns>
    /// Acknowledgement shaped for the bank (<c>success</c> / <c>message</c>).
    /// The bank-specific callback adapter determines the expected acknowledgement.
    /// </returns>
    Task<CallbackAckDto> ProcessPaymentAsync(string? bank = null);
}
