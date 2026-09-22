namespace GPay.Banking.Permissions;

/// <summary>
/// Account verification (AVS) permissions under <c>Banking.Avs</c>.
/// </summary>
public static class AvsPermissions
{
    /// <summary>Default AVS grant: <c>Banking.Avs</c>.</summary>
    public const string Default = BankingPermissions.GroupName + ".Avs";

    /// <summary>Single-account verify: <c>Banking.Avs.Verify</c>.</summary>
    public const string Verify = Default + ".Verify";

    /// <summary>Batch upload / submit: <c>Banking.Avs.Upload</c>.</summary>
    public const string Upload = Default + ".Upload";

    /// <summary>View batches and records: <c>Banking.Avs.View</c>.</summary>
    public const string View = Default + ".View";
}
