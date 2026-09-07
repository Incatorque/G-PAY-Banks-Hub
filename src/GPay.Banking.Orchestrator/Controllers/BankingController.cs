using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Dtos.AccountVerification;
using GPay.Banking.Contracts.Dtos.Balances;
using GPay.Banking.Contracts.Dtos.InstantPayment;
using GPay.Banking.Contracts.Dtos.Notifications;
using GPay.Banking.Contracts.Dtos.Statements;
using GPay.Banking.Contracts.Enums;
using GPay.Banking.Orchestrator.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPay.Banking.Orchestrator.Controllers;

/// <summary>
/// Bank capability endpoints used by GPay. All responses use <see cref="ApiResult{T}"/>.
/// </summary>
[ApiController]
[Route("api/{bank}")]
[Produces("application/json")]
[Authorize]
public sealed class BankingController : ControllerBase
{
    private readonly IBankRoutingService _routingService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BankingController"/> class.
    /// </summary>
    public BankingController(IBankRoutingService routingService)
    {
        _routingService = routingService;
    }

    /// <summary>
    /// Performs account verification (AVS) via the bank in the route.
    /// Prefer <c>POST /api/avs</c> with <c>Bank</c> in the body for a single GPay contract across banks.
    /// </summary>
    [HttpPost("account-verification")]
    [ProducesResponseType(typeof(ApiResult<AccountVerificationResponse>), StatusCodes.Status200OK)]
    public Task<ActionResult<ApiResult<AccountVerificationResponse>>> VerifyAccount(
        string bank,
        [FromBody] AccountVerificationRequest request,
        CancellationToken cancellationToken) =>
        SendAsync<AccountVerificationRequest, AccountVerificationResponse>(
            bank, BankOperation.AccountVerification, request, cancellationToken);

    /// <summary>
    /// Submits an instant payment (PayShap) via the specified bank.
    /// </summary>
    [HttpPost("payments/instant")]
    [ProducesResponseType(typeof(ApiResult<InstantPaymentResponse>), StatusCodes.Status200OK)]
    public Task<ActionResult<ApiResult<InstantPaymentResponse>>> InstantPayment(
        string bank,
        [FromBody] InstantPaymentRequest request,
        CancellationToken cancellationToken) =>
        SendAsync<InstantPaymentRequest, InstantPaymentResponse>(
            bank, BankOperation.InstantPayment, request, cancellationToken);

    /// <summary>
    /// Retrieves account balances via the specified bank.
    /// </summary>
    [HttpPost("balances")]
    [ProducesResponseType(typeof(ApiResult<BalanceResponse>), StatusCodes.Status200OK)]
    public Task<ActionResult<ApiResult<BalanceResponse>>> GetBalance(
        string bank,
        [FromBody] BalanceRequest request,
        CancellationToken cancellationToken) =>
        SendAsync<BalanceRequest, BalanceResponse>(
            bank, BankOperation.Balance, request, cancellationToken);

    /// <summary>
    /// Retrieves statement / transaction history via the specified bank.
    /// </summary>
    [HttpPost("statements")]
    [ProducesResponseType(typeof(ApiResult<StatementResponse>), StatusCodes.Status200OK)]
    public Task<ActionResult<ApiResult<StatementResponse>>> GetStatement(
        string bank,
        [FromBody] StatementRequest request,
        CancellationToken cancellationToken) =>
        SendAsync<StatementRequest, StatementResponse>(
            bank, BankOperation.Statement, request, cancellationToken);

    /// <summary>
    /// Processes notification subscribe / query via the specified bank.
    /// </summary>
    [HttpPost("notifications")]
    [ProducesResponseType(typeof(ApiResult<NotificationResponse>), StatusCodes.Status200OK)]
    public Task<ActionResult<ApiResult<NotificationResponse>>> Notifications(
        string bank,
        [FromBody] NotificationRequest request,
        CancellationToken cancellationToken) =>
        SendAsync<NotificationRequest, NotificationResponse>(
            bank, BankOperation.Notification, request, cancellationToken);

    private async Task<ActionResult<ApiResult<TResponse>>> SendAsync<TRequest, TResponse>(
        string bank,
        BankOperation operation,
        TRequest request,
        CancellationToken cancellationToken)
    {
        if (!TryParseBank(bank, out var bankCode))
        {
            return BadRequest(ApiResult<TResponse>.Fail(
                new ApiError { Code = "GPAY_INVALID_BANK", Message = $"Unknown bank '{bank}'." },
                GetCorrelationId()));
        }

        var correlationId = GetCorrelationId();
        var result = await _routingService.SendAsync<TRequest, TResponse>(
            bankCode,
            operation,
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

    private static bool TryParseBank(string bank, out BankCode bankCode)
    {
        return Enum.TryParse(bank, ignoreCase: true, out bankCode);
    }
}
