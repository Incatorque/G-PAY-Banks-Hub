namespace GPay.Banking.BankHub;

/// <summary>
/// Result of register / amend / unregister callback URL against the bank channel.
/// </summary>
public class PaymentCallbackRegistrationResultDto
{
    /// <summary>True when the bank accepted the operation.</summary>
    public bool Success { get; set; }

    /// <summary>Hub correlation id for this call.</summary>
    public string? CorrelationId { get; set; }

    /// <summary>Normalized status: Registered, Amended, Unregistered, Failed, Error.</summary>
    public string? Status { get; set; }

    /// <summary>Callback URI sent to the bank.</summary>
    public string? Uri { get; set; }

    /// <summary>Bank correlation id when returned.</summary>
    public string? BankCorrelationId { get; set; }

    /// <summary>Human-readable result description.</summary>
    public string? ResultDescription { get; set; }

    /// <summary>Error code when unsuccessful.</summary>
    public string? ErrorCode { get; set; }

    /// <summary>Error message when unsuccessful.</summary>
    public string? ErrorMessage { get; set; }

    /// <summary>Absa catalog entry when an Absa error code is present.</summary>
    public AbsaErrorInfo? AbsaError { get; set; }
}
