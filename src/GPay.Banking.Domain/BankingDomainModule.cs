using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace GPay.Banking;

[DependsOn(
    typeof(BankingDomainSharedModule),
    typeof(AbpDddDomainModule)
)]
public class BankingDomainModule : AbpModule
{
}
