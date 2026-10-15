using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.X509Certificates;
using GPay.Banking.Services.Absa;

namespace GPay.Banking.Services.Absa.Api.Clients;

/// <summary>
/// Configures the Absa <see cref="HttpClient"/> including optional mTLS.
/// </summary>
public static class AbsaHttpClientConfigurator
{
    /// <summary>
    /// Applies certificate handler when configured.
    /// </summary>
    public static IHttpClientBuilder ConfigureAbsaPrimaryHandler(this IHttpClientBuilder builder)
    {
        builder.ConfigurePrimaryHttpMessageHandler(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AbsaCapiOptions>>().Value;
            var handler = new HttpClientHandler();

            var logger = sp.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(AbsaHttpClientConfigurator));
            var cert = AbsaClientCertificateLoader.Load(options, logger);
            if (cert is not null)
            {
                handler.ClientCertificates.Add(cert);
            }
            return handler;
        });

        return builder;
    }
}
