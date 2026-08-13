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
    /// Orchestrator response queue receiving bank service replies.
    /// </summary>
    public const string OrchestratorResponses = "gpay.banking.orchestrator.responses";

    /// <summary>
    /// Builds the request queue name for a bank.
    /// </summary>
    public static string BankRequests(BankCode bankCode) =>
        $"gpay.banking.{bankCode.ToString().ToLowerInvariant()}.requests";
}
