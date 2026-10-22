using GPay.Banking.Domain;

using GPay.Banking;

namespace GPay.Banking.Domain;

/// <summary>
/// Maps bank-native error codes/messages into GPay-normalized <see cref="ApiError"/> values.
/// </summary>
public interface IBankErrorMapper
{
    /// <summary>
    /// Maps a bank-specific error into a GPay <see cref="ApiError"/>.
    /// </summary>
    /// <param name="bankErrorCode">Bank-native error code.</param>
    /// <param name="bankErrorMessage">Bank-native error message.</param>
    /// <returns>Normalized API error.</returns>
    ApiError Map(string? bankErrorCode, string? bankErrorMessage);
}




