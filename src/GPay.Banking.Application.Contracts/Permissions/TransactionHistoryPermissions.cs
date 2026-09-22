namespace GPay.Banking.Permissions;

/// <summary>
/// Transaction history / statement permissions under <c>Banking.TransactionHistory</c>.
/// </summary>
public static class TransactionHistoryPermissions
{
    /// <summary>Default grant: <c>Banking.TransactionHistory</c>.</summary>
    public const string Default = BankingPermissions.GroupName + ".TransactionHistory";

    /// <summary>Query account transaction history: <c>Banking.TransactionHistory.View</c>.</summary>
    public const string View = Default + ".View";
}
