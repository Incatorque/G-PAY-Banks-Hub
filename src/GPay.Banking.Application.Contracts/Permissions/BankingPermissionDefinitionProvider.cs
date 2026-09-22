using Volo.Abp.Authorization.Permissions;

namespace GPay.Banking.Permissions;

public class BankingPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var group = context.AddGroup(BankingPermissions.GroupName);

        var avs = group.AddPermission(AvsPermissions.Default);
        avs.AddChild(AvsPermissions.Verify);
        avs.AddChild(AvsPermissions.Upload);
        avs.AddChild(AvsPermissions.View);

        var pay = group.AddPermission(PaymentsPermissions.Default);
        pay.AddChild(PaymentsPermissions.Initiate);
        pay.AddChild(PaymentsPermissions.View);

        var history = group.AddPermission(TransactionHistoryPermissions.Default);
        history.AddChild(TransactionHistoryPermissions.View);
    }
}
