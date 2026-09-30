namespace GPay.Banking.Domain.Dtos;

/// <summary>
/// Register or amend a bank payment-status callback URL.
/// </summary>
public sealed class PaymentCallbackRegistrationRequest
{
    /// <summary>HTTPS callback URI (port 443). Max 1024 characters.</summary>
    public required string Uri { get; init; }

    /// <summary>Shared auth token Absa will echo on inbound callbacks. Max 512 characters.</summary>
    public required string Token { get; init; }

    /// <summary>Support email Absa contacts after failed callback retries. Max 1024 characters.</summary>
    public required string SupportEmail { get; init; }
}
