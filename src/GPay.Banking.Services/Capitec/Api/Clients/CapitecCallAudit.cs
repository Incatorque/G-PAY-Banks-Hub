using GPay.Banking.Domain;
using Microsoft.Extensions.Logging;

namespace GPay.Banking.Services.Capitec.Api.Clients;

internal static class CapitecCallAudit
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
            logger.LogWarning(ex, "Capitec API audit write failed for {Operation}", entry.Operation);
        }
    }
}
