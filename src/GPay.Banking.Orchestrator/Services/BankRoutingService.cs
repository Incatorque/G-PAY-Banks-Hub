using System.Text.Json;
using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Enums;
using GPay.Banking.Contracts.Messaging;
using GPay.Banking.Infrastructure.Configuration;
using GPay.Banking.Infrastructure.Correlation;
using GPay.Banking.Infrastructure.Messaging;
using Microsoft.Extensions.Options;

namespace GPay.Banking.Orchestrator.Services;

/// <summary>
/// Routes GPay capability requests to the correct bank queue and awaits correlated responses.
/// </summary>
public interface IBankRoutingService
{
    /// <summary>
    /// Publishes a bank request and waits for the correlated <see cref="ApiResult{T}"/> payload.
    /// </summary>
    Task<ApiResult<TResponse>> SendAsync<TRequest, TResponse>(
        BankCode bankCode,
        BankOperation operation,
        TRequest request,
        string correlationId,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Default bank routing service using RabbitMQ and an in-memory correlator.
/// </summary>
public sealed class BankRoutingService : IBankRoutingService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(60);

    private readonly IMessageBus _messageBus;
    private readonly IResponseCorrelator _correlator;
    private readonly BankingOptions _bankingOptions;
    private readonly ILogger<BankRoutingService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BankRoutingService"/> class.
    /// </summary>
    public BankRoutingService(
        IMessageBus messageBus,
        IResponseCorrelator correlator,
        IOptions<BankingOptions> bankingOptions,
        ILogger<BankRoutingService> logger)
    {
        _messageBus = messageBus;
        _correlator = correlator;
        _bankingOptions = bankingOptions.Value;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<ApiResult<TResponse>> SendAsync<TRequest, TResponse>(
        BankCode bankCode,
        BankOperation operation,
        TRequest request,
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        var registration = _bankingOptions.Banks.FirstOrDefault(b => b.BankCode == bankCode)
            ?? throw new InvalidOperationException($"Bank '{bankCode}' is not registered.");

        var tcs = _correlator.Register(correlationId, DefaultTimeout);

        var message = new BankRequestMessage
        {
            CorrelationId = correlationId,
            BankCode = bankCode,
            Operation = operation,
            Payload = JsonSerializer.Serialize(request, JsonOptions)
        };

        _logger.LogInformation(
            "Routing {Operation} to {Bank} CorrelationId={CorrelationId}",
            operation,
            bankCode,
            correlationId);

        await _messageBus.PublishAsync(registration.RequestQueue, message, cancellationToken);

        try
        {
            var payload = await tcs.Task.WaitAsync(cancellationToken);
            var result = JsonSerializer.Deserialize<ApiResult<TResponse>>(payload, JsonOptions);
            return result ?? ApiResult<TResponse>.Fail(
                new ApiError { Code = "GPAY_DESERIALIZE", Message = "Invalid bank response payload." },
                correlationId);
        }
        catch (TimeoutException)
        {
            _correlator.Cancel(correlationId);
            return ApiResult<TResponse>.Fail(
                new ApiError { Code = "GPAY_TIMEOUT", Message = "Timed out waiting for bank response." },
                correlationId);
        }
    }
}
