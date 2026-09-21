using GPay.Banking.Contracts.Enums;

namespace GPay.Banking.Infrastructure.Configuration;

/// <summary>
/// Registered bank and its queue / probe endpoints.
/// </summary>
public sealed class BankRegistration
{
    /// <summary>
    /// Bank code.
    /// </summary>
    public BankCode BankCode { get; set; }

    /// <summary>
    /// Display name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Request queue name.
    /// </summary>
    public string RequestQueue { get; set; } = string.Empty;

    /// <summary>
    /// Optional health probe URL for the bank service.
    /// </summary>
    public string? HealthUrl { get; set; }
}

/// <summary>
/// Platform bank registry configuration.
/// </summary>
public sealed class BankingOptions
{
    /// <summary>
    /// Configuration section name.
    /// </summary>
    public const string SectionName = "Banking";

    /// <summary>
    /// Registered banks.
    /// </summary>
    public List<BankRegistration> Banks { get; set; } = [];
}
