using Volo.Abp.Application.Services;

namespace GPay.Banking;

/// <summary>
/// Inbound bank payment callback (webhook) API.
/// </summary>
/// <remarks>
/// <para>
/// Conventional HTTP route:
/// <c>POST /api/app/callback/process-payment?bank=Absa</c>
/// </para>
/// <para>
/// Anonymous (no JWT). Security:
/// </para>
/// <list type="number">
/// <item>Optional <c>bank</c> query; when omitted, resolved from source IP / domain allow-lists.</item>
/// <item>Source allow-list check per bank (see <c>BankCallback</c> / Absa callback options).</item>
/// <item>Bank adapter validates shared <c>Token</c> in the JSON body and optional <c>X-Signature</c>.</item>
/// </list>
/// <para>
/// Request body is the <b>bank-native</b> callback JSON (not a GPay DTO). Absa may retry on non-success —
/// this endpoint is idempotent. After several failed retries Absa emails the registered SupportEmail.
/// </para>
/// <para>
/// Register with Absa: <c>POST /api/PaymentCallback/Register</c> using this URL, a shared Token, and SupportEmail.
/// Port 443 only.
/// </para>
/// </remarks>
public interface ICallbackAppService : IApplicationService
{
    /// <summary>
    /// Processes a bank payment status callback and updates the matching payment record when found.
    /// </summary>
    /// <param name="bank">
    /// Bank key: <c>Absa</c>, <c>Fnb</c>, or <c>Nedbank</c>.
    /// Omit to resolve from configured source IP / domain allow-lists.
    /// </param>
    /// <returns>
    /// Acknowledgement shaped for the bank (<c>success</c> / <c>message</c>).
    /// Absa expects a successful HTTP response with this payload.
    /// </returns>
    Task<CallbackAckDto> ProcessPaymentAsync(string? bank = null);
}
