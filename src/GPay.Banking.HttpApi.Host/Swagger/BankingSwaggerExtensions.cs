using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Volo.Abp.Modularity;

namespace GPay.Banking.Swagger;

/// <summary>
/// OpenAPI / Swagger setup for the GPay Banking HTTP API.
/// </summary>
internal static class BankingSwaggerExtensions
{
    public static void AddBankingSwagger(this ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var requireGpayAuth = configuration.GetValue("AuthServer:RequireGpayAuth", true);

        context.Services.AddAbpSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "GPay Banking API",
                Version = "v1",
                Description = BankingSwaggerDescription.Build(requireGpayAuth),
                Contact = new OpenApiContact
                {
                    Name = "GPay Banking",
                    Email = "support@gpay.co.za"
                }
            });

            options.DocInclusionPredicate((_, _) => true);
            options.CustomSchemaIds(type => type.FullName?.Replace("+", ".") ?? type.Name);
            options.DescribeAllParametersInCamelCase();

            if (requireGpayAuth)
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description =
                        "Paste your JWT only (Swagger adds the <code>Bearer</code> prefix). " +
                        "Audience must be <code>GPay</code>.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            }

            IncludeXmlComments(options);
        });
    }

    private static void IncludeXmlComments(Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions options)
    {
        var baseDir = AppContext.BaseDirectory;
        var xmlNames = new[]
        {
            "GPay.Banking.Application.Contracts.xml",
            "GPay.Banking.Application.xml",
            "GPay.Banking.HttpApi.Host.xml",
            $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"
        };

        foreach (var name in xmlNames.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var path = Path.Combine(baseDir, name);
            if (File.Exists(path))
            {
                options.IncludeXmlComments(path, includeControllerXmlComments: true);
            }
        }
    }
}
