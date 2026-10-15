using System.Net;
using System.Net.Mail;
using GPay.Banking.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GPay.Banking.EntityFrameworkCore.GPay;

/// <summary>SMTP notifications for GPay order lifecycle events produced by Bank Hub.</summary>
public sealed class SmtpGpayPaymentNotifier : IGpayPaymentNotifier
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<SmtpGpayPaymentNotifier> _logger;

    public SmtpGpayPaymentNotifier(
        IConfiguration configuration,
        ILogger<SmtpGpayPaymentNotifier> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public Task NotifyFailureAsync(int orderNumber, string? reference, string? reason, CancellationToken cancellationToken = default) =>
        SendAsync(
            "FailureRecipients",
            $"Bank Hub payment failed for order {orderNumber}",
            $"Order {orderNumber} failed.\nReference: {reference ?? "-"}\nReason: {reason ?? "Bank reported a failure."}",
            cancellationToken);

    public Task NotifyOrderCompletedAsync(int orderNumber, string? reference, string? status, string? note, CancellationToken cancellationToken = default) =>
        SendAsync(
            "OrderCompletedRecipients",
            $"Bank Hub order {orderNumber} completed",
            $"Order {orderNumber} completed.\nReference: {reference ?? "-"}\nBank status: {status ?? "Completed"}\nDetails: {note ?? "-"}",
            cancellationToken);

    public Task NotifyOrderReconciledAsync(int orderNumber, string? reference, CancellationToken cancellationToken = default) =>
        SendAsync(
            "OrderReconciledRecipients",
            $"Bank Hub order {orderNumber} reconciled",
            $"Order {orderNumber} reconciled against its Bank Hub payment statement.\nReference: {reference ?? "-"}",
            cancellationToken);

    private async Task SendAsync(string recipientsKey, string subject, string body, CancellationToken cancellationToken)
    {
        var section = _configuration.GetSection("GpayNotifications");
        var host = section["SmtpHost"];
        var from = section["From"];
        var recipients = section[recipientsKey];
        if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(recipients))
        {
            _logger.LogWarning(
                "GPay email skipped because SMTP host, sender, or {RecipientsKey} is not configured. Subject: {Subject}",
                recipientsKey,
                subject);
            return;
        }

        try
        {
            using var message = new MailMessage { From = new MailAddress(from), Subject = subject, Body = body };
            foreach (var address in recipients.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                message.To.Add(address);
            }

            if (message.To.Count == 0)
            {
                _logger.LogWarning("GPay email skipped because {RecipientsKey} has no recipients. Subject: {Subject}", recipientsKey, subject);
                return;
            }

            var port = int.TryParse(section["SmtpPort"], out var parsedPort) && parsedPort > 0 ? parsedPort : 25;
            using var client = new SmtpClient(host, port)
            {
                EnableSsl = bool.TryParse(section["EnableSsl"], out var enableSsl) && enableSsl,
                Timeout = (int.TryParse(section["SendTimeoutSeconds"], out var timeout) && timeout > 0 ? timeout : 30) * 1000
            };

            var username = section["Username"];
            if (!string.IsNullOrWhiteSpace(username))
            {
                client.Credentials = new NetworkCredential(username, section["Password"]);
            }

            cancellationToken.ThrowIfCancellationRequested();
            await client.SendMailAsync(message);
            _logger.LogInformation("GPay payment notification sent. Subject: {Subject}", subject);
        }
        catch (Exception ex)
        {
            // A notification failure must not undo a bank status or database update.
            _logger.LogError(ex, "Failed to send GPay payment notification. Subject: {Subject}", subject);
        }
    }
}
