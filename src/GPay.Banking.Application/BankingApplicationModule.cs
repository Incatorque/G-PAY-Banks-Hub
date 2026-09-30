using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Modularity;

namespace GPay.Banking;

[DependsOn(
    typeof(BankingDomainModule),
    typeof(BankingApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpBackgroundJobsModule),
    typeof(AbpBackgroundWorkersModule)
)]
public class BankingApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<AvsBatchOptions>(configuration.GetSection(AvsBatchOptions.SectionName));
        Configure<BankCallbackOptions>(configuration.GetSection(BankCallbackOptions.SectionName));
        Configure<Services.Absa.AbsaStatusPollOptions>(configuration.GetSection(Services.Absa.AbsaStatusPollOptions.SectionName));

        context.Services.AddHttpContextAccessor();
        context.Services.AddScoped<Domain.IBankApiCallAuditor, BankApiCallAuditor>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<BankingApplicationModule>();
        });
    }

    public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await base.OnApplicationInitializationAsync(context);
        await context.AddBackgroundWorkerAsync<BackgroundWorkers.AbsaPaymentStatusWorker>();
    }
}

