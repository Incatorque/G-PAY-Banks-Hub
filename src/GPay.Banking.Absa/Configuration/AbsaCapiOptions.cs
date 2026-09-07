namespace GPay.Banking.Absa.Configuration;

/// <summary>
/// Absa Corporate API (CAPI) connection and AVS settings per MIG – AVS API v1.
/// Auth: <c>POST /api/User/Authenticate</c> → Session; AVS: ValidateBankDetails (+ ValidateBankReference for non-Absa).
/// Spec: MIG – AVS API v00.5 (June 2024).
/// </summary>
public sealed class AbsaCapiOptions
{
    /// <summary>
    /// Configuration section name.
    /// </summary>
    public const string SectionName = "AbsaCapi";

    /// <summary>
    /// CAPI base URL. UAT (MIG v00.5): https://capi-uat.absa.co.za — Prod: https://capi.absa.co.za
    /// </summary>
    public string BaseUrl { get; set; } = "https://capi-uat.absa.co.za";

    /// <summary>
    /// Authenticate path.
    /// </summary>
    public string AuthenticatePath { get; set; } = "/api/User/Authenticate";

    /// <summary>
    /// Logout path.
    /// </summary>
    public string LogoutPath { get; set; } = "/api/User/Logout";

    /// <summary>
    /// Validate Bank Details path (Absa on-us synchronous; non-Absa asynchronous).
    /// </summary>
    public string ValidateBankDetailsPath { get; set; } = "/api/Account/ValidateBankDetails";

    /// <summary>
    /// Validate Bank Reference path (non-Absa second hop using ReferenceNumber).
    /// </summary>
    public string ValidateBankReferencePath { get; set; } = "/api/Account/ValidateBankReference";

    /// <summary>
    /// CAPI channel username (onboarding).
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// CAPI channel password (onboarding).
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Unique billing / CAPI code from onboarding (mandatory on AVS requests).
    /// </summary>
    public string CapiCode { get; set; } = string.Empty;

    /// <summary>
    /// X-Client-API-Key from onboarding (request header).
    /// </summary>
    public string ClientApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Default Absa bank code when the GPay request does not supply one (000016 = Absa).
    /// </summary>
    public string DefaultBankCode { get; set; } = "000016";

    /// <summary>
    /// Optional client certificate path for mTLS and X-Signature (SHA256-PSS).
    /// </summary>
    public string? ClientCertificatePath { get; set; }

    /// <summary>
    /// Optional client certificate password.
    /// </summary>
    public string? ClientCertificatePassword { get; set; }

    /// <summary>
    /// Certificate subject contains match used when loading from the Windows cert store (optional).
    /// </summary>
    public string? ClientCertificateSubjectContains { get; set; }

    /// <summary>
    /// When true, omit X-Signature (local/dev only). Live Absa requires signature per MIG §3.
    /// </summary>
    public bool SkipRequestSignature { get; set; }

    /// <summary>
    /// HTTP timeout in seconds.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 60;

    /// <summary>
    /// When true, AVS uses a simulated MIG-shaped response (no outbound CAPI call).
    /// </summary>
    public bool UseSimulator { get; set; }

    /// <summary>
    /// Session cache lifetime buffer. Absa sessions expire after ~30 minutes.
    /// </summary>
    public int SessionCacheSeconds { get; set; } = 25 * 60;

    /// <summary>
    /// Poll interval (ms) when non-Absa ValidateBankDetails returns pending (Status 001/1).
    /// </summary>
    public int ReferencePollIntervalMs { get; set; } = 2000;

    /// <summary>
    /// Max ValidateBankReference attempts for non-Absa pending results.
    /// </summary>
    public int ReferencePollMaxAttempts { get; set; } = 15;
}
