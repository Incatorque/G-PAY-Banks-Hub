using GPay.Banking.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace GPay.Banking;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        Log.Logger = CreateLoggerConfiguration().CreateLogger();

        try
        {
            var logDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");
            Directory.CreateDirectory(logDirectory);
            await Log.CloseAndFlushAsync();

            Log.Logger = CreateLoggerConfiguration()
                .WriteTo.Async(c => c.File(
                    Path.Combine(logDirectory, "BankHub-.log"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 31,
                    shared: true,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}"))
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();

            foreach (var bank in Enum.GetNames<BankCode>())
            {
                builder.Configuration.AddJsonFile(
                    $"appsettings.{bank}.json",
                    optional: true,
                    reloadOnChange: true);
            }

            builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
            builder.Configuration.AddEnvironmentVariables(prefix: "GPAY_");

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

    private static LoggerConfiguration CreateLoggerConfiguration() =>
        new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.Console());
}
