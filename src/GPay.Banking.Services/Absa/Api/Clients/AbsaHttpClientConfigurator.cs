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

            if (!string.IsNullOrWhiteSpace(options.ClientCertificatePath) &&
                File.Exists(options.ClientCertificatePath))
            {
                var cert = string.IsNullOrWhiteSpace(options.ClientCertificatePassword)
                    ? X509CertificateLoader.LoadPkcs12FromFile(options.ClientCertificatePath, null)
                    : X509CertificateLoader.LoadPkcs12FromFile(
                        options.ClientCertificatePath,
                        options.ClientCertificatePassword);

                handler.ClientCertificates.Add(cert);
            }

            return handler;
        });

        return builder;
    }
}
