namespace GPay.Banking.Helpers;

public sealed class CallbackSourceCheckResult
{
    public bool IsAllowed { get; init; }

    public string Message { get; init; } = string.Empty;

    public static CallbackSourceCheckResult Allowed(string message) => new()
    {
        IsAllowed = true,
        Message = message
    };

    public static CallbackSourceCheckResult Denied(string message) => new()
    {
        IsAllowed = false,
        Message = message
    };
}
