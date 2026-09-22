using System.Text.Json.Serialization;
using GPay.Banking.EntityFrameworkCore;
using GPay.Banking.Services;
using GPay.Banking.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace GPay.Banking;

[DependsOn(
    typeof(BankingHttpApiModule),
    typeof(BankingApplicationModule),
    typeof(BankingEntityFrameworkCoreModule),
    typeof(BankingServicesModule),
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule)
)]
public class BankingHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hosting = context.Services.GetHostingEnvironment();
        var requireGpayAuth = configuration.GetValue("AuthServer:RequireGpayAuth", true);

        context.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });

        if (requireGpayAuth)
        {
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = configuration.GetValue("AuthServer:RequireHttpsMetadata", true);
                    options.Audience = configuration["AuthServer:Audience"] ?? "GPay";
                });

            context.Services.AddAuthorization();
        }
        else
        {
            // No JWT — [Authorize] / permission attributes still succeed.
            context.Services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAssertion(_ => true)
                    .Build();
            });
            context.Services.AddSingleton<IAuthorizationHandler, AllowWhenGpayAuthDisabledHandler>();
        }

        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(BankingApplicationModule).Assembly);
        });

        context.AddBankingSwagger();

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                var origins = configuration.GetSection("App:CorsOrigins").Get<string[]>()
                    ?? ["http://localhost:4200"];
                builder.WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        _ = hosting;
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();
        var requireGpayAuth = context.GetConfiguration().GetValue("AuthServer:RequireGpayAuth", true);

        app.UseForwardedHeaders();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCorrelationId();
        app.UseCors();
        app.UseRouting();

        if (requireGpayAuth)
        {
            app.UseAuthentication();
        }

        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "GPay Banking API v1");
            options.DocumentTitle = "GPay Banking API";
            options.DisplayRequestDuration();
            options.EnableDeepLinking();
            options.EnableFilter();
            options.DefaultModelsExpandDepth(2);
            options.HeadContent = BankingSwaggerDescription.Styles;
        });
        app.UseConfiguredEndpoints();
    }
}

/// <summary>
/// When <c>AuthServer:RequireGpayAuth</c> is false, satisfies every authorization requirement.
/// </summary>
internal sealed class AllowWhenGpayAuthDisabledHandler : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        foreach (var requirement in context.PendingRequirements.ToList())
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
