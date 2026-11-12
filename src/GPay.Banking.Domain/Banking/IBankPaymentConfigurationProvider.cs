using GPay.Banking;

namespace GPay.Banking.Domain;

/// <summary>Reads bank rails and their execution/amount rules from the shared GPay database.</summary>
public interface IBankPaymentConfigurationProvider
{
    Task<BankPaymentConfiguration?> GetAsync(string bank, CancellationToken cancellationToken = default);
}
