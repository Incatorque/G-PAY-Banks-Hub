namespace GPay.Banking;

/// <summary>
/// Inbound bank webhook security (source IP / domain allow-lists and tokens).
/// </summary>
public sealed class BankCallbackOptions
{
    public const string SectionName = "BankCallbacks";

    /// <summary>
    /// When true, requests are rejected if the bank has AllowedSourceIps/AllowedSourceDomains
    /// and the source matches none. When false, allow-lists are skipped.
    /// </summary>
    public bool EnforceSourceAllowList { get; set; }

    /// <summary>
    /// Per-bank callback settings. Keys: Absa, Fnb, Nedbank.
    /// </summary>
    public Dictionary<string, BankCallbackBankOptions> Banks { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}
