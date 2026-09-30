using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Absa payment correlation item (MIG – Payment API v1.8).
/// </summary>
/// <remarks>
/// 1 = SourceStatementRef, 2 = TargetStatementRef, 3 = ApiRef, 4 = TransactionRef,
/// 5 = UniqueEFTnumber, 6 = PaymentNumber.
/// </remarks>
[JsonConverter(typeof(AbsaPaymentCorrelationConverter))]
public sealed class AbsaPaymentCorrelation
{
    /// <summary>
    /// Correlation type (MIG field <c>Correlation</c>).
    /// </summary>
    public int Type { get; init; }

    /// <summary>
    /// Correlation value (MIG field <c>CorrelationId</c>).
    /// </summary>
    public string? Value { get; init; }

    /// <summary>
    /// Internal unique id returned by Absa.
    /// </summary>
    public string? Id { get; init; }
}
