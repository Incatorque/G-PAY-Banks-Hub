namespace GPay.Banking;

/// <summary>
/// Lookup for Absa CAPI error codes (Payment API v1.8 appendices B–C, callback CB*, AVS status codes).
/// </summary>
/// <remarks>
/// A static catalog is the right shape here. Absa codes are strings (<c>BR0007</c>, <c>MG0001</c>),
/// and <c>MG0001</c> is reused for many field rules, so the bank's own message is the specific text.
/// An enum cannot represent that without losing the code or inventing one member per sentence.
/// Category and severity are fixed labels on each known code. Unknown codes still return a result,
/// with <see cref="AbsaErrorInfo.Known"/> false and the bank message preserved.
/// </remarks>
public static class AbsaErrorCatalog
{
    private static readonly Dictionary<string, AbsaErrorInfo> Known = Build();

    /// <summary>
    /// Resolves a bank code to a catalog entry. The bank message wins when Absa sent one.
    /// </summary>
    public static AbsaErrorInfo Resolve(string? code, string? bankMessage = null)
    {
        var normalized = Normalize(code);
        if (normalized is null)
        {
            return new AbsaErrorInfo
            {
                Code = "UNKNOWN",
                Message = string.IsNullOrWhiteSpace(bankMessage) ? "Absa operation failed." : bankMessage.Trim(),
                Category = "Unknown",
                Severity = "Error",
                Known = false,
                BankMessage = bankMessage
            };
        }

        if (!Known.TryGetValue(normalized, out var definition))
        {
            return new AbsaErrorInfo
            {
                Code = normalized,
                Message = string.IsNullOrWhiteSpace(bankMessage) ? $"Uncatalogued Absa error {normalized}." : bankMessage.Trim(),
                Category = "Unknown",
                Severity = "Error",
                Known = false,
                BankMessage = bankMessage
            };
        }

        if (string.IsNullOrWhiteSpace(bankMessage))
        {
            return definition;
        }

        return new AbsaErrorInfo
        {
            Code = definition.Code,
            Message = bankMessage.Trim(),
            Category = definition.Category,
            Severity = definition.Severity,
            Known = true,
            BankMessage = bankMessage.Trim()
        };
    }

    private static string? Normalize(string? code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return null;
        }

        var value = code.Trim();
        if (value.StartsWith("ABSA_", StringComparison.OrdinalIgnoreCase))
        {
            value = value[5..];
        }

        return value.ToUpperInvariant();
    }

    private static Dictionary<string, AbsaErrorInfo> Build()
    {
        var items = new AbsaErrorInfo[]
        {
            Entry("AC0001", "We encountered an error authenticating the specified user.", "Authentication", "Error"),
            Entry("AC0002", "We encountered an error creating a new user session.", "Authentication", "Error"),
            Entry("MG0001", "The request failed Absa schema or field validation. Use the bank message for the field.", "Schema", "Error"),
            Entry("MG0002", "Absa returned a dynamic exception. The bank message identifies the field or session fault.", "Schema", "Error"),
            Entry("CB0001", "Callback Uri is mandatory.", "Callback", "Error"),
            Entry("CB0002", "X-Client-API-Key is mandatory.", "Callback", "Error"),
            Entry("CB0003", "Callback auth token is mandatory.", "Callback", "Error"),
            Entry("CB0004", "Callback support email address is mandatory.", "Callback", "Error"),
            Entry("CB0005", "Callback Uri exceeds 1024 characters.", "Callback", "Error"),
            Entry("CB0006", "Callback Uri is invalid.", "Callback", "Error"),
            Entry("CB0007", "Callback auth token exceeds 512 characters.", "Callback", "Error"),
            Entry("CB0008", "Callback support email address exceeds 1024 characters.", "Callback", "Error"),
            Entry("CB0009", "Callback support email address is invalid.", "Callback", "Error"),
            Entry("BR0001", "Transaction limit exceeded.", "BusinessRule", "Error"),
            Entry("BR0002", "Daily limit exceeded.", "BusinessRule", "Error"),
            Entry("BR0004", "Source and target account numbers must differ.", "BusinessRule", "Error"),
            Entry("BR0005", "Bank branch code 000000 is not allowed.", "BusinessRule", "Error"),
            Entry("BR0007", "Transaction has timed out.", "BusinessRule", "Error"),
            Entry("BR9999", "Payment rejected because it was submitted outside the allowed cut-off.", "BusinessRule", "Error"),
            Entry("ME0080", "The corporate account is not registered.", "BusinessRule", "Error"),
            Entry("400", "Request signature is missing.", "Http", "Error"),
            Entry("401", "Request signature is invalid.", "Http", "Error"),
            Entry("403", "Client certificate is expired.", "Http", "Error"),
            Entry("412", "Unable to retrieve the client certificate.", "Http", "Error"),
            Entry("500", "Absa internal server error.", "Http", "Fatal"),
            Entry("033", "Account verification technical error.", "AccountVerification", "Error"),
            Entry("098", "Duplicate account verification request.", "AccountVerification", "Error"),
            Entry("099", "Account verification timed out.", "AccountVerification", "Error"),
            Entry("001", "Account verification is pending.", "AccountVerification", "Warning")
        };

        return items.ToDictionary(x => x.Code, StringComparer.OrdinalIgnoreCase);
    }

    private static AbsaErrorInfo Entry(string code, string message, string category, string severity) =>
        new()
        {
            Code = code,
            Message = message,
            Category = category,
            Severity = severity,
            Known = true
        };
}

/// <summary>
/// Absa error returned on Banking API responses.
/// </summary>
public sealed class AbsaErrorInfo
{
    /// <summary>Absa rule or HTTP code, without the <c>ABSA_</c> prefix.</summary>
    public required string Code { get; init; }

    /// <summary>Catalog text, or the bank's own message when Absa sent one.</summary>
    public required string Message { get; init; }

    /// <summary>Authentication, Schema, BusinessRule, Callback, Http, AccountVerification, or Unknown.</summary>
    public required string Category { get; init; }

    /// <summary>Information, Warning, Error, or Fatal.</summary>
    public required string Severity { get; init; }

    /// <summary><c>true</c> when <see cref="Code"/> is in the catalog.</summary>
    public bool Known { get; init; }

    /// <summary>Original bank text when it differs from <see cref="Message"/> or was supplied with the code.</summary>
    public string? BankMessage { get; init; }
}
