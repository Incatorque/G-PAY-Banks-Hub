using GPay.Banking;
using GPay.Banking.Domain;
using GPay.Banking.Services.Absa;
using GPay.Banking.Services.Absa.Api.Clients;
using GPay.Banking.Services.Absa.Api.Mapping;
using GPay.Banking.Services.Absa.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace GPay.Banking.Services;

[DependsOn(typeof(BankingDomainModule))]
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

        AddAbsaClient<IAbsaSessionProvider, AbsaSessionProvider>(context.Services);
        AddAbsaClient<IAbsaCapiClient, AbsaCapiClient>(context.Services);

        var absa = BankCode.Absa.ToString();
        context.Services.AddKeyedScoped<IAccountVerificationService, AbsaAccountVerificationService>(absa);
        context.Services.AddKeyedScoped<IInstantPaymentService, AbsaInstantPaymentService>(absa);
        context.Services.AddKeyedScoped<IStatementService, AbsaStatementService>(absa);
        context.Services.AddKeyedScoped<IPaymentCallbackService, AbsaPaymentCallbackService>(absa);
        context.Services.AddKeyedScoped<IPaymentCallbackRegistrationService, AbsaPaymentCallbackRegistrationService>(absa);
    }

    private static void AddAbsaClient<TService, TImplementation>(IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        services.AddHttpClient<TService, TImplementation>()
            .AddHttpMessageHandler(sp => new BankApiCallAuditHandler(
                sp.GetRequiredService<IServiceScopeFactory>(),
                sp.GetRequiredService<ILogger<BankApiCallAuditHandler>>(),
                BankCode.Absa.ToString()))
            .ConfigureAbsaClient();
    }
}
