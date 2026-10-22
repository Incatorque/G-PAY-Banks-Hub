using GPay.Banking;
using GPay.Banking.Domain;
using GPay.Banking.Services.Absa;
using GPay.Banking.Services.Absa.Api.Clients;
using GPay.Banking.Services.Absa.Api.Mapping;
using GPay.Banking.Services.Absa.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace GPay.Banking.Services;

[DependsOn(
    typeof(BankingDomainModule),
    typeof(BankingDomainSharedModule),
    typeof(AbpAutofacModule)
)]
public class BankingServicesModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbsaCapiOptions>(configuration.GetSection(AbsaCapiOptions.SectionName));
        Configure<BankPaymentIntakeOptions>(configuration.GetSection(BankPaymentIntakeOptions.SectionName));
        Configure<BankCallbackOptions>(configuration.GetSection(BankCallbackOptions.SectionName));
        Configure<AvsBatchOptions>(configuration.GetSection(AvsBatchOptions.SectionName));

        context.Services.AddSingleton<IAbsaRequestSigner, AbsaRequestSigner>();
        context.Services.AddSingleton<IAbsaAvsMapper, AbsaAvsMapper>();
        context.Services.AddSingleton<IAbsaPaymentMapper, AbsaPaymentMapper>();
        context.Services.AddSingleton<IBankErrorMapper, AbsaErrorMapper>();
        context.Services.AddScoped<IBankCapabilityResolver, BankCapabilityResolver>();
        context.Services.AddScoped<IBankRouterService, BankRouterService>();

        context.Services.AddHttpClient<IAbsaSessionProvider, AbsaSessionProvider>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<AbsaCapiOptions>>().Value;
            if (!string.IsNullOrWhiteSpace(options.BaseUrl))
            {
                client.BaseAddress = new Uri(options.BaseUrl);
            }
            client.Timeout = TimeSpan.FromSeconds(Math.Max(5, options.TimeoutSeconds));
        })
            .AddHttpMessageHandler(sp => new BankApiCallAuditHandler(
                sp.GetRequiredService<IServiceScopeFactory>(),
                sp.GetRequiredService<ILogger<BankApiCallAuditHandler>>(),
                BankCode.Absa.ToString()))
            .ConfigureAbsaPrimaryHandler();

        context.Services.AddHttpClient<IAbsaCapiClient, AbsaCapiClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<AbsaCapiOptions>>().Value;
            if (!string.IsNullOrWhiteSpace(options.BaseUrl))
            {
                client.BaseAddress = new Uri(options.BaseUrl);
            }
            client.Timeout = TimeSpan.FromSeconds(Math.Max(5, options.TimeoutSeconds));
        })
            .AddHttpMessageHandler(sp => new BankApiCallAuditHandler(
                sp.GetRequiredService<IServiceScopeFactory>(),
                sp.GetRequiredService<ILogger<BankApiCallAuditHandler>>(),
                BankCode.Absa.ToString()))
            .ConfigureAbsaPrimaryHandler();

        var absa = BankCode.Absa.ToString();
        context.Services.AddKeyedScoped<IAccountVerificationService, AbsaAccountVerificationService>(absa);
        context.Services.AddKeyedScoped<IInstantPaymentService, AbsaInstantPaymentService>(absa);
        context.Services.AddKeyedScoped<IBalanceService, AbsaBalanceService>(absa);
        context.Services.AddKeyedScoped<IStatementService, AbsaStatementService>(absa);
        context.Services.AddKeyedScoped<ITransactionHistoryService, AbsaStatementService>(absa);
        context.Services.AddKeyedScoped<INotificationService, AbsaNotificationService>(absa);
        context.Services.AddKeyedScoped<IPaymentCallbackService, AbsaPaymentCallbackService>(absa);
        context.Services.AddKeyedScoped<IPaymentCallbackRegistrationService, AbsaPaymentCallbackRegistrationService>(absa);
    }
}
