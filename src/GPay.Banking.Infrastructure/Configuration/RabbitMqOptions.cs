namespace GPay.Banking.Infrastructure.Configuration;

/// <summary>
/// RabbitMQ connection and queue threshold settings.
/// </summary>
public sealed class RabbitMqOptions
{
    /// <summary>
    /// Configuration section name.
    /// </summary>
    public const string SectionName = "RabbitMq";

    /// <summary>
    /// Broker hostname.
    /// </summary>
    public string HostName { get; set; } = "localhost";

    /// <summary>
    /// AMQP port.
    /// </summary>
    public int Port { get; set; } = 5672;

    /// <summary>
    /// Virtual host.
    /// </summary>
    public string VirtualHost { get; set; } = "/";

    /// <summary>
    /// Username.
    /// </summary>
    public string UserName { get; set; } = "guest";

    /// <summary>
    /// Password.
    /// </summary>
    public string Password { get; set; } = "guest";

    /// <summary>
    /// Message count above which a queue is considered clogged.
    /// </summary>
    public int ClogThreshold { get; set; } = 1000;

    /// <summary>
    /// RabbitMQ management API base URL (optional, for richer queue stats).
    /// </summary>
    public string? ManagementBaseUrl { get; set; }
}
