using GPay.Banking.Services.Capitec.Api.Models.Payment;
using GPay.Banking.Domain.Dtos;

namespace GPay.Banking.Services.Capitec.Api.Mapping;

/// <summary>
/// Maps GPay instant payment DTOs to/from Capitec API payment payloads (Bulk Payments API Version 2.5.3).
/// </summary>
public interface ICapitecPaymentMapper
{
    /// <summary>
    /// Maps a GPay instant payment request to the CPS payout JSON structure.
    /// </summary>
    CapitecPaymentInitiateRequest ToCapitecInitiateRequest(InstantPaymentRequest request, string correlationId);

    /// <summary>
    /// Maps a Capitec initiate response to a GPay response.
    /// </summary>
    InstantPaymentResponse ToGpayInitiateResponse(
        CapitecPaymentInitiateResponse response,
        InstantPaymentRequest request);

    /// <summary>
    /// Maps a GPay status request to the CPS status enquiry JSON structure.
    /// </summary>
    CapitecPaymentStatusRequest ToCapitecStatusRequest(PaymentStatusRequest request);

    /// <summary>
    /// Maps a Capitec status response to a GPay status response.
    /// </summary>
    PaymentStatusResponse ToGpayStatusResponse(
        CapitecPaymentStatusResponse response,
        PaymentStatusRequest request);

    /// <summary>
    /// Maps the CPS status label or code to a normalized Bank Hub status.
    /// </summary>
    string ToStatusLabel(string? status);
}
