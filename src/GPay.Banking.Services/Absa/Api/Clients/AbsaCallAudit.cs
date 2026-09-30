using GPay.Banking.Domain;
using Microsoft.Extensions.Logging;

namespace GPay.Banking.Services.Absa.Api.Clients;

internal static class AbsaCallAudit
{
    public static async Task WriteAsync(
        IBankApiCallAuditor auditor,
        ILogger logger,
        BankApiCallAuditEntry entry,
        CancellationToken cancellationToken)
    {
        try
        {
            await auditor.RecordAsync(entry, CancellationToken.None);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Absa API audit write failed for {Operation}", entry.Operation);
        }
    }
}
