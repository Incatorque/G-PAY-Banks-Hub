using GPay.Banking.Domain;

namespace GPay.Banking.Services.Absa.Api.Mapping;

/// <summary>
/// Maps Absa CAPI error codes through <see cref="AbsaErrorCatalog"/>.
/// </summary>
public sealed class AbsaErrorMapper : IBankErrorMapper
{
    /// <inheritdoc />
    public ApiError Map(string? bankErrorCode, string? bankErrorMessage)
    {
        var info = AbsaErrorCatalog.Resolve(bankErrorCode, bankErrorMessage);
        var code = string.IsNullOrWhiteSpace(info.Code) || info.Code == "UNKNOWN"
            ? "ABSA_UNKNOWN"
            : $"ABSA_{info.Code}";

        return new ApiError
        {
            Code = code,
            Message = info.Message,
            BankCode = info.Code == "UNKNOWN" ? bankErrorCode : info.Code,
            BankMessage = bankErrorMessage,
            Details = new Dictionary<string, string[]>
            {
                ["category"] = [info.Category],
                ["severity"] = [info.Severity],
                ["known"] = [info.Known ? "true" : "false"]
            }
        };
    }
}
