using GPay.Banking.Absa.Clients;
using GPay.Banking.Absa.Configuration;
using GPay.Banking.Absa.Consumers;
using GPay.Banking.Absa.Mapping;
using GPay.Banking.Absa.Services;
using GPay.Banking.Contracts.Interfaces;
using GPay.Banking.Infrastructure;
using GPay.Banking.Infrastructure.Http;
using GPay.Banking.Infrastructure.Logging;
using GPay.Banking.Persistence;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables(prefix: "GPAY_");
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>(optional: true);
}

builder.Host.UseGpaySerilog("GPay.Banking.Absa");

builder.Services.AddGpayBankingInfrastructure(builder.Configuration);
builder.Services.AddGpayBankingPersistence(builder.Configuration);

builder.Services.Configure<AbsaCapiOptions>(builder.Configuration.GetSection(AbsaCapiOptions.SectionName));
builder.Services.AddSingleton<IAbsaRequestSigner, AbsaRequestSigner>();
builder.Services.AddSingleton<IAbsaAvsMapper, AbsaAvsMapper>();
builder.Services.AddSingleton<IBankErrorMapper, AbsaErrorMapper>();

builder.Services.AddHttpClient<IAbsaSessionProvider, AbsaSessionProvider>((sp, client) =>
{
    var options = sp.GetRequiredService<IOptions<AbsaCapiOptions>>().Value;
    if (!string.IsNullOrWhiteSpace(options.BaseUrl))
    {
        client.BaseAddress = new Uri(options.BaseUrl);
    }

    client.Timeout = TimeSpan.FromSeconds(Math.Max(5, options.TimeoutSeconds));
}).ConfigureAbsaPrimaryHandler();

builder.Services.AddHttpClient<IAbsaCapiClient, AbsaCapiClient>((sp, client) =>
{
    var options = sp.GetRequiredService<IOptions<AbsaCapiOptions>>().Value;
    if (!string.IsNullOrWhiteSpace(options.BaseUrl))
    {
        client.BaseAddress = new Uri(options.BaseUrl);
    }

    client.Timeout = TimeSpan.FromSeconds(Math.Max(5, options.TimeoutSeconds));
}).ConfigureAbsaPrimaryHandler();

builder.Services.AddScoped<IAccountVerificationService, AbsaAccountVerificationService>();
builder.Services.AddScoped<IInstantPaymentService, AbsaInstantPaymentService>();
builder.Services.AddScoped<IBalanceService, AbsaBalanceService>();
builder.Services.AddScoped<IStatementService, AbsaStatementService>();
builder.Services.AddScoped<ITransactionHistoryService, AbsaStatementService>();
builder.Services.AddScoped<INotificationService, AbsaNotificationService>();
builder.Services.AddHostedService<AbsaRequestConsumer>();

var app = builder.Build();

await app.Services.EnsureGpayBankingDatabaseAsync();

app.UseGpayGlobalExceptionHandler();
app.UseMiddleware<HttpTrafficLoggingMiddleware>();
app.UseSerilogRequestLogging();

app.MapGet("/health", (IOptions<AbsaCapiOptions> options) =>
{
    var o = options.Value;
    return Results.Ok(new
    {
        service = "Absa",
        status = "Healthy",
        capability = "AVS",
        mode = o.UseSimulator ? "simulator" : "live",
        baseUrl = o.BaseUrl,
        configured = IsLiveCredential(o.Username) &&
                     IsLiveCredential(o.Password) &&
                     IsLiveCredential(o.CapiCode) &&
                     IsLiveCredential(o.ClientApiKey),
        validateBankDetailsPath = o.ValidateBankDetailsPath,
        checkedAtUtc = DateTimeOffset.UtcNow
    });
});

app.Run();

static bool IsLiveCredential(string? value) =>
    !string.IsNullOrWhiteSpace(value) &&
    !value.StartsWith("SET_", StringComparison.OrdinalIgnoreCase);

/// <summary>
/// Marker for WebApplicationFactory integration tests.
/// </summary>
public partial class Program;
