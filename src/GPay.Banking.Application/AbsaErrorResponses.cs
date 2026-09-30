namespace GPay.Banking;

/// <summary>
/// Attaches <see cref="AbsaErrorCatalog"/> entries to API responses.
/// </summary>
internal static class AbsaErrorResponses
{
    public static AbsaErrorInfo? From(ApiError? error)
    {
        if (error is null)
        {
            return null;
        }

        return AbsaErrorCatalog.Resolve(error.BankCode ?? error.Code, error.BankMessage ?? error.Message);
    }

    public static AbsaErrorInfo? FromCode(string? code, string? bankMessage)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return null;
        }

        return AbsaErrorCatalog.Resolve(code, bankMessage);
    }
}
