using GPay.Banking.Contracts.Health;
using GPay.Banking.Infrastructure.Health;
using Microsoft.AspNetCore.Mvc;

namespace GPay.Banking.Orchestrator.Controllers;

/// <summary>
/// Platform health and queue monitoring endpoints for GPay and the Angular ops UI.
/// </summary>
[ApiController]
[Route("")]
[Produces("application/json")]
public sealed class HealthController : ControllerBase
{
    private readonly IPlatformHealthService _healthService;

    /// <summary>
    /// Initializes a new instance of the <see cref="HealthController"/> class.
    /// </summary>
    public HealthController(IPlatformHealthService healthService)
    {
        _healthService = healthService;
    }

    /// <summary>
    /// Returns aggregated health for orchestrator, RabbitMQ, and each bank service.
    /// Clogged queues are flagged Unhealthy; non-responding services are Critical.
    /// </summary>
    [HttpGet("health")]
    [ProducesResponseType(typeof(PlatformHealthResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<PlatformHealthResponse>> GetHealth(CancellationToken cancellationToken)
    {
        var health = await _healthService.GetPlatformHealthAsync(cancellationToken);
        return Ok(health);
    }

    /// <summary>
    /// Returns the number of ready messages in each known RabbitMQ queue.
    /// </summary>
    [HttpGet("api/queues")]
    [ProducesResponseType(typeof(IReadOnlyList<QueueDepthInfo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<QueueDepthInfo>>> GetQueues(CancellationToken cancellationToken)
    {
        var queues = await _healthService.GetQueueDepthsAsync(cancellationToken);
        return Ok(queues);
    }
}
