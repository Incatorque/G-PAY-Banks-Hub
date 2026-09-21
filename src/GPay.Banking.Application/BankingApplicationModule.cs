using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace GPay.Banking;

[DependsOn(
    typeof(BankingDomainModule),
    typeof(BankingApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpBackgroundJobsModule)
)]
public class BankingApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<AvsBatchOptions>(configuration.GetSection(AvsBatchOptions.SectionName));
        Configure<BankCallbackOptions>(configuration.GetSection(BankCallbackOptions.SectionName));

        context.Services.AddHttpContextAccessor();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<BankingApplicationModule>();
        });
    }
}

