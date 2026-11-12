namespace GPay.Banking;

/// <summary>Catalog of Capitec Payments Bulk Payments API v2.5.3 error codes.</summary>
public static class CapitecErrorCatalog
{
    private static readonly Dictionary<string, CapitecErrorInfo> Known = Build();

    public static CapitecErrorInfo Resolve(string? code, string? bankMessage = null)
    {
        var normalized = Normalize(code);
        if (normalized is null || !Known.TryGetValue(normalized, out var definition))
        {
            var unknownCode = normalized ?? "UNKNOWN";
            return new CapitecErrorInfo
            {
                Code = unknownCode,
                Message = string.IsNullOrWhiteSpace(bankMessage) ? $"Uncatalogued Capitec error {unknownCode}." : bankMessage.Trim(),
                Category = "Unknown",
                Severity = "Error",
                Known = false,
                BankMessage = bankMessage
            };
        }

        return string.IsNullOrWhiteSpace(bankMessage) ? definition : new CapitecErrorInfo
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
        if (string.IsNullOrWhiteSpace(code)) return null;
        var value = code.Trim();
        if (value.StartsWith("CAPITEC_", StringComparison.OrdinalIgnoreCase)) value = value[8..];
        return value.ToUpperInvariant();
    }

    private static Dictionary<string, CapitecErrorInfo> Build()
    {
        var messages = new Dictionary<string, string>
        {
            ["BP001"] = "The MessageId of the request is a duplicate.",
            ["BP002"] = "The UETR of the instruction is a duplicate.",
            ["BP003"] = "The Initiating Party ID is invalid.",
            ["BP004"] = "The Instruction UETR is invalid.",
            ["BP005"] = "The Group Header is invalid.",
            ["BP006"] = "The instructed date is invalid.",
            ["BP007"] = "The transaction count is invalid or does not match the request.",
            ["BP008"] = "The control sum does not match the transaction amounts.",
            ["BP009"] = "The debtor details are incorrect.",
            ["BP010"] = "The debtor account is invalid or is not authorised.",
            ["BP011"] = "A downstream system timed out.",
            ["BP012"] = "A technical failure occurred in the order management layer.",
            ["BP013"] = "The instruction is not in the correct state.",
            ["BP014"] = "The request could not be transformed for downstream processing.",
            ["BP015"] = "The creditor details are incorrect.",
            ["BP016"] = "The creditor account is invalid or is not authorised.",
            ["BP017"] = "The instructed amount exceeds the limit for this payment type.",
            ["BP018"] = "Proxy payments above the PayShap limit are not allowed.",
            ["BP019"] = "The creditor bank does not participate in immediate payment schemes.",
            ["BP020"] = "The creditor bank does not participate in RTC.",
            ["BP021"] = "The creditor bank does not participate in PayShap.",
            ["BP022"] = "The payment exceeds the industry limit.",
            ["BP023"] = "Proxy payments are not allowed.",
            ["BP024"] = "The account is blocked.",
            ["BP025"] = "The account is inactive.",
            ["BP026"] = "The account was not found.",
            ["BP027"] = "The amount cannot be zero.",
            ["BP028"] = "The amount is invalid.",
            ["BP029"] = "The account has insufficient funds.",
            ["BP030"] = "The method of payment could not be derived.",
            ["BP031"] = "Downstream transaction validation failed.",
            ["BP032"] = "The account holder is deceased.",
            ["BP033"] = "Credits are not allowed to this account.",
            ["BP034"] = "The account does not meet FICA requirements.",
            ["BP035"] = "Debits are not allowed to this account.",
            ["BP036"] = "A technical failure occurred in the payment processing layer.",
            ["BP037"] = "The cellphone number is unverified or did not match.",
            ["BP038"] = "The account type is invalid.",
            ["BP039"] = "The instructed date format is incorrect.",
            ["BP040"] = "The method of payment is invalid.",
            ["BP041"] = "The creation date-time format is incorrect.",
            ["BP042"] = "The creditor BICFI is invalid.",
            ["BP043"] = "The request could not be parsed.",
            ["BP044"] = "Instruction validation failed.",
            ["BP045"] = "The payment response could not be parsed.",
            ["BP046"] = "The payment response could not be matched to a request.",
            ["BP047"] = "A duplicate payment response was received.",
            ["BP048"] = "The callback endpoint could not be reached.",
            ["BP049"] = "The credit transaction is a duplicate.",
            ["BP050"] = "The Initiating Party ID is not active.",
            ["BP051"] = "The transaction exceeds the item limit.",
            ["BP052"] = "The authorised debit account is inactive.",
            ["BP053"] = "The payment operator or beneficiary bank returned an error.",
            ["BP054"] = "The account is locked.",
            ["BP055"] = "The account is closed.",
            ["BP056"] = "The account is in liquidation.",
            ["BP057"] = "The minimum account balance of R150 has been reached.",
            ["400"] = "Bad request. The request must not be retried until corrected.",
            ["401"] = "Unauthorized request. Retry is allowed after authorization is corrected.",
            ["403"] = "Forbidden request. Retry is allowed after access is corrected.",
            ["404"] = "Endpoint not found. Retry is allowed after the endpoint is corrected.",
            ["408"] = "Request timed out. Retry is allowed.",
            ["409"] = "Duplicate request. Do not retry with the same payload.",
            ["429"] = "Too many requests. Do not retry immediately.",
            ["500"] = "Capitec returned an internal server error. Retry is not allowed by the guide.",
            ["502"] = "Capitec returned a bad gateway response. Retry is not allowed by the guide.",
            ["503"] = "Capitec is unavailable. Retry is allowed.",
            ["504"] = "Capitec gateway timed out. Retry is allowed.",
            ["511"] = "Network authentication is required. Retry is allowed after network access is corrected."
        };
        return messages.Select(pair => new CapitecErrorInfo
            {
                Code = pair.Key,
                Message = pair.Value,
                Category = pair.Key.StartsWith("BP", StringComparison.Ordinal) ? "BusinessRule" : "Http",
                Severity = "Error",
                Known = true
            })
            .ToDictionary(item => item.Code, StringComparer.OrdinalIgnoreCase);
    }
}

/// <summary>Normalized Capitec error detail.</summary>
public sealed class CapitecErrorInfo
{
    public required string Code { get; init; }
    public required string Message { get; init; }
    public required string Category { get; init; }
    public required string Severity { get; init; }
    public bool Known { get; init; }
    public string? BankMessage { get; init; }
}
