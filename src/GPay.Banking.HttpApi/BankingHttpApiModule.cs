using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace GPay.Banking;

[DependsOn(
    typeof(BankingApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule)
)]
public class BankingHttpApiModule : AbpModule
{
}
