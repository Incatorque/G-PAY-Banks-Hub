namespace GPay.Banking;

/// <summary>Selects the first non-blank configuration or request value.</summary>
public static class StringValueResolver
{
    public static string? FirstNonEmpty(params string?[] values) =>
        values.FirstOrDefault(value => !string.IsNullOrWhiteSpace(value))?.Trim();
}
