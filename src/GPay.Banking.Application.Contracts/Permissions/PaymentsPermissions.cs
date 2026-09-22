namespace GPay.Banking.Permissions;

/// <summary>
/// Instant payment permissions under <c>Banking.Payments</c>.
/// </summary>
public static class PaymentsPermissions
{
    /// <summary>Default payments grant: <c>Banking.Payments</c>.</summary>
    public const string Default = BankingPermissions.GroupName + ".Payments";

    /// <summary>Initiate payments: <c>Banking.Payments.Initiate</c>.</summary>
    public const string Initiate = Default + ".Initiate";

    /// <summary>View status / records: <c>Banking.Payments.View</c>.</summary>
    public const string View = Default + ".View";
}
