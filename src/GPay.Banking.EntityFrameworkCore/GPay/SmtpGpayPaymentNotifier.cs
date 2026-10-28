using System.Net;
using System.Net.Mail;
using System.Globalization;
using System.Text.Encodings.Web;
using GPay.Banking.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GPay.Banking.EntityFrameworkCore.GPay;

/// <summary>SMTP notifications for GPay order lifecycle events produced by Bank Hub.</summary>
internal sealed class SmtpGpayPaymentNotifier : IGpayPaymentNotifier
{
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ILogger<SmtpGpayPaymentNotifier> _logger;

    public SmtpGpayPaymentNotifier(
        IConfiguration configuration,
        IHostEnvironment hostEnvironment,
        ILogger<SmtpGpayPaymentNotifier> logger)
    {
        _configuration = configuration;
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }

    public Task NotifyPaymentInitiatedAsync(
        GpayPaymentInitiatedNotification notification,
        CancellationToken cancellationToken = default) =>
        SendPaymentReportAsync(notification, cancellationToken);

    private async Task SendPaymentReportAsync(
        GpayPaymentInitiatedNotification notification,
        CancellationToken cancellationToken)
    {
        var templateFileName = notification.IsStatusCheck
            ? notification.IsError
                ? "payment-status-check-error.html"
                : "payment-status-check-success.html"
            : notification.IsError
                ? "payment-initiate-error.html"
                : "payment-initiate.html";
        var templatePath = new[]
            {
                Path.Combine(_hostEnvironment.ContentRootPath, "Templates", templateFileName),
                Path.Combine(AppContext.BaseDirectory, "Templates", templateFileName)
            }
            .FirstOrDefault(File.Exists);

        if (templatePath is null)
        {
            _logger.LogError("Payment notification email template {TemplateFileName} was not found.", templateFileName);
            return;
        }

        var html = await File.ReadAllTextAsync(templatePath, cancellationToken);
        var environmentBadge = _hostEnvironment.IsDevelopment() ||
                               string.Equals(_hostEnvironment.EnvironmentName, "Dev", StringComparison.OrdinalIgnoreCase)
            ? "DEV / TESTING"
            : "PROD";
        var statusDescription = notification.StatusDescription ??
                                (notification.IsError
                                    ? "The bank reported that payment settlement failed."
                                    : "Payment successfully processed and settled.");

        html = html
            .Replace("{{EnvironmentBadge}}", Encode(environmentBadge), StringComparison.Ordinal)
            .Replace("{{BankName}}", Encode(notification.Bank), StringComparison.Ordinal)
            .Replace("{{Currency}}", Encode(notification.Currency), StringComparison.Ordinal)
            .Replace("{{Amount}}", notification.Amount.ToString("N2", CultureInfo.InvariantCulture), StringComparison.Ordinal)
            .Replace("{{OrderNumber}}", Encode(notification.OrderNumber), StringComparison.Ordinal)
            .Replace("{{FromAccountNumber}}", Encode(notification.FromAccountNumber), StringComparison.Ordinal)
            .Replace("{{ToAccountNumber}}", Encode(notification.ToAccountNumber), StringComparison.Ordinal)
            .Replace("{{Reference}}", Encode(notification.Reference), StringComparison.Ordinal)
            .Replace("{{Date}}", DateTimeOffset.Now.ToString("dd MMM yyyy HH:mm", CultureInfo.InvariantCulture), StringComparison.Ordinal)
            .Replace("{{Status}}", Encode(notification.Status), StringComparison.Ordinal)
            .Replace("{{ErrorCode}}", Encode(notification.ErrorCode ?? notification.Status), StringComparison.Ordinal)
            .Replace("{{ErrorMessage}}", Encode(notification.ErrorMessage ?? statusDescription), StringComparison.Ordinal)
            .Replace("{{StatusDescription}}", Encode(statusDescription), StringComparison.Ordinal)
            .Replace("{{TransactionReference}}", Encode(notification.TransactionReference ?? "-"), StringComparison.Ordinal);

        await SendAsync(
            "PaymentInitiatedRecipients",
            "ABSA CAPI - Payment Initiation Status Report",
            html,
            cancellationToken);
    }

    private async Task SendAsync(
        string recipientsKey,
        string subject,
        string body,
        CancellationToken cancellationToken)
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
            using var message = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            foreach (var address in recipients.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                message.To.Add(address);
            }

            if (message.To.Count == 0)
            {
                _logger.LogWarning("GPay email skipped because {RecipientsKey} has no recipients. Subject: {Subject}", recipientsKey, subject);
                return;
            }

            var port = PositiveInt(section["SmtpPort"], 25);
            using var client = new SmtpClient(host, port)
            {
                EnableSsl = bool.TryParse(section["EnableSsl"], out var enableSsl) && enableSsl,
                Timeout = PositiveInt(section["SendTimeoutSeconds"], 30) * 1000
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

    private static int PositiveInt(string? value, int fallback) =>
        int.TryParse(value, out var parsed) && parsed > 0 ? parsed : fallback;

    private static string Encode(string? value) => HtmlEncoder.Default.Encode(value ?? string.Empty);
}
