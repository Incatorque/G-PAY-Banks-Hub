using GPay.Banking.Services.Absa;
using GPay.Banking;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using GPay.Banking.Services;

namespace GPay.Banking.Services.Absa.Api.Clients;

/// <summary>
/// Signs request bodies with SHA256 + RSA-PSS using the onboarding client certificate.
/// </summary>
internal sealed class AbsaRequestSigner(
    IOptions<AbsaCapiOptions> options,
    ILogger<AbsaRequestSigner> logger) : IAbsaRequestSigner
{
    private readonly AbsaCapiOptions _options = options.Value;
    private readonly Lazy<X509Certificate2?> _certificate =
        new(() => AbsaClientCertificateLoader.Load(options.Value, logger));

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
            logger.LogWarning("Absa X-Signature skipped (AbsaCapi:SkipRequestSignature=true).");
            return;
        }

        var signature = CreateSignature(jsonBody ?? string.Empty, dateHeader);
        request.Headers.TryAddWithoutValidation("X-Signature", signature);
    }

    private string CreateSignature(string postRequest, string dateHeaderValue)
    {
        // MIG sample CreateSignedHeader:
        // "X-Date:{date}\nX-Client-API-Key:{key}\n{body}"
        var lineBreak = Environment.NewLine;
        var textualPart = $"X-Date:{dateHeaderValue}{lineBreak}X-Client-API-Key:{_options.ClientApiKey}{lineBreak}{postRequest}";

        var signatureBytes = Encoding.UTF8.GetBytes(textualPart);
        var certificate = _certificate.Value
            ?? throw new InvalidOperationException(
                "Unable to load Absa client certificate for X-Signature. " +
                "Set AbsaCapi:ClientCertificateSubjectContains to the certificate thumbprint and configure its store, " +
                "or set AbsaCapi:ClientCertificatePath as a P12 fallback. SkipRequestSignature is for local-only testing.");

        using var rsa = certificate.GetRSAPrivateKey()
            ?? throw new InvalidOperationException("Absa client certificate does not contain an RSA private key.");

        var signed = rsa.SignData(signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);
        return Convert.ToBase64String(signed);
    }
}
