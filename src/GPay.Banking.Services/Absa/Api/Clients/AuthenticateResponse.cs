namespace GPay.Banking.Services.Absa.Api.Clients;

internal sealed class AuthenticateResponse
{
    public bool? Success { get; init; }
    public string? CorrelationId { get; init; }
    public string? Session { get; init; }
    public List<AbsaErrorItem>? ErrorList { get; init; }
}
