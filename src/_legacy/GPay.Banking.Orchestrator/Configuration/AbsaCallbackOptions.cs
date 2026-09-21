namespace GPay.Banking.Orchestrator.Configuration;

/// <summary>
/// Absa inbound callback verification settings.
/// </summary>
public sealed class AbsaCallbackOptions
{
    /// <summary>
    /// Configuration section name.
    /// </summary>
    public const string SectionName = "AbsaCallback";

    /// <summary>
    /// Shared secret Token expected on Absa payment callbacks.
    /// </summary>
    public string PaymentToken { get; set; } = string.Empty;
}
