namespace GPay.Banking.Domain.Dtos;

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
