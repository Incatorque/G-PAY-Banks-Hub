namespace GPay.Banking.Contracts.Enums;

/// <summary>
/// Banking capability operations routed through the orchestrator.
/// </summary>
public enum BankOperation
{
    /// <summary>Account holder / account verification (AVS).</summary>
    AccountVerification = 1,

    /// <summary>Instant payment (e.g. PayShap).</summary>
    InstantPayment = 2,

    /// <summary>Account balance enquiry.</summary>
    Balance = 3,

    /// <summary>Statement or transaction history retrieval.</summary>
    Statement = 4,

    /// <summary>Bank notification subscription or retrieval.</summary>
    Notification = 5
}
