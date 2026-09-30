using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Helpers;
using GPay.Banking.Permissions;
using GPay.Banking.Services.Absa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace GPay.Banking;

/// <summary>
/// Channel callback URL administration AppService (ABP conventional Swagger endpoints).
/// </summary>
[Authorize(PaymentsPermissions.Default)]
public class CallbackRegistrationAppService : ApplicationService, ICallbackRegistrationAppService
{
    private readonly IBankCapabilityResolver _resolver;
    private readonly AbsaCapiOptions _absaOptions;
    private readonly BankCallbackOptions _callbackOptions;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CallbackRegistrationAppService(
        IBankCapabilityResolver resolver,
        IOptions<AbsaCapiOptions> absaOptions,
        IOptions<BankCallbackOptions> callbackOptions,
        IHttpContextAccessor httpContextAccessor)
    {
        _resolver = resolver;
        _absaOptions = absaOptions.Value;
        _callbackOptions = callbackOptions.Value;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc />
    [Authorize(PaymentsPermissions.View)]
    public Task<PaymentCallbackDefaultsDto> GetDefaultsAsync(string bank = "Absa")
    {
        _ = BankCodeParser.Parse(bank);
        var inbound = BuildRecommendedInboundUri();

        var absaBank = _callbackOptions.Banks.TryGetValue("Absa", out var cfg) ? cfg : null;

        return Task.FromResult(new PaymentCallbackDefaultsDto
        {
            Bank = "Absa",
            RecommendedInboundUri = inbound,
            ConfiguredUri = NullIfEmpty(_absaOptions.PaymentCallbackUri),
            TokenConfigured = !string.IsNullOrWhiteSpace(_absaOptions.PaymentCallbackToken),
            ConfiguredSupportEmail = NullIfEmpty(_absaOptions.PaymentCallbackSupportEmail),
            InboundTokenConfigured = !string.IsNullOrWhiteSpace(absaBank?.PaymentToken),
            RegisterPath = _absaOptions.PaymentCallbackRegisterPath,
            UnregisterPath = _absaOptions.PaymentCallbackUnregisterPath
        });
    }

    /// <inheritdoc />
    [Authorize(PaymentsPermissions.Initiate)]
    public async Task<PaymentCallbackRegistrationResultDto> RegisterAsync(RegisterPaymentCallbackRequestDto input)
    {
        var bank = BankCodeParser.Parse(input.Bank);
        var correlationId = Guid.NewGuid().ToString("N");
        var request = ResolveRegistration(input);
        var service = _resolver.GetPaymentCallbackRegistration(bank);
        var result = await service.RegisterAsync(request, correlationId);
        return Map(result, correlationId);
    }

    /// <inheritdoc />
    [Authorize(PaymentsPermissions.Initiate)]
    public async Task<PaymentCallbackRegistrationResultDto> AmendAsync(RegisterPaymentCallbackRequestDto input)
    {
        var bank = BankCodeParser.Parse(input.Bank);
        var correlationId = Guid.NewGuid().ToString("N");
        var request = ResolveRegistration(input);
        var service = _resolver.GetPaymentCallbackRegistration(bank);
        var result = await service.AmendAsync(request, correlationId);
        return Map(result, correlationId);
    }

    /// <inheritdoc />
    [Authorize(PaymentsPermissions.Initiate)]
    public async Task<PaymentCallbackRegistrationResultDto> UnregisterAsync(UnregisterPaymentCallbackRequestDto input)
    {
        var bank = BankCodeParser.Parse(input.Bank);
        var correlationId = Guid.NewGuid().ToString("N");
        var uri = FirstNonEmpty(input.Uri, _absaOptions.PaymentCallbackUri)
                  ?? throw new UserFriendlyException(
                      "Uri is required (or set AbsaCapi:PaymentCallbackUri).");

        var service = _resolver.GetPaymentCallbackRegistration(bank);
        var result = await service.UnregisterAsync(
            new PaymentCallbackUnregisterRequest { Uri = uri },
            correlationId);
        return Map(result, correlationId);
    }

    private PaymentCallbackRegistrationRequest ResolveRegistration(RegisterPaymentCallbackRequestDto input)
    {
        var uri = FirstNonEmpty(input.Uri, _absaOptions.PaymentCallbackUri, BuildRecommendedInboundUri());
        var token = FirstNonEmpty(input.Token, _absaOptions.PaymentCallbackToken);
        var email = FirstNonEmpty(input.SupportEmail, _absaOptions.PaymentCallbackSupportEmail);

        if (string.IsNullOrWhiteSpace(uri))
        {
            throw new UserFriendlyException(
                "Uri is required (or set AbsaCapi:PaymentCallbackUri / expose a public HTTPS host).");
        }

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new UserFriendlyException(
                "Token is required (or set AbsaCapi:PaymentCallbackToken).");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new UserFriendlyException(
                "SupportEmail is required (or set AbsaCapi:PaymentCallbackSupportEmail).");
        }

        return new PaymentCallbackRegistrationRequest
        {
            Uri = uri,
            Token = token,
            SupportEmail = email
        };
    }

    private string? BuildRecommendedInboundUri()
    {
        var http = _httpContextAccessor.HttpContext?.Request;
        if (http is null)
        {
            return null;
        }

        var host = http.Host.HasValue ? http.Host.Value : null;
        if (string.IsNullOrWhiteSpace(host))
        {
            return null;
        }

        // Prefer request scheme (http on localhost Swagger); Absa production requires HTTPS :443.
        var scheme = http.Scheme;
        return $"{scheme}://{host}/api/app/callback/process-payment?bank=Absa";
    }

    private static PaymentCallbackRegistrationResultDto Map(
        ApiResult<PaymentCallbackRegistrationResponse> result,
        string correlationId)
    {
        if (!result.Success || result.Data is null)
        {
            return new PaymentCallbackRegistrationResultDto
            {
                Success = false,
                CorrelationId = correlationId,
                Status = "Error",
                ErrorCode = result.Error?.Code,
                ErrorMessage = result.Error?.Message,
                AbsaError = AbsaErrorResponses.From(result.Error)
            };
        }

        var d = result.Data;
        return new PaymentCallbackRegistrationResultDto
        {
            Success = true,
            CorrelationId = correlationId,
            Status = d.Status,
            Uri = d.Uri,
            BankCorrelationId = d.BankCorrelationId,
            ResultDescription = d.ResultDescription,
            ErrorCode = d.ErrorCode,
            AbsaError = AbsaErrorResponses.FromCode(d.ErrorCode, d.ResultDescription)
        };
    }

    private static string? FirstNonEmpty(params string?[] values)
    {
        foreach (var v in values)
        {
            if (!string.IsNullOrWhiteSpace(v))
            {
                return v.Trim();
            }
        }

        return null;
    }

    private static string? NullIfEmpty(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
