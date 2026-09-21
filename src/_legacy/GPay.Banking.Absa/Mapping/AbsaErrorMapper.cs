using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Interfaces;

namespace GPay.Banking.Absa.Mapping;

/// <summary>
/// Maps Absa CAPI error codes into GPay-normalized errors.
/// </summary>
public sealed class AbsaErrorMapper : IBankErrorMapper
{
    /// <inheritdoc />
    public ApiError Map(string? bankErrorCode, string? bankErrorMessage)
    {
        return new ApiError
        {
            Code = string.IsNullOrWhiteSpace(bankErrorCode) ? "ABSA_UNKNOWN" : $"ABSA_{bankErrorCode}",
            Message = string.IsNullOrWhiteSpace(bankErrorMessage) ? "Absa operation failed." : bankErrorMessage,
            BankCode = bankErrorCode,
            BankMessage = bankErrorMessage
        };
    }
}
