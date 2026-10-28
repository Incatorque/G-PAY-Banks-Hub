using System.Text.Json;
using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Clients;

internal static class AbsaJsonOptions
{
    public static readonly JsonSerializerOptions Default = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = null,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}
