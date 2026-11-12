using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Capitec.Api.Clients;

internal sealed class CapitecTokenResponse
{
    [JsonPropertyName("access_token")]
    public  string AccessToken { get; set; } = string.Empty;
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = string.Empty;
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
}