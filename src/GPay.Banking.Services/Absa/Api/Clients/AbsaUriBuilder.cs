namespace GPay.Banking.Services.Absa.Api.Clients;

internal static class AbsaUriBuilder
{
    public static Uri Build(string baseUrl, string relativePath) =>
        new(new Uri(baseUrl.TrimEnd('/') + "/"), relativePath.TrimStart('/'));

    public static Uri Resolve(string baseUrl, string path) =>
        path.StartsWith("http", StringComparison.OrdinalIgnoreCase)
            ? new Uri(path)
            : Build(baseUrl, path);
}
