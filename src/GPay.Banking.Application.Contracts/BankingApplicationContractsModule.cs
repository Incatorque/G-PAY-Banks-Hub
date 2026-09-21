using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace GPay.Banking;

[DependsOn(
    typeof(BankingDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
)]
public class BankingApplicationContractsModule : AbpModule
{
}
