using GPay.Banking.BankHub;
using Volo.Abp.Domain.Repositories;

namespace GPay.Banking.Helpers;

internal static class BankPaymentCallLookup
{
    public static async Task<BankHubApiCall?> FindAsync(
        IRepository<BankHubApiCall, Guid> repository,
        string bankCode,
        string? transactionReference,
        string? apiReference)
    {
        var calls = await repository.GetListAsync(call =>
            call.BankCode == bankCode &&
            call.Direction == "Outbound" &&
            call.Operation == "initiate" &&
            call.GpayOrderId.HasValue &&
            call.ResponseJson != null &&
            ((!string.IsNullOrWhiteSpace(transactionReference) && call.ResponseJson.Contains(transactionReference)) ||
             (!string.IsNullOrWhiteSpace(apiReference) && call.ResponseJson.Contains(apiReference))));

        return calls.OrderByDescending(call => call.CreationTime).FirstOrDefault();
    }
}
