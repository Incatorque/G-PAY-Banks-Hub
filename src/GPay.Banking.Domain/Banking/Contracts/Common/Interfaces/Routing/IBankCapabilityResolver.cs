namespace GPay.Banking.Domain;

/// <summary>
/// Resolves bank capability adapters by bank code.
/// </summary>
public interface IBankCapabilityResolver
{
    bool SupportsInstantPayment(BankCode bank);
    IAccountVerificationService GetAccountVerification(BankCode bank);
    IInstantPaymentService GetInstantPayment(BankCode bank);
    IStatementService GetStatement(BankCode bank);
    IPaymentCallbackService GetPaymentCallback(BankCode bank);
    IPaymentCallbackRegistrationService GetPaymentCallbackRegistration(BankCode bank);
}
