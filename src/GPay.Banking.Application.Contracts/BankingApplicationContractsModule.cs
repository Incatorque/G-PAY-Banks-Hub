using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace GPay.Banking;

[DependsOn(
    typeof(BankingDomainSharedModule),
    typeof(AbpDddApplicationContractsModule)
)]
public class BankingApplicationContractsModule : AbpModule
{
}
