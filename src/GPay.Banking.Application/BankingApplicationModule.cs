using GPay.Banking.Auditing;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Modularity;

namespace GPay.Banking;

[DependsOn(
    typeof(BankingDomainModule),
    typeof(BankingApplicationContractsModule),
    typeof(AbpDddApplicationModule),
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

        Configure<PaymentStatusPollOptions>(configuration.GetSection(PaymentStatusPollOptions.SectionName));

        Configure<BankPaymentIntakeOptions>(configuration.GetSection(BankPaymentIntakeOptions.SectionName));
        context.Services.AddScoped<IInstantPaymentInitiator, InstantPaymentInitiator>();
        context.Services.AddTransient<Helpers.IGpayPaymentNotificationQueue, Helpers.GpayPaymentNotificationQueue>();

        context.Services.AddHttpContextAccessor();

        context.Services.AddScoped<Domain.IBankApiCallAuditor, BankApiCallAuditor>();

    }

    public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await base.OnApplicationInitializationAsync(context);

        await context.AddBackgroundWorkerAsync<BackgroundWorkers.PaymentStatusWorker>();

        await context.AddBackgroundWorkerAsync<BackgroundWorkers.BankPaymentOrderWorker>();
    }
}
