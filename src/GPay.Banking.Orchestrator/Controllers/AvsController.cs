using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Dtos.AccountVerification;
using GPay.Banking.Contracts.Enums;
using GPay.Banking.Orchestrator.Services;
using Microsoft.AspNetCore.Mvc;

namespace GPay.Banking.Orchestrator.Controllers;

/// <summary>
/// Bank-agnostic AVS entry point for GPay.
/// Same request shape for every bank; <see cref="AccountVerificationRequest.Bank"/> selects the route.
/// </summary>
[ApiController]
[Route("api")]
[Produces("application/json")]
public sealed class AvsController : ControllerBase
{
    private readonly IBankRoutingService _routingService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AvsController"/> class.
    /// </summary>
    public AvsController(IBankRoutingService routingService)
    {
        _routingService = routingService;
    }

    /// <summary>
    /// Performs account verification via the bank specified in the request body.
    /// Preferred GPay entry point: one contract for all banks.
    /// </summary>
    /// <remarks>
    /// Equivalent bank-specific route: <c>POST /api/{bank}/account-verification</c>.
    /// </remarks>
    [HttpPost("avs")]
    [ProducesResponseType(typeof(ApiResult<AccountVerificationResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<AccountVerificationResponse>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResult<AccountVerificationResponse>>> Verify(
        [FromBody] AccountVerificationRequest request,
        CancellationToken cancellationToken)
    {
        var correlationId = GetCorrelationId();

        if (request.Bank is null)
        {
            return BadRequest(ApiResult<AccountVerificationResponse>.Fail(
                new ApiError
                {
                    Code = "GPAY_BANK_REQUIRED",
                    Message = "Bank is required on the AVS request so the orchestrator can route to the correct bank service."
                },
                correlationId));
        }

        var result = await _routingService.SendAsync<AccountVerificationRequest, AccountVerificationResponse>(
            request.Bank.Value,
            BankOperation.AccountVerification,
            request,
            correlationId,
            cancellationToken);

        return Ok(result);
    }

    private string GetCorrelationId()
    {
        if (Request.Headers.TryGetValue("X-Correlation-Id", out var header) &&
            !string.IsNullOrWhiteSpace(header))
        {
            return header.ToString();
        }

        return HttpContext.TraceIdentifier;
    }
}
