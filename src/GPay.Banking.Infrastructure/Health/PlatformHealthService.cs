using GPay.Banking.Contracts.Enums;
using GPay.Banking.Contracts.Health;
using GPay.Banking.Contracts.Messaging;
using GPay.Banking.Infrastructure.Configuration;
using GPay.Banking.Infrastructure.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GPay.Banking.Infrastructure.Health;

/// <summary>
/// Builds queue depth and aggregated platform health snapshots.
/// </summary>
public interface IPlatformHealthService
{
    /// <summary>
    /// Returns depths for all known platform queues.
    /// </summary>
    Task<IReadOnlyList<QueueDepthInfo>> GetQueueDepthsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns aggregated health for orchestrator, MQ, and each registered bank.
    /// </summary>
    Task<PlatformHealthResponse> GetPlatformHealthAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Default platform health and queue monitoring service.
/// </summary>
public sealed class PlatformHealthService : IPlatformHealthService
{
    private readonly IMessageBus _messageBus;
    private readonly RabbitMqOptions _rabbitOptions;
    private readonly BankingOptions _bankingOptions;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<PlatformHealthService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformHealthService"/> class.
    /// </summary>
    public PlatformHealthService(
        IMessageBus messageBus,
        IOptions<RabbitMqOptions> rabbitOptions,
        IOptions<BankingOptions> bankingOptions,
        IHttpClientFactory httpClientFactory,
        ILogger<PlatformHealthService> logger)
    {
        _messageBus = messageBus;
        _rabbitOptions = rabbitOptions.Value;
        _bankingOptions = bankingOptions.Value;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<QueueDepthInfo>> GetQueueDepthsAsync(CancellationToken cancellationToken = default)
    {
        var queues = BuildKnownQueues();
        var results = new List<QueueDepthInfo>();

        foreach (var (queueName, bank) in queues)
        {
            var (messages, consumers) = await _messageBus.GetQueueStatsAsync(queueName, cancellationToken);
            results.Add(new QueueDepthInfo
            {
                QueueName = queueName,
                MessageCount = messages,
                ConsumerCount = (int)consumers,
                Bank = bank
            });
        }

        return results;
    }

    /// <inheritdoc />
    public async Task<PlatformHealthResponse> GetPlatformHealthAsync(CancellationToken cancellationToken = default)
    {
        var services = new List<ServiceHealthInfo>
        {
            await BuildOrchestratorHealthAsync(cancellationToken)
        };

        foreach (var bank in _bankingOptions.Banks)
        {
            services.Add(await BuildBankHealthAsync(bank, cancellationToken));
        }

        var overall = services.Max(s => s.Status);
        return new PlatformHealthResponse
        {
            OverallStatus = overall,
            Services = services,
            CheckedAtUtc = DateTimeOffset.UtcNow
        };
    }

    private async Task<ServiceHealthInfo> BuildOrchestratorHealthAsync(CancellationToken cancellationToken)
    {
        var mqConnected = _messageBus.IsConnected;
        if (!mqConnected)
        {
            try
            {
                await _messageBus.DeclareQueueAsync(QueueNames.OrchestratorRequests, cancellationToken);
                mqConnected = _messageBus.IsConnected;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "RabbitMQ connectivity check failed.");
            }
        }

        var requestQueue = await BuildQueueHealthAsync(QueueNames.OrchestratorRequests, cancellationToken);
        var queues = new[] { requestQueue };

        var status = !mqConnected
            ? ServiceHealthStatus.Critical
            : queues.Any(q => q.IsClogged)
                ? ServiceHealthStatus.Unhealthy
                : ServiceHealthStatus.Healthy;

        return new ServiceHealthInfo
        {
            ServiceName = "Orchestrator",
            Status = status,
            IsResponding = true,
            Detail = mqConnected ? "RabbitMQ reachable." : "RabbitMQ not responding.",
            Queues = queues
        };
    }

    private async Task<ServiceHealthInfo> BuildBankHealthAsync(BankRegistration bank, CancellationToken cancellationToken)
    {
        var queueHealth = await BuildQueueHealthAsync(bank.RequestQueue, cancellationToken);
        var isResponding = true;
        var detail = "OK";

        if (!string.IsNullOrWhiteSpace(bank.HealthUrl))
        {
            try
            {
                var client = _httpClientFactory.CreateClient("BankHealth");
                using var response = await client.GetAsync(bank.HealthUrl, cancellationToken);
                isResponding = response.IsSuccessStatusCode;
                detail = isResponding ? "Health probe succeeded." : $"Health probe returned {(int)response.StatusCode}.";
            }
            catch (Exception ex)
            {
                isResponding = false;
                detail = $"Health probe failed: {ex.Message}";
                _logger.LogWarning(ex, "Bank health probe failed for {Bank}", bank.Name);
            }
        }
        else if (queueHealth.ConsumerCount == 0)
        {
            isResponding = false;
            detail = "No consumers on bank request queue.";
        }

        var status = !isResponding
            ? ServiceHealthStatus.Critical
            : queueHealth.IsClogged
                ? ServiceHealthStatus.Unhealthy
                : ServiceHealthStatus.Healthy;

        return new ServiceHealthInfo
        {
            ServiceName = bank.Name,
            BankCode = bank.BankCode,
            Status = status,
            IsResponding = isResponding,
            Detail = detail,
            Queues = [queueHealth]
        };
    }

    private async Task<QueueHealthInfo> BuildQueueHealthAsync(string queueName, CancellationToken cancellationToken)
    {
        var (messages, consumers) = await _messageBus.GetQueueStatsAsync(queueName, cancellationToken);
        return new QueueHealthInfo
        {
            QueueName = queueName,
            MessageCount = messages,
            ConsumerCount = (int)consumers,
            IsClogged = messages >= _rabbitOptions.ClogThreshold
        };
    }

    private List<(string QueueName, string? Bank)> BuildKnownQueues()
    {
        var list = new List<(string, string?)>
        {
            (QueueNames.OrchestratorRequests, null)
        };

        foreach (var bank in _bankingOptions.Banks)
        {
            list.Add((bank.RequestQueue, bank.Name));
        }

        return list;
    }
}
