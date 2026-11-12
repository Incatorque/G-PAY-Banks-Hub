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
            call.GpayOrderId.HasValue &&
            ((transactionReference != null &&
              ((call.RequestJson != null && call.RequestJson.Contains(transactionReference)) ||
               (call.ResponseJson != null && call.ResponseJson.Contains(transactionReference)))) ||
             (apiReference != null &&
              ((call.RequestJson != null && call.RequestJson.Contains(apiReference)) ||
               (call.ResponseJson != null && call.ResponseJson.Contains(apiReference))))));

        return calls.OrderByDescending(call => call.CreationTime).FirstOrDefault();
    }
}
