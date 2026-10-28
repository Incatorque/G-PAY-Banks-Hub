namespace GPay.Banking.Helpers;

internal sealed record CallbackSourceCheckResult(bool IsAllowed, string Message)
{
    public static CallbackSourceCheckResult Allowed(string message) => new(true, message);

    public static CallbackSourceCheckResult Denied(string message) => new(false, message);
}
