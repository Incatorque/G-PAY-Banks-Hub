using GPay.Banking.Domain;

namespace GPay.Banking.Services.Capitec.Api.Mapping;

/// <summary>Maps Capitec-native errors to the shared API error shape.</summary>
public sealed class CapitecErrorMapper
{
    public ApiError Map(string? bankErrorCode, string? bankErrorMessage)
    {
        var info = CapitecErrorCatalog.Resolve(bankErrorCode, bankErrorMessage);
        var code = string.Equals(info.Code, "UNKNOWN", StringComparison.OrdinalIgnoreCase)
            ? "CAPITEC_UNKNOWN"
            : $"CAPITEC_{info.Code}";

        return new ApiError
        {
            Code = code,
            Message = info.Message,
            BankCode = info.Code == "UNKNOWN" ? bankErrorCode : info.Code,
            BankMessage = bankErrorMessage,
            Details = new Dictionary<string, string[]>
            {
                ["bank"] = ["Capitec"],
                ["category"] = [info.Category],
                ["severity"] = [info.Severity],
                ["known"] = [info.Known ? "true" : "false"]
            }
        };
    }
}
