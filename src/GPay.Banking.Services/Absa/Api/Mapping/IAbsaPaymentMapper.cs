using GPay.Banking.Services.Absa.Api.Models.Payment;
using GPay.Banking.Domain.Dtos;

namespace GPay.Banking.Services.Absa.Api.Mapping;

/// <summary>
/// Maps GPay instant payment DTOs to/from Absa CAPI payment payloads (MIG – Payment API v1.8).
/// </summary>
public interface IAbsaPaymentMapper
{
    /// <summary>
    /// Maps a GPay instant payment request to an Absa initiate request (Session filled by the client).
    /// </summary>
    AbsaPaymentInitiateRequest ToAbsaInitiateRequest(InstantPaymentRequest request, string correlationId);

    /// <summary>
    /// Maps an Absa initiate response to a GPay response.
    /// </summary>
    InstantPaymentResponse ToGpayInitiateResponse(
        AbsaPaymentInitiateResponse response,
        InstantPaymentRequest request);

    /// <summary>
    /// Maps a GPay status request to an Absa status request (Session filled by the client).
    /// </summary>
    AbsaPaymentStatusRequest ToAbsaStatusRequest(PaymentStatusRequest request);

    /// <summary>
    /// Maps an Absa status response to a GPay status response.
    /// </summary>
    PaymentStatusResponse ToGpayStatusResponse(
        AbsaPaymentStatusResponse response,
        PaymentStatusRequest request);

    /// <summary>
    /// Maps Absa Status code to a normalized label.
    /// </summary>
    string ToStatusLabel(int? statusCode, bool hasErrors);
}
