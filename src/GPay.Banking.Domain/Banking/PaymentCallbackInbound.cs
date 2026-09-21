namespace GPay.Banking.Domain;

/// <summary>
/// Raw inbound payment callback from a bank webhook.
/// </summary>
public sealed class PaymentCallbackInbound
{
    /// <summary>
    /// Shared secret / token supplied by the bank (header or body).
    /// </summary>
    public string? Token { get; init; }

    /// <summary>
    /// Serialized bank-native JSON payload.
    /// </summary>
    public required string RawJson { get; init; }
}
