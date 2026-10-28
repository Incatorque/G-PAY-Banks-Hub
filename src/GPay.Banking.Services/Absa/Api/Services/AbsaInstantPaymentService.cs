using GPay.Banking.Services.Absa.Api.Clients;
using GPay.Banking.Services.Absa.Api.Mapping;
using GPay.Banking;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;

namespace GPay.Banking.Services.Absa.Api.Services;

/// <summary>
/// Absa instant payment (PayShap / RTC) implementation per MIG – Payment API v1.8.
/// </summary>
internal sealed class AbsaInstantPaymentService : IInstantPaymentService
{
    private readonly IAbsaCapiClient _capiClient;
    private readonly IAbsaPaymentMapper _mapper;
    private readonly IBankErrorMapper _errorMapper;
    private readonly ILogger<AbsaInstantPaymentService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbsaInstantPaymentService"/> class.
    /// </summary>
    public AbsaInstantPaymentService(
        IAbsaCapiClient capiClient,
        IAbsaPaymentMapper mapper,
        IBankErrorMapper errorMapper,
        ILogger<AbsaInstantPaymentService> logger)
    {
        _capiClient = capiClient;
        _mapper = mapper;
        _errorMapper = errorMapper;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<ApiResult<InstantPaymentResponse>> PayAsync(InstantPaymentRequest request, string correlationId, CancellationToken cancellationToken = default)
    {
        var validationError = ValidatePay(request);

        if (validationError is not null)
        {
            AbsaServiceLogging.LogOutcome(_logger, "payment initiate", request, null, false, validationError.Code, validationError.Message, correlationId);

            return ApiResult<InstantPaymentResponse>.Fail(validationError, correlationId);
        }

        try
        {
            var absaRequest = _mapper.ToAbsaInitiateRequest(request);

            _logger.LogInformation(
                "Absa payment initiate start Amount={Amount} Rail={Rail} TxRef={TxRef} CorrelationId={CorrelationId}",
                absaRequest.Economics.Amount,
                absaRequest.Economics.Indicator,
                absaRequest.Economics.TransactionRef,
                correlationId);

            var absaResponse = await _capiClient.InitiatePaymentAsync(absaRequest, cancellationToken);

            if (absaResponse.HasErrors)
            {
                var first = absaResponse.ErrorList![0];
                var code = first.Code ?? "PAYMENT_ERROR";
                var message = first.Description ?? first.Message ?? "Absa payment initiate failed.";
                var apiError = _errorMapper.Map(code, message);
                AbsaServiceLogging.LogOutcome(_logger, "payment initiate", request, absaResponse, false, apiError.Code, apiError.Message, correlationId);
                return ApiResult<InstantPaymentResponse>.Fail(apiError, correlationId);
            }

            var gpayResponse = _mapper.ToGpayInitiateResponse(absaResponse, request);
            AbsaServiceLogging.LogOutcome(
                _logger,
                "payment initiate",
                request,
                absaResponse,
                true,
                gpayResponse.RawStatusLabel,
                gpayResponse.ResultDescription,
                correlationId);

            return ApiResult<InstantPaymentResponse>.Ok(gpayResponse, correlationId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Absa payment initiate failed CorrelationId={CorrelationId}", correlationId);
            var apiError = _errorMapper.Map("PAYMENT_EXCEPTION", ex.Message);
            AbsaServiceLogging.LogOutcome(_logger, "payment initiate", request, null, false, apiError.Code, apiError.Message, correlationId);
            return ApiResult<InstantPaymentResponse>.Fail(apiError, correlationId);
        }
    }

    /// <inheritdoc />
    public async Task<ApiResult<PaymentStatusResponse>> GetStatusAsync(
        PaymentStatusRequest request,
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        var validationError = ValidateStatus(request);
        if (validationError is not null)
        {
            AbsaServiceLogging.LogOutcome(_logger, "payment status", request, null, false, validationError.Code, validationError.Message, correlationId);
            return ApiResult<PaymentStatusResponse>.Fail(validationError, correlationId);
        }

        try
        {
            var absaRequest = _mapper.ToAbsaStatusRequest(request);

            _logger.LogInformation(
                "Absa payment status start CorrelationId={CorrelationId}",
                correlationId);

            var absaResponse = await _capiClient.GetPaymentStatusAsync(absaRequest, cancellationToken);

            if (absaResponse.HasErrors)
            {
                var first = absaResponse.ErrorList![0];
                var code = first.Code ?? "PAYMENT_STATUS_ERROR";
                var message = first.Description ?? first.Message ?? "Absa payment status failed.";
                var apiError = _errorMapper.Map(code, message);
                AbsaServiceLogging.LogOutcome(_logger, "payment status", request, absaResponse, false, apiError.Code, apiError.Message, correlationId);
                return ApiResult<PaymentStatusResponse>.Fail(apiError, correlationId);
            }

            var gpayResponse = _mapper.ToGpayStatusResponse(absaResponse, request);
            AbsaServiceLogging.LogOutcome(
                _logger,
                "payment status",
                request,
                absaResponse,
                true,
                gpayResponse.RawStatusLabel,
                gpayResponse.ResultDescription,
                correlationId);

            return ApiResult<PaymentStatusResponse>.Ok(gpayResponse, correlationId);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Absa payment status failed CorrelationId={CorrelationId}",
                correlationId);
            var apiError = _errorMapper.Map("PAYMENT_STATUS_EXCEPTION", ex.Message);
            AbsaServiceLogging.LogOutcome(_logger, "payment status", request, null, false, apiError.Code, apiError.Message, correlationId);
            return ApiResult<PaymentStatusResponse>.Fail(apiError, correlationId);
        }
    }

    private static ApiError? ValidatePay(InstantPaymentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FromAccountNumber))
        {
            return new ApiError { Code = "GPAY_PAY_FROM_REQUIRED", Message = "FromAccountNumber is required." };
        }

        if (string.IsNullOrWhiteSpace(request.ToAccountNumber))
        {
            return new ApiError { Code = "GPAY_PAY_TO_REQUIRED", Message = "ToAccountNumber is required." };
        }

        if (string.IsNullOrWhiteSpace(request.ToBranchCode))
        {
            return new ApiError { Code = "GPAY_PAY_BRANCH_REQUIRED", Message = "ToBranchCode is required." };
        }

        if (string.IsNullOrWhiteSpace(request.Reference))
        {
            return new ApiError { Code = "GPAY_PAY_REF_REQUIRED", Message = "Reference is required." };
        }

        if (request.Amount <= 0)
        {
            return new ApiError { Code = "GPAY_PAY_AMOUNT_INVALID", Message = "Amount must be greater than zero." };
        }

        return null;
    }

    private static ApiError? ValidateStatus(PaymentStatusRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.TransactionReference) &&
            string.IsNullOrWhiteSpace(request.ApiReference))
        {
            return new ApiError
            {
                Code = "GPAY_PAY_STATUS_REF_REQUIRED",
                Message = "TransactionReference or ApiReference is required."
            };
        }

        return null;
    }

}
