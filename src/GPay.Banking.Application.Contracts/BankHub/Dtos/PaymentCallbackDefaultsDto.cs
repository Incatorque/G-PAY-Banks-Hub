namespace GPay.Banking.BankHub;

/// <summary>
/// Recommended inbound callback URL and configured AbsaCapi defaults (no bank call).
/// </summary>
public class PaymentCallbackDefaultsDto
{
    /// <summary>Target bank key.</summary>
    public string Bank { get; set; } = "Absa";

    /// <summary>
    /// Recommended hub inbound URL for Absa:
    /// <c>/api/app/callback/process-payment?bank=Absa</c> on this host (HTTPS).
    /// </summary>
    public string? RecommendedInboundUri { get; set; }

    /// <summary>Configured <c>AbsaCapi:PaymentCallbackUri</c>.</summary>
    public string? ConfiguredUri { get; set; }

    /// <summary>True when <c>AbsaCapi:PaymentCallbackToken</c> is set (value not returned).</summary>
    public bool TokenConfigured { get; set; }

    /// <summary>Configured <c>AbsaCapi:PaymentCallbackSupportEmail</c>.</summary>
    public string? ConfiguredSupportEmail { get; set; }

    /// <summary>True when inbound <c>BankCallbacks:Banks:Absa:PaymentToken</c> is set.</summary>
    public bool InboundTokenConfigured { get; set; }

    /// <summary>Absa register path (relative to CAPI base).</summary>
    public string RegisterPath { get; set; } = "/api/PaymentCallback/Register";

    /// <summary>Absa unregister path (relative to CAPI base).</summary>
    public string UnregisterPath { get; set; } = "/api/PaymentCallback/UnRegister";
}
