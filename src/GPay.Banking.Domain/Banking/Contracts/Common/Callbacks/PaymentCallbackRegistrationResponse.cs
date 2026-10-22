namespace GPay.Banking.Domain.Dtos;

/// <summary>
/// Result of a bank callback URL register / amend / unregister call.
/// </summary>
public sealed class PaymentCallbackRegistrationResponse
{
    /// <summary>Normalized outcome label (Registered, Amended, Unregistered, Failed).</summary>
    public required string Status { get; init; }

    /// <summary>Callback URI sent to the bank.</summary>
    public string? Uri { get; init; }

    /// <summary>Bank correlation id when returned.</summary>
    public string? BankCorrelationId { get; init; }

    /// <summary>Human-readable result description.</summary>
    public string? ResultDescription { get; init; }

    /// <summary>Bank / normalized error code when failed.</summary>
    public string? ErrorCode { get; init; }
}
