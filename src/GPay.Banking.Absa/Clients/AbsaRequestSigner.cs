using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using GPay.Banking.Absa.Configuration;
using Microsoft.Extensions.Options;

namespace GPay.Banking.Absa.Clients;

/// <summary>
/// Builds Absa CAPI request security headers (MIG §3): X-Date, X-Client-API-Key, X-Signature.
/// </summary>
public interface IAbsaRequestSigner
{
    /// <summary>
    /// Applies required Absa headers to an outbound request.
    /// </summary>
    void ApplyHeaders(HttpRequestMessage request, string? jsonBody);
}

/// <summary>
/// Signs request bodies with SHA256 + RSA-PSS using the onboarding client certificate.
/// </summary>
public sealed class AbsaRequestSigner : IAbsaRequestSigner
{
    private readonly AbsaCapiOptions _options;
    private readonly ILogger<AbsaRequestSigner> _logger;
    private readonly Lazy<X509Certificate2?> _certificate;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbsaRequestSigner"/> class.
    /// </summary>
    public AbsaRequestSigner(IOptions<AbsaCapiOptions> options, ILogger<AbsaRequestSigner> logger)
    {
        _options = options.Value;
        _logger = logger;
        _certificate = new Lazy<X509Certificate2?>(LoadCertificate);
    }

    /// <inheritdoc />
    public void ApplyHeaders(HttpRequestMessage request, string? jsonBody)
    {
        if (string.IsNullOrWhiteSpace(_options.ClientApiKey))
        {
            throw new InvalidOperationException(
                "AbsaCapi:ClientApiKey is required for live CAPI calls (X-Client-API-Key header).");
        }

        // MIG sample headers use ISO-8601; keep the same value in the signature payload.
        var dateHeader = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
        request.Headers.TryAddWithoutValidation("Cache-Control", "no-cache");
        request.Headers.TryAddWithoutValidation("X-Date", dateHeader);
        request.Headers.TryAddWithoutValidation("X-Client-API-Key", _options.ClientApiKey);

        if (_options.SkipRequestSignature)
        {
            _logger.LogWarning("Absa X-Signature skipped (AbsaCapi:SkipRequestSignature=true).");
            return;
        }

        var signature = CreateSignature(jsonBody ?? string.Empty, dateHeader);
        request.Headers.TryAddWithoutValidation("X-Signature", signature);
    }

    private string CreateSignature(string postRequest, string dateHeaderValue)
    {
        // MIG sample CreateSignedHeader:
        // "X-Date:{date}\nX-Client-API-Key:{key}\n{body}"
        var textualPart = string.Format(
            System.Globalization.CultureInfo.InvariantCulture,
            "X-Date:{1}{0}X-Client-API-Key:{2}{0}{3}",
            Environment.NewLine,
            dateHeaderValue,
            _options.ClientApiKey,
            postRequest);

        var signatureBytes = Encoding.UTF8.GetBytes(textualPart);
        var certificate = _certificate.Value
            ?? throw new InvalidOperationException(
                "Unable to load Absa client certificate for X-Signature. " +
                "Set AbsaCapi:ClientCertificatePath (or store subject) or SkipRequestSignature=true for local-only testing.");

        using var rsa = certificate.GetRSAPrivateKey()
            ?? throw new InvalidOperationException("Absa client certificate does not contain an RSA private key.");

        var signed = rsa.SignData(signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);
        return Convert.ToBase64String(signed);
    }

    private X509Certificate2? LoadCertificate()
    {
        if (!string.IsNullOrWhiteSpace(_options.ClientCertificatePath) &&
            File.Exists(_options.ClientCertificatePath))
        {
            return string.IsNullOrWhiteSpace(_options.ClientCertificatePassword)
                ? X509CertificateLoader.LoadPkcs12FromFile(_options.ClientCertificatePath, null)
                : X509CertificateLoader.LoadPkcs12FromFile(
                    _options.ClientCertificatePath,
                    _options.ClientCertificatePassword);
        }

        if (string.IsNullOrWhiteSpace(_options.ClientCertificateSubjectContains) &&
            string.IsNullOrWhiteSpace(_options.ClientApiKey))
        {
            return null;
        }

        var match = _options.ClientCertificateSubjectContains ?? _options.ClientApiKey;
        try
        {
            using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            return store.Certificates
                .Cast<X509Certificate2>()
                .FirstOrDefault(c => c.Subject.Contains(match, StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to load Absa certificate from CurrentUser store.");
            return null;
        }
    }
}
