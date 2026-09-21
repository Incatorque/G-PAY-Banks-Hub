namespace GPay.Banking.Permissions;

public static class AvsPermissions
{
    public const string Default = BankingPermissions.GroupName + ".Avs";
    public const string Verify = Default + ".Verify";
    public const string Upload = Default + ".Upload";
    public const string View = Default + ".View";
}
