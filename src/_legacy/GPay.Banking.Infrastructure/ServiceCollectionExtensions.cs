using GPay.Banking.Infrastructure.Configuration;
using GPay.Banking.Infrastructure.Correlation;
using GPay.Banking.Infrastructure.Health;
using GPay.Banking.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GPay.Banking.Infrastructure;

/// <summary>
/// DI registration helpers for shared infrastructure.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers RabbitMQ, correlator, health, and configuration options.
    /// </summary>
    public static IServiceCollection AddGpayBankingInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.SectionName));
        services.Configure<ElasticsearchOptions>(configuration.GetSection(ElasticsearchOptions.SectionName));
        services.Configure<BankingOptions>(configuration.GetSection(BankingOptions.SectionName));

        services.AddSingleton<IMessageBus, RabbitMqMessageBus>();
        services.AddSingleton<IResponseCorrelator, InMemoryResponseCorrelator>();
        services.AddSingleton<IPlatformHealthService, PlatformHealthService>();
        services.AddHttpClient("BankHealth", client =>
        {
            client.Timeout = TimeSpan.FromSeconds(5);
        });

        return services;
    }
}
