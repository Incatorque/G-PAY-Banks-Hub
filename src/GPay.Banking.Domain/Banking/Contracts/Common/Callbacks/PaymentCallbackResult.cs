namespace GPay.Banking.Domain;

/// <summary>
/// Outcome of a bank payment callback after bank-specific validation/mapping.
/// </summary>
public sealed class PaymentCallbackResult
{
    public bool IsAuthorized { get; init; }

    public bool Success { get; init; }

    public string Message { get; init; } = string.Empty;

    public PaymentCallbackUpdate? Update { get; init; }

    public static PaymentCallbackResult Unauthorized(string message) => new()
    {
        Message = message
    };

    public static PaymentCallbackResult Ok(PaymentCallbackUpdate? update = null) => new()
    {
        IsAuthorized = true,
        Success = true,
        Update = update
    };

    public static PaymentCallbackResult Fail(string message) => new()
    {
        IsAuthorized = true,
        Message = message
    };
}
