using GPay.Banking.Infrastructure;
using GPay.Banking.Infrastructure.Http;
using GPay.Banking.Infrastructure.Logging;
using GPay.Banking.Orchestrator.Consumers;
using GPay.Banking.Orchestrator.Services;
using GPay.Banking.Persistence;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

builder.Host.UseGpaySerilog("GPay.Banking.Orchestrator");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GPay Banking Orchestrator",
        Version = "v1",
        Description = "Unified banking orchestrator API for GPay bank integrations."
    });

    var xml = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    if (File.Exists(xml))
    {
        options.IncludeXmlComments(xml, includeControllerXmlComments: true);
    }

    var contractsXml = Path.Combine(AppContext.BaseDirectory, "GPay.Banking.Contracts.xml");
    if (File.Exists(contractsXml))
    {
        options.IncludeXmlComments(contractsXml);
    }
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("OpsUi", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins(
                builder.Configuration.GetSection("Cors:Origins").Get<string[]>()
                ?? ["http://localhost:4200", "http://localhost"]);
    });
});

builder.Services.AddGpayBankingInfrastructure(builder.Configuration);
builder.Services.AddGpayBankingPersistence(builder.Configuration);
builder.Services.AddScoped<IBankRoutingService, BankRoutingService>();
builder.Services.AddHostedService<BankResponseConsumer>();
builder.Services.AddHostedService<OrchestratorRequestConsumer>();

var app = builder.Build();

await app.Services.EnsureGpayBankingDatabaseAsync();

app.UseGpayGlobalExceptionHandler();
app.UseMiddleware<HttpTrafficLoggingMiddleware>();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("OpsUi");
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

/// <summary>
/// Marker for WebApplicationFactory integration tests.
/// </summary>
public partial class Program;
