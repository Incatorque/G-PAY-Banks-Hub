using System.Text.Json;
using GPay.Banking.Services;

namespace GPay.Banking.Services.Absa.Api.Mapping;

/// <summary>
/// Parsed Absa payment callback (MIG – Payment API v1.8 §9.5).
/// </summary>
public sealed class AbsaParsedPaymentCallback
{
    /// <summary>Shared token registered with Absa.</summary>
    public string? Token { get; init; }

    /// <summary>Payment rail: RPP, IIP, or PAAF.</summary>
    public string? PaymentRail { get; init; }

    /// <summary>Absa PaymentStatus.Status integer.</summary>
    public int? StatusCode { get; init; }

    /// <summary>Normalized GPay status label.</summary>
    public required string StatusLabel { get; init; }

    /// <summary>Client transaction reference.</summary>
    public string? TransactionReference { get; init; }

    /// <summary>CAPI ApiRef (correlation 3).</summary>
    public string? ApiReference { get; init; }

    /// <summary>First rejection code when present.</summary>
    public string? ErrorCode { get; init; }

    /// <summary>First rejection description when present.</summary>
    public string? ErrorMessage { get; init; }
}

/// <summary>
/// Reads the Absa callback body. Live payloads wrap fields in <c>Data</c> with
/// <c>PaymentStatus.Status</c>, <c>TransactionRef</c>, and <c>ApiRef</c>.
/// </summary>
public static class AbsaPaymentCallbackParser
{
    /// <summary>
    /// Parses a callback body. Returns null when the JSON is not an object.
    /// </summary>
    public static AbsaParsedPaymentCallback? Parse(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }

        try
        {
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.ValueKind != JsonValueKind.Object)
            {
                return null;
            }

            var root = doc.RootElement;
            var topLevelToken = JsonElementReader.ReadString(root, "Token");
            if (JsonElementReader.TryGet(root, "Data", out var data) && data.ValueKind == JsonValueKind.Object)
            {
                root = data;
            }

            var (statusCode, errorCode, errorMessage) = ReadStatus(root);
            var (txRef, apiRef) = ReadReferences(root);

            return new AbsaParsedPaymentCallback
            {
                Token = topLevelToken ?? JsonElementReader.ReadString(root, "Token"),
                PaymentRail = JsonElementReader.ReadString(root, "Type"),
                StatusCode = statusCode,
                StatusLabel = AbsaPaymentStatuses.ToLabel(statusCode),
                TransactionReference = txRef,
                ApiReference = apiRef,
                ErrorCode = errorCode,
                ErrorMessage = errorMessage ?? (statusCode is 4 or 6 or 7
                    ? AbsaPaymentStatuses.ToBankName(statusCode)
                    : null)
            };
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static (int? StatusCode, string? ErrorCode, string? ErrorMessage) ReadStatus(JsonElement root)
    {
        if (!JsonElementReader.TryGet(root, "PaymentStatus", out var paymentStatus))
        {
            return (null, null, null);
        }

        if (paymentStatus.ValueKind is JsonValueKind.Number or JsonValueKind.String)
        {
            return (JsonElementReader.ReadInt(paymentStatus), null, null);
        }

        if (paymentStatus.ValueKind != JsonValueKind.Object)
        {
            return (null, null, null);
        }

        int? status = null;
        if (JsonElementReader.TryGet(paymentStatus, "Status", out var statusElement))
        {
            status = JsonElementReader.ReadInt(statusElement);
        }

        string? code = null;
        string? message = null;
        if (JsonElementReader.TryGet(paymentStatus, "ErrorList", out var errors) && errors.ValueKind == JsonValueKind.Array)
        {
            foreach (var error in errors.EnumerateArray())
            {
                if (error.ValueKind != JsonValueKind.Object)
                {
                    continue;
                }

                code = JsonElementReader.ReadString(error, "Code");
                message = JsonElementReader.ReadString(error, "Description") ?? JsonElementReader.ReadString(error, "Message");
                break;
            }
        }

        return (status, code, message);
    }

    private static (string? TransactionReference, string? ApiReference) ReadReferences(JsonElement root)
    {
        var txRef = JsonElementReader.ReadString(root, "TransactionRef");
        var apiRef = JsonElementReader.ReadString(root, "ApiRef");

        if (JsonElementReader.TryGet(root, "Correlations", out var correlations) && correlations.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in correlations.EnumerateArray())
            {
                if (item.ValueKind != JsonValueKind.Object)
                {
                    continue;
                }

                var type = 0;
                if (JsonElementReader.TryGet(item, "Correlation", out var correlation) || JsonElementReader.TryGet(item, "Type", out correlation))
                {
                    type = JsonElementReader.ReadInt(correlation) ?? 0;
                }

                var value = JsonElementReader.ReadString(item, "CorrelationId") ?? JsonElementReader.ReadString(item, "Value");
                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }

                if (type == 4 && string.IsNullOrWhiteSpace(txRef))
                {
                    txRef = value;
                }
                else if (type == 3 && string.IsNullOrWhiteSpace(apiRef))
                {
                    apiRef = value;
                }
            }
        }

        return (txRef, apiRef);
    }

}
