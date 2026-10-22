namespace GPay.Banking.BankHub;

/// <summary>
/// Register or amend the Absa channel payment-status callback URL.
/// </summary>
/// <remarks>
/// Absa: <c>POST /api/PaymentCallback/Register</c>. HTTPS port 443 only.
/// Omit Uri/Token/SupportEmail to use <c>AbsaCapi:PaymentCallback*</c> defaults.
/// Keep <c>BankCallbacks:Banks:Absa:PaymentToken</c> equal to <see cref="Token"/>.
/// </remarks>
public class RegisterPaymentCallbackRequestDto
{
    /// <summary>Target bank. Currently only <c>Absa</c>.</summary>
    /// <example>Absa</example>
    public string Bank { get; set; } = "Absa";

    /// <summary>
    /// Public HTTPS callback URI Absa will POST to.
    /// Typical hub path: <c>https://&lt;host&gt;/api/app/callback/process-payment?bank=Absa</c>.
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>Shared secret Absa includes in inbound callback JSON.</summary>
    public string? Token { get; set; }

    /// <summary>Email Absa notifies after exhausted callback retries.</summary>
    public string? SupportEmail { get; set; }
}
