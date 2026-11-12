using GPay.Banking.Domain;

namespace GPay.Banking;

/// <summary>Resolves native errors into a shared bank-neutral API shape.</summary>
internal static class BankErrorResponses
{
    public static BankErrorInfo? From(ApiError? error)
    {
        if (error is null) return null;
        var bank = ReadBank(error);
        return FromCode(error.BankCode ?? error.Code, error.BankMessage ?? error.Message, bank);
    }

    public static BankErrorInfo? FromCode(string? code, string? message, string? bank = null)
    {
        if (string.IsNullOrWhiteSpace(code)) return null;
        bank = ResolveBank(bank, code);
        return bank switch
        {
            "ABSA" => Convert("ABSA", AbsaErrorCatalog.Resolve(code, message)),
            "CAPITEC" => Convert("CAPITEC", CapitecErrorCatalog.Resolve(code, message)),
            _ => new BankErrorInfo
            {
                Bank = bank,
                Code = code.Trim(),
                Message = string.IsNullOrWhiteSpace(message) ? $"{bank} returned error {code.Trim()}." : message.Trim(),
                Category = "Unknown",
                Severity = "Error",
                BankMessage = message
            }
        };
    }

    public static AbsaErrorInfo? FromAbsa(ApiError? error) =>
        error is not null && ResolveBank(ReadBank(error), error.Code) == "ABSA"
            ? AbsaErrorCatalog.Resolve(error.BankCode ?? error.Code, error.BankMessage ?? error.Message)
            : null;

    public static AbsaErrorInfo? FromAbsaCode(string? code, string? message, string? bank = null) =>
        ResolveBank(bank, code) == "ABSA" && !string.IsNullOrWhiteSpace(code)
            ? AbsaErrorCatalog.Resolve(code, message)
            : null;

    private static string ReadBank(ApiError error)
    {
        if (error.Details?.TryGetValue("bank", out var values) == true && values.Length > 0)
            return values[0];
        return ResolveBank(null, error.Code);
    }

    private static string ResolveBank(string? bank, string? code)
    {
        var value = bank?.Trim().ToUpperInvariant();
        if (value is "ABSA" or "CAPITEC") return value;
        var normalizedCode = code?.Trim().ToUpperInvariant() ?? string.Empty;
        if (normalizedCode.StartsWith("ABSA_", StringComparison.Ordinal)) return "ABSA";
        if (normalizedCode.StartsWith("CAPITEC_", StringComparison.Ordinal)) return "CAPITEC";
        return string.IsNullOrWhiteSpace(value) ? "BANK" : value;
    }

    private static BankErrorInfo Convert(string bank, CapitecErrorInfo error) => new()
    {
        Bank = bank,
        Code = error.Code,
        Message = error.Message,
        Category = error.Category,
        Severity = error.Severity,
        Known = error.Known,
        BankMessage = error.BankMessage
    };

    private static BankErrorInfo Convert(string bank, AbsaErrorInfo error) => new()
    {
        Bank = bank,
        Code = error.Code,
        Message = error.Message,
        Category = error.Category,
        Severity = error.Severity,
        Known = error.Known,
        BankMessage = error.BankMessage
    };
}
