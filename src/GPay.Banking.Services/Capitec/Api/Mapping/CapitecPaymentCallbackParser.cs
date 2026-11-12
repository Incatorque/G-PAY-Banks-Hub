using System.Text.Json;

namespace GPay.Banking.Services.Capitec.Api.Mapping;

/// <summary>Parses the CPS callback fields defined by the Capitec Bulk Payments API guide.</summary>
public static class CapitecPaymentCallbackParser
{
    public static CapitecParsedPaymentCallback? Parse(string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return null;
        try
        {
            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;
            if (root.ValueKind != JsonValueKind.Object) return null;
            var type = Read(root, "trantype");
            var identifier = Decode(Read(root, "unique_identifier"));
            var status = Decode(Read(root, "status"));
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(status))
                return null;

            return new CapitecParsedPaymentCallback
            {
                TransactionType = type,
                UniqueIdentifier = identifier,
                PaymentRail = Decode(Read(root, "mop")),
                StatusCode = CapitecPaymentStatuses.ToCode(status),
                StatusLabel = CapitecPaymentStatuses.ToLabel(status),
                TransactionReference = string.Equals(type, "03", StringComparison.OrdinalIgnoreCase) ? identifier : null,
                ApiReference = string.Equals(type, "03", StringComparison.OrdinalIgnoreCase) ? null : identifier,
                ErrorCode = Decode(Read(root, "err")),
                ErrorMessage = Decode(Read(root, "err"))
            };
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static string? Read(JsonElement root, string name)
    {
        foreach (var property in root.EnumerateObject())
        {
            if (!string.Equals(property.Name, name, StringComparison.OrdinalIgnoreCase)) continue;
            return property.Value.ValueKind == JsonValueKind.String ? property.Value.GetString()?.Trim() : property.Value.ToString();
        }
        return null;
    }

    private static string? Decode(string? value) => string.IsNullOrEmpty(value) ? value : Uri.UnescapeDataString(value);
}

public sealed class CapitecParsedPaymentCallback
{
    public required string TransactionType { get; init; }
    public required string UniqueIdentifier { get; init; }
    public string? PaymentRail { get; init; }
    public int? StatusCode { get; init; }
    public required string StatusLabel { get; init; }
    public string? TransactionReference { get; init; }
    public string? ApiReference { get; init; }
    public string? ErrorCode { get; init; }
    public string? ErrorMessage { get; init; }
}
