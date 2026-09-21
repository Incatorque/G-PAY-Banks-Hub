using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Dtos.AccountVerification;
using GPay.Banking.Contracts.Enums;
using GPay.Banking.Orchestrator.Services;
using Microsoft.AspNetCore.Mvc;

namespace GPay.Banking.Orchestrator.Controllers;

/// <summary>
/// Async mixed-bank AVS batch API.
/// </summary>
[ApiController]
[Route("api/avs")]
[Produces("application/json")]
public sealed class AvsBatchController : ControllerBase
{
    private readonly IAvsBatchService _batchService;

    public AvsBatchController(IAvsBatchService batchService)
    {
        _batchService = batchService;
    }

    /// <summary>
    /// Accepts up to 20 000 AVS items, splits into ≤5 000 bank-homogeneous segments, and queues them.
    /// </summary>
    [HttpPost("batch")]
    [ProducesResponseType(typeof(ApiResult<AvsBatchSubmitResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResult<AvsBatchSubmitResponse>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResult<AvsBatchSubmitResponse>>> Submit(
        [FromBody] AvsBatchSubmitRequest request,
        CancellationToken cancellationToken)
    {
        var correlationId = GetCorrelationId();
        try
        {
            var response = await _batchService.SubmitAsync(request, correlationId, cancellationToken);
            return Accepted(ApiResult<AvsBatchSubmitResponse>.Ok(response, correlationId));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResult<AvsBatchSubmitResponse>.Fail(
                new ApiError { Code = "GPAY_AVS_BATCH_INVALID", Message = ex.Message },
                correlationId));
        }
    }

    /// <summary>
    /// Returns batch progress including segment and per-bank breakdown.
    /// </summary>
    [HttpGet("batches/{batchId:guid}")]
    [ProducesResponseType(typeof(ApiResult<AvsBatchProgressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult<AvsBatchProgressDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResult<AvsBatchProgressDto>>> GetProgress(
        Guid batchId,
        CancellationToken cancellationToken)
    {
        var correlationId = GetCorrelationId();
        var progress = await _batchService.GetProgressAsync(batchId, cancellationToken);
        if (progress is null)
        {
            return NotFound(ApiResult<AvsBatchProgressDto>.Fail(
                new ApiError { Code = "GPAY_AVS_BATCH_NOT_FOUND", Message = "Batch was not found." },
                correlationId));
        }

        return Ok(ApiResult<AvsBatchProgressDto>.Ok(progress, correlationId));
    }

    /// <summary>
    /// Returns paged batch item results (for ABP sync / drill-down).
    /// </summary>
    [HttpGet("batches/{batchId:guid}/items")]
    [ProducesResponseType(typeof(ApiResult<AvsBatchItemPageDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResult<AvsBatchItemPageDto>>> GetItems(
        Guid batchId,
        [FromQuery] AvsBatchItemStatus? status,
        [FromQuery] BankCode? bankCode,
        [FromQuery] bool? processedOnly,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 100,
        CancellationToken cancellationToken = default)
    {
        var correlationId = GetCorrelationId();
        var page = await _batchService.GetItemsAsync(
            batchId,
            new AvsBatchItemQuery
            {
                Status = status,
                BankCode = bankCode,
                ProcessedOnly = processedOnly,
                Skip = skip,
                Take = take
            },
            cancellationToken);

        return Ok(ApiResult<AvsBatchItemPageDto>.Ok(page, correlationId));
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
