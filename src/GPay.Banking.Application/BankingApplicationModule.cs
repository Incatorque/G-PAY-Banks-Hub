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
        var paymentPollSection = configuration.GetSection(PaymentStatusPollOptions.SectionName);
        if (!paymentPollSection.GetChildren().Any())
        {
            paymentPollSection = configuration.GetSection("AbsaStatusPoll");
        }

        Configure<PaymentStatusPollOptions>(paymentPollSection);
        Configure<BackgroundWorkers.BankPaymentIntakeOptions>(configuration.GetSection(BackgroundWorkers.BankPaymentIntakeOptions.SectionName));
        context.Services.PostConfigure<BackgroundWorkers.BankPaymentIntakeOptions>(options =>
        {
            var bankHubSection = configuration.GetSection(BackgroundWorkers.BankPaymentIntakeOptions.SectionName);
            var legacyAbsaSection = bankHubSection.GetSection("Absa");

            if (string.IsNullOrWhiteSpace(bankHubSection["PollPeriodSeconds"]))
            {
                if (int.TryParse(legacyAbsaSection["PollPeriodSeconds"], out var legacyPeriodSeconds))
                {
                    options.PollPeriodSeconds = legacyPeriodSeconds;
                }
            }

            if (string.IsNullOrWhiteSpace(bankHubSection["BatchSize"]))
            {
                if (int.TryParse(legacyAbsaSection["BatchSize"], out var legacyBatchSize))
                {
                    options.BatchSize = legacyBatchSize;
                }
            }
        });
        context.Services.AddScoped<IInstantPaymentInitiator, InstantPaymentInitiator>();

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
        await context.AddBackgroundWorkerAsync<BackgroundWorkers.PaymentStatusWorker>();
        await context.AddBackgroundWorkerAsync<BackgroundWorkers.BankPaymentOrderWorker>();
    }
}

