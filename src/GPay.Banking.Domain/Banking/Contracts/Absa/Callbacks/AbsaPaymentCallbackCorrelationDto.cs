namespace GPay.Banking.Domain.Dtos;

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
