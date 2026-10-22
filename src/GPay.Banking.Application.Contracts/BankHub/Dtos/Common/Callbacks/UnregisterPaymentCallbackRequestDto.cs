namespace GPay.Banking.BankHub;

/// <summary>
/// Unregister a previously registered Absa channel payment-status callback URL.
/// </summary>
/// <remarks>
/// Absa: <c>POST /api/PaymentCallback/UnRegister</c>.
/// Omit Uri to use <c>AbsaCapi:PaymentCallbackUri</c>.
/// </remarks>
public class UnregisterPaymentCallbackRequestDto
{
    /// <summary>Target bank. Currently only <c>Absa</c>.</summary>
    /// <example>Absa</example>
    public string Bank { get; set; } = "Absa";

    /// <summary>Callback URI previously registered with Absa.</summary>
    public string? Uri { get; set; }
}
