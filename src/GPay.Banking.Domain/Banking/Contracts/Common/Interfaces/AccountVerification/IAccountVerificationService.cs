using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;

using GPay.Banking;

namespace GPay.Banking.Domain;

/// <summary>
/// Account verification (AVS) capability implemented by each bank adapter.
/// </summary>
public interface IAccountVerificationService
{
    /// <summary>
    /// Verifies account holder / account details with the bank.
    /// </summary>
    /// <param name="request">GPay AVS request.</param>
    /// <param name="correlationId">Correlation identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Unified API result containing verification outcome.</returns>
    Task<ApiResult<AccountVerificationResponse>> VerifyAsync(
        AccountVerificationRequest request,
        string correlationId,
        CancellationToken cancellationToken = default);
}




