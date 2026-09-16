namespace GPay.Banking.Contracts.Dtos.InstantPayment;

/// <summary>
/// Inbound Absa payment callback payload (MIG – Payment API v1.8).
/// Shared with Orchestrator so bank adapters are not referenced by the API host.
/// </summary>
public sealed class AbsaPaymentCallbackDto
{
    /// <summary>
    /// Shared secret token registered with Absa.
    /// </summary>
    public string? Token { get; init; }

    /// <summary>
    /// Payment type / rail: RPP, IIP, or PAAF.
    /// </summary>
    public string? Type { get; init; }

    /// <summary>
    /// Payment status from Absa.
    /// </summary>
    public object? PaymentStatus { get; init; }

    /// <summary>
    /// Correlations: 1=source stmt, 2=target stmt, 3=ApiRef, 4=TransactionRef.
    /// </summary>
    public List<AbsaPaymentCallbackCorrelationDto>? Correlations { get; init; }

    /// <summary>
    /// Optional error list.
    /// </summary>
    public List<AbsaPaymentCallbackErrorDto>? ErrorList { get; init; }

    /// <summary>
    /// Optional amount echo.
    /// </summary>
    public string? Amount { get; init; }

    /// <summary>
    /// Optional currency echo.
    /// </summary>
    public string? CurrencyCode { get; init; }
}

/// <summary>
/// Correlation item on an Absa payment callback.
/// </summary>
public sealed class AbsaPaymentCallbackCorrelationDto
{
    /// <summary>
    /// Correlation type (1–4).
    /// </summary>
    public int Type { get; init; }

    /// <summary>
    /// Correlation value.
    /// </summary>
    public string? Value { get; init; }
}

/// <summary>
/// Error item on an Absa payment callback.
/// </summary>
public sealed class AbsaPaymentCallbackErrorDto
{
    /// <summary>
    /// Error code.
    /// </summary>
    public string? Code { get; init; }

    /// <summary>
    /// Error description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Error message.
    /// </summary>
    public string? Message { get; init; }
}
