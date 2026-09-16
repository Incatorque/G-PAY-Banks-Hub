using System.Collections.Concurrent;
using System.Text.Json;
using GPay.Banking.Contracts.Dtos.InstantPayment;
using GPay.Banking.Orchestrator.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GPay.Banking.Orchestrator.Controllers;

/// <summary>
/// Inbound bank callback endpoints (no JWT — verified via shared Token).
/// </summary>
[ApiController]
[Route("api/callbacks")]
[Produces("application/json")]
[AllowAnonymous]
public sealed class CallbacksController : ControllerBase
{
    private static readonly ConcurrentDictionary<string, byte> SeenCallbacks = new(StringComparer.Ordinal);
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly AbsaCallbackOptions _options;
    private readonly ILogger<CallbacksController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CallbacksController"/> class.
    /// </summary>
    public CallbacksController(
        IOptions<AbsaCallbackOptions> options,
        ILogger<CallbacksController> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    /// <summary>
    /// Absa payment status callback (PayShap / RTC / PAAF).
    /// </summary>
    [HttpPost("absa/payment")]
    [ProducesResponseType(typeof(AbsaCallbackAck), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AbsaCallbackAck), StatusCodes.Status401Unauthorized)]
    public ActionResult<AbsaCallbackAck> AbsaPaymentCallback([FromBody] AbsaPaymentCallbackDto payload)
    {
        if (string.IsNullOrWhiteSpace(_options.PaymentToken) ||
            !string.Equals(payload.Token, _options.PaymentToken, StringComparison.Ordinal))
        {
            _logger.LogWarning("Absa payment callback rejected: invalid Token");
            return Unauthorized(new AbsaCallbackAck { Success = false, Message = "Invalid token." });
        }

        var apiRef = payload.Correlations?.FirstOrDefault(c => c.Type == 3)?.Value;
        var txRef = payload.Correlations?.FirstOrDefault(c => c.Type == 4)?.Value;
        var idempotencyKey = $"{txRef ?? apiRef ?? "unknown"}|{payload.Type}|{payload.PaymentStatus}";

        if (!SeenCallbacks.TryAdd(idempotencyKey, 0))
        {
            _logger.LogInformation(
                "Absa payment callback duplicate ignored Key={Key} Type={Type} Status={Status}",
                idempotencyKey,
                payload.Type,
                payload.PaymentStatus);
            return Ok(new AbsaCallbackAck { Success = true, Message = string.Empty });
        }

        _logger.LogInformation(
            "Absa payment callback received Type={Type} PaymentStatus={Status} ApiRef={ApiRef} TxRef={TxRef} Payload={Payload}",
            payload.Type,
            payload.PaymentStatus,
            apiRef,
            txRef,
            JsonSerializer.Serialize(payload, JsonOptions));

        return Ok(new AbsaCallbackAck { Success = true, Message = string.Empty });
    }
}

/// <summary>
/// Minimal Absa callback acknowledgement.
/// </summary>
public sealed class AbsaCallbackAck
{
    /// <summary>
    /// Indicates the callback was accepted.
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// Optional message (empty on success per Absa expectation).
    /// </summary>
    public string Message { get; init; } = string.Empty;
}
