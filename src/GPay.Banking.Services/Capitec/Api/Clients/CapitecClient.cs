using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using GPay.Banking.Domain;
using GPay.Banking.Services.Capitec.Api.Models.Payment;

namespace GPay.Banking.Services.Capitec.Api.Clients;

/// <summary>HTTP client for Capitec Payments Bulk Payments API v2.5.3.</summary>
public sealed class CapitecClient : ICapitecClient
{
    private readonly HttpClient _httpClient;
    private readonly ICapitecAccessTokenProvider _sessionProvider;
    private readonly CapitecOptions _options;
    private readonly ILogger<CapitecClient> _logger;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public CapitecClient(HttpClient httpClient, ICapitecAccessTokenProvider sessionProvider,
        IOptions<CapitecOptions> options, ILogger<CapitecClient> logger)
    {
        _httpClient = httpClient;
        _sessionProvider = sessionProvider;
        _options = options.Value;
        _logger = logger;
    }

    public bool IsConfigured => IsSet(_options.BaseUrl) && IsSet(_options.ClientId) &&
        IsSet(_options.ClientSecret) && IsSet(_options.Username) && IsSet(_options.Password);

    public async Task<CapitecPaymentInitiateResponse> InitiatePaymentAsync(
        CapitecPaymentInitiateRequest request, CancellationToken cancellationToken = default)
    {
        EnsureConfigured("payment initiation");
        var json = JsonSerializer.Serialize(request, JsonOptions);
        await SendAsync(_options.CreditTransferPath, HttpMethod.Post, json, cancellationToken, request.GpayOrderId);
        var instruction = request.PaymentInstruction.Single();
        return new CapitecPaymentInitiateResponse
        {
            MessageIdentification = request.GroupHeader.MessageIdentification,
            InstructionUetr = instruction.InstructionUetr,
            EndToEndIdentification = instruction.CreditTransferTransaction.Single().EndToEndIdentification,
            StatusCode = 1
        };
    }

    public async Task<CapitecPaymentStatusResponse> GetPaymentStatusAsync(
        CapitecPaymentStatusRequest request, CancellationToken cancellationToken = default)
    {
        EnsureConfigured("status enquiry");
        var json = JsonSerializer.Serialize(request, JsonOptions);
        var body = await SendAsync(_options.StatusEnquiryPath, HttpMethod.Post, json, cancellationToken, request.GpayOrderId);
        return DeserializePaymentStatus(body);
    }

    public async Task<string> SendAsync(string relativePath, HttpMethod method, string? jsonBody,
        CancellationToken cancellationToken = default, Guid? gpayOrderId = null)
    {
        if (!Uri.TryCreate(_options.BaseUrl, UriKind.Absolute, out var baseUri))
        {
            throw new InvalidOperationException("Capitec:BaseUrl must be an absolute URL.");
        }

        var uri = Uri.TryCreate(relativePath, UriKind.Absolute, out var absoluteUri)
            ? absoluteUri
            : new Uri(baseUri, relativePath.TrimStart('/'));
        using var request = new HttpRequestMessage(method, uri);
        var accessToken = await _sessionProvider.GetAccessTokenAsync(cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        if (gpayOrderId is Guid orderId)
        {
            request.Options.Set(BankApiCallAuditMetadata.GpayOrderId, orderId);
        }
        if (jsonBody is not null)
        {
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        }

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        _logger.LogInformation("Capitec {Method} {Path} returned HTTP {StatusCode}.", method, uri.AbsolutePath, (int)response.StatusCode);
        if (!response.IsSuccessStatusCode)
        {
            var detail = string.IsNullOrWhiteSpace(responseBody) ? response.ReasonPhrase : responseBody;
            throw new HttpRequestException($"Capitec API returned HTTP {(int)response.StatusCode}: {detail}", null, response.StatusCode);
        }
        return responseBody;
    }

    private void EnsureConfigured(string operation)
    {
        if (!IsConfigured)
        {
            throw new InvalidOperationException($"Capitec is not configured for {operation}. Set BaseUrl, ClientId, ClientSecret, Username, and Password in Capitec settings.");
        }
    }

    private static bool IsSet(string? value) => !string.IsNullOrWhiteSpace(value) &&
        !value.StartsWith("SET_", StringComparison.OrdinalIgnoreCase);

    private static CapitecPaymentStatusResponse DeserializePaymentStatus(string body)
    {
        try
        {
            var items = JsonSerializer.Deserialize<List<CapitecPaymentStatusListItem>>(body, JsonOptions);
            return new CapitecPaymentStatusResponse { StatusList = items };
        }
        catch (JsonException exception)
        {
            return new CapitecPaymentStatusResponse
            {
                ErrorList = [new CapitecPaymentErrorItem
                {
                    Code = "CAPITEC_STATUS_RESPONSE_INVALID",
                    Description = $"Unable to parse Capitec status response: {exception.Message}"
                }]
            };
        }
    }
}

