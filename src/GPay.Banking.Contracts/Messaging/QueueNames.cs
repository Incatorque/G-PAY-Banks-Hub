using GPay.Banking.Contracts.Enums;

namespace GPay.Banking.Contracts.Messaging;

/// <summary>
/// Canonical RabbitMQ queue naming helpers.
/// </summary>
public static class QueueNames
{
    /// <summary>
    /// Orchestrator inbound queue for GPay-originated MQ requests.
    /// </summary>
    public const string OrchestratorRequests = "gpay.banking.orchestrator.requests";

    /// <summary>
    /// RabbitMQ pseudo-queue used for direct RPC replies to the orchestrator.
    /// </summary>
    public const string DirectReplyTo = "amq.rabbitmq.reply-to";

    /// <summary>
    /// Builds the request queue name for a bank.
    /// </summary>
    public static string BankRequests(BankCode bankCode) =>
        $"gpay.banking.{bankCode.ToString().ToLowerInvariant()}.requests";
}
