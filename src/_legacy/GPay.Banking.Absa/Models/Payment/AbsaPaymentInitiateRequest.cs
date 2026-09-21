using System.Text.Json.Serialization;

namespace GPay.Banking.Absa.Models.Payment;

/// <summary>
/// Absa Payment Initiate request (MIG – Payment API v1.8).
/// </summary>
public sealed class AbsaPaymentInitiateRequest
{
    /// <summary>
    /// Authorisation block.
    /// </summary>
    [JsonPropertyName("Authorisation")]
    public required AbsaPaymentAuthorisation Authorisation { get; init; }

    /// <summary>
    /// Debit / source account.
    /// </summary>
    [JsonPropertyName("Source")]
    public required AbsaPaymentSource Source { get; init; }

    /// <summary>
    /// Credit / target account.
    /// </summary>
    [JsonPropertyName("Target")]
    public required AbsaPaymentTarget Target { get; init; }

    /// <summary>
    /// Economics / payment instruction.
    /// </summary>
    [JsonPropertyName("Economics")]
    public required AbsaPaymentEconomics Economics { get; init; }

    /// <summary>
    /// Optional proof of payment notification.
    /// </summary>
    [JsonPropertyName("ProofOfPayment")]
    public AbsaPaymentProofOfPayment? ProofOfPayment { get; init; }

    /// <summary>
    /// Optional callback registration for this payment.
    /// </summary>
    [JsonPropertyName("Callback")]
    public AbsaPaymentCallback? Callback { get; init; }

    /// <summary>
    /// Session from <c>/api/User/Authenticate</c>.
    /// </summary>
    [JsonPropertyName("Session")]
    public string? Session { get; set; }
}

/// <summary>
/// Absa payment authorisation block.
/// </summary>
public sealed class AbsaPaymentAuthorisation
{
    [JsonPropertyName("SubmittingEntityName")]
    public required string SubmittingEntityName { get; init; }

    [JsonPropertyName("SubsidiaryEntityName")]
    public required string SubsidiaryEntityName { get; init; }

    [JsonPropertyName("Indicator")]
    public int Indicator { get; init; }
}

/// <summary>
/// Absa payment source (debit) account.
/// </summary>
public sealed class AbsaPaymentSource
{
    [JsonPropertyName("StatementRef")]
    public required string StatementRef { get; init; }

    [JsonPropertyName("ShortName")]
    public required string ShortName { get; init; }

    [JsonPropertyName("AccountType")]
    public int AccountType { get; init; }

    [JsonPropertyName("AccountNumber")]
    public required string AccountNumber { get; init; }
}

/// <summary>
/// Absa payment target (credit) account.
/// </summary>
public sealed class AbsaPaymentTarget
{
    [JsonPropertyName("StatementRef")]
    public required string StatementRef { get; init; }

    [JsonPropertyName("AccountType")]
    public int AccountType { get; init; }

    [JsonPropertyName("AccountNumber")]
    public required string AccountNumber { get; init; }

    [JsonPropertyName("BankBranchCode")]
    public required string BankBranchCode { get; init; }

    [JsonPropertyName("Name")]
    public required string Name { get; init; }

    [JsonPropertyName("IsTrustAccount")]
    public required string IsTrustAccount { get; init; }
}

/// <summary>
/// Absa payment economics block. Amount is a string per MIG.
/// </summary>
public sealed class AbsaPaymentEconomics
{
    /// <summary>
    /// RPP = PayShap, IIP = RTC, PAAF = other.
    /// </summary>
    [JsonPropertyName("Indicator")]
    public required string Indicator { get; init; }

    [JsonPropertyName("CurrencyCode")]
    public required string CurrencyCode { get; init; }

    /// <summary>
    /// Amount as string (MIG).
    /// </summary>
    [JsonPropertyName("Amount")]
    public required string Amount { get; init; }

    [JsonPropertyName("PaymentDate")]
    public required string PaymentDate { get; init; }

    [JsonPropertyName("TransactionRef")]
    public required string TransactionRef { get; init; }
}

/// <summary>
/// Optional proof-of-payment notification.
/// </summary>
public sealed class AbsaPaymentProofOfPayment
{
    [JsonPropertyName("EmailAddress")]
    public string? EmailAddress { get; init; }

    [JsonPropertyName("MobileNumber")]
    public string? MobileNumber { get; init; }

    [JsonPropertyName("Indicator")]
    public int? Indicator { get; init; }
}

/// <summary>
/// Optional payment callback registration on initiate.
/// </summary>
public sealed class AbsaPaymentCallback
{
    [JsonPropertyName("Uri")]
    public required string Uri { get; init; }

    [JsonPropertyName("Token")]
    public required string Token { get; init; }

    [JsonPropertyName("SupportEmail")]
    public string? SupportEmail { get; init; }
}
