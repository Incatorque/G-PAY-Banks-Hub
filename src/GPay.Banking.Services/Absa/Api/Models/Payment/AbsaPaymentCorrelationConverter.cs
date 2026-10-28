using System.Text.Json;
using System.Text.Json.Serialization;
using GPay.Banking.Services;

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
            Type = JsonElementReader.ReadFirstInt(root, "Correlation", "Type"),
            Value = JsonElementReader.ReadString(root, "CorrelationId", "Value"),
            Id = JsonElementReader.ReadString(root, "Id")
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
}
