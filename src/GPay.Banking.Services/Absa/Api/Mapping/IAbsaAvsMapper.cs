using GPay.Banking.Services.Absa.Api.Models.Avs;
using GPay.Banking.Domain.Dtos;

namespace GPay.Banking.Services.Absa.Api.Mapping;

/// <summary>
/// Maps GPay AVS DTOs to/from Absa CAPI AVS payloads (MIG – AVS API v00.5).
/// </summary>
public interface IAbsaAvsMapper
{
    /// <summary>
    /// Maps a GPay request to an Absa ValidateBankDetails request (Session filled by the client).
    /// </summary>
    AbsaAvsRequest ToAbsaRequest(AccountVerificationRequest request, string correlationId);

    /// <summary>
    /// Maps an Absa AVS response to a GPay response.
    /// </summary>
    AccountVerificationResponse ToGpayResponse(AbsaAvsResponse response, string? reference);
}
