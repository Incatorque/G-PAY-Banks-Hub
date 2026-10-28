using GPay.Banking.Domain;

namespace GPay.Banking.Services.Absa.Api.Clients;

internal static class AbsaCallAudit
{
    public static async Task WriteAsync(
        IBankApiCallAuditor auditor,
        ILogger logger,
        BankApiCallAuditEntry entry)
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
