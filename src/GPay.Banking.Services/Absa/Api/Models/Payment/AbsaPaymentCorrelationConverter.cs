using System.Text.Json;
using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Reads Absa correlation objects. MIG samples use <c>Correlation</c> / <c>CorrelationId</c>;
/// older payloads may use <c>Type</c> / <c>Value</c>.
/// </summary>
public sealed class AbsaPaymentCorrelationConverter : JsonConverter<AbsaPaymentCorrelation>
{
    /// <inheritdoc />
    public override AbsaPaymentCorrelation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        return new AbsaPaymentCorrelation
        {
            Type = ReadInt(root, "Correlation", "Type"),
            Value = ReadString(root, "CorrelationId", "Value"),
            Id = ReadString(root, "Id")
        };
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, AbsaPaymentCorrelation value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("Correlation", value.Type);
        if (value.Value is not null)
        {
            writer.WriteString("CorrelationId", value.Value);
        }

        if (value.Id is not null)
        {
            writer.WriteString("Id", value.Id);
        }

        writer.WriteEndObject();
    }

    private static int ReadInt(JsonElement root, params string[] names)
    {
        foreach (var name in names)
        {
            if (!TryGet(root, name, out var value))
            {
                continue;
            }

            if (value.ValueKind == JsonValueKind.Number && value.TryGetInt32(out var number))
            {
                return number;
            }

            if (value.ValueKind == JsonValueKind.String &&
                int.TryParse(value.GetString(), out var parsed))
            {
                return parsed;
            }
        }

        return 0;
    }

    private static string? ReadString(JsonElement root, params string[] names)
    {
        foreach (var name in names)
        {
            if (TryGet(root, name, out var value) && value.ValueKind == JsonValueKind.String)
            {
                var text = value.GetString();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    return text.Trim();
                }
            }
        }

        return null;
    }

    private static bool TryGet(JsonElement root, string name, out JsonElement value)
    {
        foreach (var property in root.EnumerateObject())
        {
            if (string.Equals(property.Name, name, StringComparison.OrdinalIgnoreCase))
            {
                value = property.Value;
                return true;
            }
        }

        value = default;
        return false;
    }
}
