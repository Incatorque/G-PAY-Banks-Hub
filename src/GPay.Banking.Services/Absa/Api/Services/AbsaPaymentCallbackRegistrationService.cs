using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Services.Absa.Api.Clients;
using GPay.Banking.Services.Absa.Api.Models.Payment;

namespace GPay.Banking.Services.Absa.Api.Services;

/// <summary>
/// Absa channel callback URL registration (Payment API v1.8 §9.2–9.3).
/// </summary>
internal sealed class AbsaPaymentCallbackRegistrationService(
    IAbsaCapiClient capiClient,
    IBankErrorMapper errorMapper,
    ILogger<AbsaPaymentCallbackRegistrationService> logger) : IPaymentCallbackRegistrationService
{
    /// <inheritdoc />
    public Task<ApiResult<PaymentCallbackRegistrationResponse>> RegisterAsync(
        PaymentCallbackRegistrationRequest request,
        string correlationId,
        CancellationToken cancellationToken = default) =>
        RegisterInternalAsync(request, correlationId, successStatus: "Registered", cancellationToken);

    /// <inheritdoc />
    public Task<ApiResult<PaymentCallbackRegistrationResponse>> AmendAsync(
        PaymentCallbackRegistrationRequest request,
        string correlationId,
        CancellationToken cancellationToken = default) =>
        // Absa has no Amend API — re-register updates Uri/Token/SupportEmail.
        RegisterInternalAsync(request, correlationId, successStatus: "Amended", cancellationToken);

    /// <inheritdoc />
    public async Task<ApiResult<PaymentCallbackRegistrationResponse>> UnregisterAsync(
        PaymentCallbackUnregisterRequest request,
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Uri))
        {
            return ApiResult<PaymentCallbackRegistrationResponse>.Fail(
                new ApiError { Code = "VALIDATION", Message = "Uri is required." },
                correlationId);
        }

        try
        {
            var uri = request.Uri.Trim();
            logger.LogInformation(
                "Absa payment callback unregister Uri={Uri} CorrelationId={CorrelationId}",
                request.Uri,
                correlationId);

            var absaResponse = await capiClient.UnregisterPaymentCallbackAsync(
                new AbsaPaymentCallbackUnregisterRequest { Uri = uri },
                cancellationToken);

            return MapResponse(
                absaResponse,
                uri,
                "Unregistered",
                correlationId,
                "CALLBACK_UNREGISTER_ERROR",
                "Absa payment callback unregistration failed.");
        }
        catch (Exception ex) when (ex is not OperationCanceledException || !cancellationToken.IsCancellationRequested)
        {
            return MapException(ex, "unregister", "CALLBACK_UNREGISTER_EXCEPTION", correlationId);
        }
    }

    private async Task<ApiResult<PaymentCallbackRegistrationResponse>> RegisterInternalAsync(
        PaymentCallbackRegistrationRequest request,
        string correlationId,
        string successStatus,
        CancellationToken cancellationToken)
    {
        var validation = ValidateRegister(request);
        if (validation is not null)
        {
            return ApiResult<PaymentCallbackRegistrationResponse>.Fail(validation, correlationId);
        }

        try
        {
            var uri = request.Uri.Trim();
            logger.LogInformation(
                "Absa payment callback register Uri={Uri} CorrelationId={CorrelationId}",
                request.Uri,
                correlationId);

            var absaResponse = await capiClient.RegisterPaymentCallbackAsync(
                new AbsaPaymentCallbackRegisterRequest
                {
                    Uri = uri,
                    Token = request.Token.Trim(),
                    SupportEmail = request.SupportEmail.Trim()
                },
                cancellationToken);

            return MapResponse(
                absaResponse,
                uri,
                successStatus,
                correlationId,
                "CALLBACK_REGISTER_ERROR",
                "Absa payment callback registration failed.");
        }
        catch (Exception ex) when (ex is not OperationCanceledException || !cancellationToken.IsCancellationRequested)
        {
            return MapException(ex, "register", "CALLBACK_REGISTER_EXCEPTION", correlationId);
        }
    }

    private ApiResult<PaymentCallbackRegistrationResponse> MapException(
        Exception exception,
        string operation,
        string errorCode,
        string correlationId)
    {
        logger.LogError(
            exception,
            "Absa payment callback {Operation} failed CorrelationId={CorrelationId}",
            operation,
            correlationId);
        return ApiResult<PaymentCallbackRegistrationResponse>.Fail(
            errorMapper.Map(errorCode, exception.Message),
            correlationId);
    }

    private ApiResult<PaymentCallbackRegistrationResponse> MapResponse(
        AbsaPaymentCallbackRegisterResponse absaResponse,
        string uri,
        string successStatus,
        string correlationId,
        string fallbackErrorCode,
        string fallbackErrorMessage)
    {
        if (!absaResponse.Succeeded)
        {
            var first = absaResponse.ErrorList?.FirstOrDefault();
            var code = first?.Code ?? fallbackErrorCode;
            var message = first?.Description ?? first?.Message ?? fallbackErrorMessage;
            var apiError = errorMapper.Map(code, message);
            return ApiResult<PaymentCallbackRegistrationResponse>.Fail(apiError, correlationId);
        }

        return ApiResult<PaymentCallbackRegistrationResponse>.Ok(
            new PaymentCallbackRegistrationResponse
            {
                Status = successStatus,
                Uri = uri,
                BankCorrelationId = absaResponse.CorrelationId,
                ResultDescription = successStatus
            },
            correlationId);
    }

    private static ApiError? ValidateRegister(PaymentCallbackRegistrationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Uri))
        {
            return new ApiError { Code = "CB0001", Message = "Callback Uri is mandatory." };
        }

        if (request.Uri.Length > 1024)
        {
            return new ApiError { Code = "CB0005", Message = "Callback Uri exceeds 1024 characters." };
        }

        if (!Uri.TryCreate(request.Uri.Trim(), UriKind.Absolute, out var parsed)
            || (parsed.Scheme != Uri.UriSchemeHttps && parsed.Scheme != Uri.UriSchemeHttp))
        {
            return new ApiError { Code = "CB0006", Message = "Callback Uri is invalid." };
        }

        if (string.IsNullOrWhiteSpace(request.Token))
        {
            return new ApiError { Code = "CB0003", Message = "Callback auth token is mandatory." };
        }

        if (request.Token.Length > 512)
        {
            return new ApiError { Code = "CB0007", Message = "Callback auth token exceeds 512 characters." };
        }

        if (string.IsNullOrWhiteSpace(request.SupportEmail))
        {
            return new ApiError { Code = "CB0004", Message = "Callback support email address is mandatory." };
        }

        if (request.SupportEmail.Length > 1024)
        {
            return new ApiError { Code = "CB0008", Message = "Callback support email address exceeds 1024 characters." };
        }

        return null;
    }
}
