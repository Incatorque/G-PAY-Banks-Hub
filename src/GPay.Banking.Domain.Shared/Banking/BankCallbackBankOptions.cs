namespace GPay.Banking;

/// <summary>
/// Callback security settings for a single bank.
/// </summary>
public sealed class BankCallbackBankOptions
{
    /// <summary>
    /// Shared secret / token the bank sends on payment callbacks.
    /// </summary>
    public string PaymentToken { get; set; } = string.Empty;

    /// <summary>
    /// Bank egress IPs or CIDRs (e.g. 196.0.0.0/24).
    /// </summary>
    public List<string> AllowedSourceIps { get; set; } = [];

    /// <summary>
    /// Expected reverse-DNS suffixes or hostnames (e.g. absa.co.za).
    /// </summary>
    public List<string> AllowedSourceDomains { get; set; } = [];
}
