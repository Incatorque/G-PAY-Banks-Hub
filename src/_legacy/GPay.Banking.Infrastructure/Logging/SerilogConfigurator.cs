using GPay.Banking.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace GPay.Banking.Infrastructure.Logging;

/// <summary>
/// Configures Serilog with console and optional Elasticsearch sinks for Kibana.
/// </summary>
public static class SerilogConfigurator
{
    /// <summary>
    /// Creates a Serilog logger from configuration.
    /// </summary>
    public static ILogger CreateLogger(IConfiguration configuration, string applicationName)
    {
        var es = configuration.GetSection(ElasticsearchOptions.SectionName).Get<ElasticsearchOptions>()
                 ?? new ElasticsearchOptions();

        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", applicationName)
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .WriteTo.Console();

        if (es.Enabled && !string.IsNullOrWhiteSpace(es.Uri))
        {
            loggerConfig.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(es.Uri))
            {
                AutoRegisterTemplate = true,
                IndexFormat = es.IndexFormat,
                NumberOfShards = 1,
                NumberOfReplicas = 0
            });
        }

        return loggerConfig.CreateLogger();
    }

    /// <summary>
    /// Applies Serilog to the host builder.
    /// </summary>
    public static IHostBuilder UseGpaySerilog(this IHostBuilder hostBuilder, string applicationName)
    {
        return hostBuilder.UseSerilog((context, _, config) =>
        {
            var es = context.Configuration.GetSection(ElasticsearchOptions.SectionName).Get<ElasticsearchOptions>()
                     ?? new ElasticsearchOptions();

            config
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", applicationName)
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()
                .WriteTo.Console();

            if (es.Enabled && !string.IsNullOrWhiteSpace(es.Uri))
            {
                config.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(es.Uri))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = es.IndexFormat
                });
            }
        });
    }
}
