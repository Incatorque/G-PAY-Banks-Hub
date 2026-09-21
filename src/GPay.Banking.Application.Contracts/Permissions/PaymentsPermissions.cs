namespace GPay.Banking.Permissions;

public static class PaymentsPermissions
{
    public const string Default = BankingPermissions.GroupName + ".Payments";
    public const string Initiate = Default + ".Initiate";
    public const string View = Default + ".View";
}
