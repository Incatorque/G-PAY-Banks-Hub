namespace GPay.Banking.Domain;

/// <summary>
/// Outcome of a bank payment callback after bank-specific validation/mapping.
/// </summary>
public sealed class PaymentCallbackResult
{
    public bool IsAuthorized { get; init; }

    public bool IsDuplicate { get; init; }

    public bool Success { get; init; }

    public string Message { get; init; } = string.Empty;

    public PaymentCallbackUpdate? Update { get; init; }

    public static PaymentCallbackResult Unauthorized(string message) => new()
    {
        IsAuthorized = false,
        Success = false,
        Message = message
    };

    public static PaymentCallbackResult Duplicate() => new()
    {
        IsAuthorized = true,
        IsDuplicate = true,
        Success = true,
        Message = string.Empty
    };

    public static PaymentCallbackResult Ok(PaymentCallbackUpdate? update = null) => new()
    {
        IsAuthorized = true,
        Success = true,
        Message = string.Empty,
        Update = update
    };

    public static PaymentCallbackResult Fail(string message) => new()
    {
        IsAuthorized = true,
        Success = false,
        Message = message
    };
}
