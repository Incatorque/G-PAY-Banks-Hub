using System.Text;
using System.Text.Json;
using GPay.Banking.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GPay.Banking.Infrastructure.Messaging;

/// <summary>
/// RabbitMQ implementation of <see cref="IMessageBus"/>.
/// </summary>
public sealed class RabbitMqMessageBus : IMessageBus, IAsyncDisposable
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly RabbitMqOptions _options;
    private readonly ILogger<RabbitMqMessageBus> _logger;
    private readonly ConnectionFactory _factory;
    private IConnection? _connection;
    private readonly SemaphoreSlim _connectionLock = new(1, 1);
    private readonly List<IChannel> _consumerChannels = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMqMessageBus"/> class.
    /// </summary>
    public RabbitMqMessageBus(IOptions<RabbitMqOptions> options, ILogger<RabbitMqMessageBus> logger)
    {
        _options = options.Value;
        _logger = logger;
        _factory = new ConnectionFactory
        {
            HostName = _options.HostName,
            Port = _options.Port,
            VirtualHost = _options.VirtualHost,
            UserName = _options.UserName,
            Password = _options.Password
        };
    }

    /// <inheritdoc />
    public bool IsConnected => _connection is { IsOpen: true };

    /// <inheritdoc />
    public async Task DeclareQueueAsync(string queueName, CancellationToken cancellationToken = default)
    {
        await using var channel = await CreateChannelAsync(cancellationToken);
        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public async Task PublishAsync<T>(string queueName, T message, CancellationToken cancellationToken = default)
    {
        await using var channel = await CreateChannelAsync(cancellationToken);
        await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: cancellationToken);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message, JsonOptions));
        var props = new BasicProperties
        {
            ContentType = "application/json",
            DeliveryMode = DeliveryModes.Persistent,
            MessageId = Guid.NewGuid().ToString("N")
        };

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: queueName,
            mandatory: false,
            basicProperties: props,
            body: body,
            cancellationToken: cancellationToken);

        _logger.LogInformation("Published message to queue {QueueName}", queueName);
    }

    /// <inheritdoc />
    public async Task SubscribeAsync<T>(string queueName, Func<T, CancellationToken, Task> handler, CancellationToken cancellationToken = default)
    {
        var channel = await CreateChannelAsync(cancellationToken);
        _consumerChannels.Add(channel);

        await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: cancellationToken);
        await channel.BasicQosAsync(0, 1, false, cancellationToken);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (_, args) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                var message = JsonSerializer.Deserialize<T>(json, JsonOptions)
                    ?? throw new InvalidOperationException($"Unable to deserialize message from {queueName}.");

                await handler(message, cancellationToken);
                await channel.BasicAckAsync(args.DeliveryTag, false, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed processing message from {QueueName}", queueName);
                await channel.BasicNackAsync(args.DeliveryTag, false, requeue: false, cancellationToken);
            }
        };

        await channel.BasicConsumeAsync(queueName, autoAck: false, consumer, cancellationToken);
        _logger.LogInformation("Subscribed to queue {QueueName}", queueName);
    }

    /// <inheritdoc />
    public async Task<(uint MessageCount, uint ConsumerCount)> GetQueueStatsAsync(
        string queueName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await using var channel = await CreateChannelAsync(cancellationToken);
            var result = await channel.QueueDeclarePassiveAsync(queueName, cancellationToken);
            return (result.MessageCount, result.ConsumerCount);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Unable to read stats for queue {QueueName}", queueName);
            return (0, 0);
        }
    }

    private async Task<IChannel> CreateChannelAsync(CancellationToken cancellationToken)
    {
        var connection = await EnsureConnectionAsync(cancellationToken);
        return await connection.CreateChannelAsync(cancellationToken: cancellationToken);
    }

    private async Task<IConnection> EnsureConnectionAsync(CancellationToken cancellationToken)
    {
        if (_connection is { IsOpen: true })
        {
            return _connection;
        }

        await _connectionLock.WaitAsync(cancellationToken);
        try
        {
            if (_connection is { IsOpen: true })
            {
                return _connection;
            }

            _connection = await _factory.CreateConnectionAsync("gpay-banking", cancellationToken);
            return _connection;
        }
        finally
        {
            _connectionLock.Release();
        }
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        foreach (var channel in _consumerChannels)
        {
            await channel.CloseAsync();
            await channel.DisposeAsync();
        }

        _consumerChannels.Clear();

        if (_connection is not null)
        {
            await _connection.CloseAsync();
            await _connection.DisposeAsync();
        }

        _connectionLock.Dispose();
    }
}
