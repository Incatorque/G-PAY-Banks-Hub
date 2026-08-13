namespace GPay.Banking.Contracts.Dtos.Balances;

/// <summary>
/// GPay account balance enquiry response.
/// </summary>
public sealed class BalanceResponse
{
    /// <summary>
    /// Account number enquired.
    /// </summary>
    public required string AccountNumber { get; init; }

    /// <summary>
    /// Available balance.
    /// </summary>
    public decimal AvailableBalance { get; init; }

    /// <summary>
    /// Current / ledger balance.
    /// </summary>
    public decimal CurrentBalance { get; init; }

    /// <summary>
    /// ISO 4217 currency code.
    /// </summary>
    public string Currency { get; init; } = "ZAR";

    /// <summary>
    /// Balance as-of timestamp (UTC).
    /// </summary>
    public DateTimeOffset AsOfUtc { get; init; }
}
