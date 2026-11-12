using GPay.Banking.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace GPay.Banking;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        var logDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");
        Directory.CreateDirectory(logDirectory);
        var fileLogPath = Path.Combine(logDirectory, "BankHub-.log");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Async(c => c.File(
                fileLogPath,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 31,
                shared: true))
            .WriteTo.Async(c => c.Console())
            .CreateBootstrapLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac();

            foreach (var bank in Enum.GetNames<BankCode>())
            {
                builder.Configuration.AddJsonFile(
                    $"appsettings.{bank}.json",
                    optional: true,
                    reloadOnChange: true);
            }

            builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
            builder.Configuration.AddEnvironmentVariables(prefix: "GPAY_");

            builder.Host.UseSerilog((context, services, loggerConfiguration) =>
                ConfigureLogger(loggerConfiguration, context.Configuration, context.HostingEnvironment, logDirectory));

            await builder.AddApplicationAsync<BankingHttpApiHostModule>();
            var app = builder.Build();
            app.MapGet("/health", async (BankingDbContext db, CancellationToken cancellationToken) =>
            {
                var isHealthy = await db.Database.CanConnectAsync(cancellationToken);
                return Results.Json(
                    new { status = isHealthy ? "Healthy" : "Unhealthy", checkedAtUtc = DateTimeOffset.UtcNow },
                    statusCode: isHealthy ? StatusCodes.Status200OK : StatusCodes.Status503ServiceUnavailable);
            }).AllowAnonymous();

            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "GPay Banking host terminated unexpectedly");
            return 1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    private static void ConfigureLogger(
        LoggerConfiguration loggerConfiguration,
        IConfiguration configuration,
        IHostEnvironment hostingEnvironment,
        string logDirectory)
    {
        var environmentName = hostingEnvironment.EnvironmentName.ToLowerInvariant();
        loggerConfiguration
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
            .MinimumLevel.Override("HealthChecks", LogEventLevel.Warning)
            .Filter.ByExcluding(logEvent =>
                logEvent.Properties.TryGetValue("RequestPath", out var requestPath) &&
                (requestPath.ToString().Contains("/health", StringComparison.OrdinalIgnoreCase) ||
                 requestPath.ToString().Contains("/health-status", StringComparison.OrdinalIgnoreCase)))
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "GPay.BankHub.HttpApi.Host")
            .Enrich.WithProperty("Environment", environmentName)
            .WriteTo.Async(c => c.File(
                Path.Combine(logDirectory, "BankHub-.log"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 31,
                shared: true,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}"))
            .WriteTo.Async(c => c.Console());

        if (!configuration.GetValue<bool>("Elasticsearch:Enabled"))
        {
            return;
        }

        var elasticsearchUrl = configuration["Elasticsearch:Url"];
        if (!Uri.TryCreate(elasticsearchUrl, UriKind.Absolute, out var elasticsearchUri))
        {
            SelfLog.Enable(Console.Error);
            Console.Error.WriteLine("Elasticsearch logging is enabled, but Elasticsearch:Url is missing or invalid. File and console logging remain active.");
            return;
        }

        var indexPrefix = configuration["Elasticsearch:IndexPrefix"];
        if (string.IsNullOrWhiteSpace(indexPrefix))
        {
            indexPrefix = "bankhub";
        }

        loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticsearchUri)
        {
            IndexFormat = $"{environmentName}-{indexPrefix.Trim().ToLowerInvariant()}-{{0:yyyy.MM.dd}}",
            AutoRegisterTemplate = configuration.GetValue("Elasticsearch:AutoRegisterTemplate", true),
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
            EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog
        });
        SelfLog.Enable(Console.Error);
    }
}
