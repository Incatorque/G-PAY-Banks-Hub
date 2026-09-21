using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace GPay.Banking;

[DependsOn(typeof(AbpValidationModule))]
public class BankingDomainSharedModule : AbpModule
{
}
