using System.Security.Cryptography.X509Certificates;
using GPay.Banking.Services.Absa;

namespace GPay.Banking.Services.Absa.Api.Clients;

/// <summary>Loads the configured Absa certificate from the Windows store, then falls back to a P12 file.</summary>
internal static class AbsaClientCertificateLoader
{
    public static X509Certificate2? Load(AbsaCapiOptions options, ILogger logger)
    {
        var thumbprint = NormalizeThumbprint(options.ClientCertificateSubjectContains);
        var storeLocation = Enum.TryParse<StoreLocation>(options.ClientCertificateStoreLocation, true, out var parsedLocation)
            ? parsedLocation
            : StoreLocation.LocalMachine;

        if (string.IsNullOrWhiteSpace(thumbprint))
        {
            logger.LogInformation(
                "No Absa certificate thumbprint is configured; skipping {StoreLocation}\\My store lookup and trying the configured P12 path.",
                storeLocation);
        }
        else
        {
            try
            {
                using var store = new X509Store(StoreName.My, storeLocation);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

                var certificate = store.Certificates
                    .Cast<X509Certificate2>()
                    .FirstOrDefault(candidate =>
                        candidate.HasPrivateKey &&
                        string.Equals(NormalizeThumbprint(candidate.Thumbprint), thumbprint, StringComparison.OrdinalIgnoreCase));

                if (certificate is not null)
                {
                    logger.LogInformation(
                        "Loaded Absa client certificate from {StoreLocation}\\My using configured thumbprint {Thumbprint}.",
                        storeLocation,
                        certificate.Thumbprint);
                    return new X509Certificate2(certificate);
                }

                logger.LogWarning(
                    "No Absa certificate with configured thumbprint {Thumbprint} and a private key was found in {StoreLocation}\\My; trying the configured P12 path.",
                    thumbprint,
                    storeLocation);
            }
            catch (Exception ex)
            {
                logger.LogWarning(
                    ex,
                    "Could not load Absa certificate thumbprint {Thumbprint} from {StoreLocation}\\My; trying the configured P12 path.",
                    thumbprint,
                    storeLocation);
            }
        }

        if (string.IsNullOrWhiteSpace(options.ClientCertificatePath))
        {
            logger.LogWarning("No Absa client certificate P12 fallback path is configured.");
            return null;
        }

        if (!File.Exists(options.ClientCertificatePath))
        {
            logger.LogWarning(
                "Absa client certificate was not found in the certificate store, and the configured P12 file does not exist at {CertificatePath}.",
                options.ClientCertificatePath);
            return null;
        }

        try
        {
            var fileCertificate = string.IsNullOrWhiteSpace(options.ClientCertificatePassword)
                ? X509CertificateLoader.LoadPkcs12FromFile(options.ClientCertificatePath, null)
                : X509CertificateLoader.LoadPkcs12FromFile(
                    options.ClientCertificatePath,
                    options.ClientCertificatePassword);

            if (!fileCertificate.HasPrivateKey)
            {
                fileCertificate.Dispose();
                logger.LogError(
                    "Absa client certificate P12 at {CertificatePath} does not contain a private key.",
                    options.ClientCertificatePath);
                return null;
            }

            logger.LogInformation(
                "Loaded Absa client certificate from configured P12 file {CertificatePath}; certificate thumbprint is {Thumbprint}.",
                options.ClientCertificatePath,
                fileCertificate.Thumbprint);
            return fileCertificate;
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Failed to load Absa client certificate from configured P12 file {CertificatePath}.",
                options.ClientCertificatePath);
            return null;
        }
    }

    private static string NormalizeThumbprint(string? value) =>
        string.Concat((value ?? string.Empty).Where(character => !char.IsWhiteSpace(character) && character != ':'));
}
