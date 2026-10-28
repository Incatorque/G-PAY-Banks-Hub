using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;
using GPay.Banking.Services.Absa;

namespace GPay.Banking.Services.Absa.Api.Clients;

/// <summary>
/// Configures the Absa <see cref="HttpClient"/> including optional mTLS.
/// </summary>
internal static class AbsaHttpClientConfigurator
{
    /// <summary>
    /// Applies the shared base address, timeout, and optional certificate handler.
    /// </summary>
    internal static IHttpClientBuilder ConfigureAbsaClient(this IHttpClientBuilder builder)
    {
        builder.ConfigureHttpClient((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<AbsaCapiOptions>>().Value;
            if (!string.IsNullOrWhiteSpace(options.BaseUrl))
            {
                client.BaseAddress = new Uri(options.BaseUrl);
            }

            client.Timeout = TimeSpan.FromSeconds(Math.Max(5, options.TimeoutSeconds));
        });

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
