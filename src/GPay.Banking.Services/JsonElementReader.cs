using System.Text.Json;

namespace GPay.Banking.Services;

internal static class JsonElementReader
{
    public static string ReadScalarString(object? value) => value switch
    {
        null => string.Empty,
        string text => text.Trim(),
        JsonElement element when element.ValueKind == JsonValueKind.Number => element.GetRawText(),
        JsonElement element when element.ValueKind == JsonValueKind.String => element.GetString()?.Trim() ?? string.Empty,
        _ => value.ToString()?.Trim() ?? string.Empty
    };

    public static int? ReadInt(JsonElement value)
    {
        if (value.ValueKind == JsonValueKind.Number && value.TryGetInt32(out var number))
        {
            return number;
        }

        return value.ValueKind == JsonValueKind.String &&
               int.TryParse(value.GetString(), out var parsed)
            ? parsed
            : null;
    }

    public static int ReadFirstInt(JsonElement root, params string[] names)
    {
        foreach (var name in names)
        {
            if (TryGet(root, name, out var value) && ReadInt(value) is int result)
            {
                return result;
            }
        }

        return 0;
    }

    public static string? ReadString(JsonElement root, params string[] names)
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

    public static string? FindString(JsonElement element, string propertyName)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            foreach (var property in element.EnumerateObject())
            {
                if (property.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase) &&
                    property.Value.ValueKind == JsonValueKind.String)
                {
                    return property.Value.GetString();
                }

                var nestedValue = FindString(property.Value, propertyName);
                if (nestedValue is not null)
                {
                    return nestedValue;
                }
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in element.EnumerateArray())
            {
                var nestedValue = FindString(item, propertyName);
                if (nestedValue is not null)
                {
                    return nestedValue;
                }
            }
        }

        return null;
    }

    public static bool TryGet(JsonElement root, string name, out JsonElement value)
    {
        if (root.ValueKind == JsonValueKind.Object)
        {
            foreach (var property in root.EnumerateObject())
            {
                if (string.Equals(property.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    value = property.Value;
                    return true;
                }
            }
        }

        value = default;
        return false;
    }
}
