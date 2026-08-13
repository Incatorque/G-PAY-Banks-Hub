using System.Text.Json;
using GPay.Banking.Absa.Clients;
using GPay.Banking.Absa.Mapping;
using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Dtos.AccountVerification;
using GPay.Banking.Contracts.Interfaces;
using GPay.Banking.Contracts.Persistence;

namespace GPay.Banking.Absa.Services;

/// <summary>
/// Absa Account Verification Service (AVS) implementation per MIG – AVS API v1.
/// </summary>
public sealed class AbsaAccountVerificationService : IAccountVerificationService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly IAbsaCapiClient _capiClient;
    private readonly IAbsaAvsMapper _mapper;
    private readonly IBankErrorMapper _errorMapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AbsaAccountVerificationService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbsaAccountVerificationService"/> class.
    /// </summary>
    public AbsaAccountVerificationService(
        IAbsaCapiClient capiClient,
        IAbsaAvsMapper mapper,
        IBankErrorMapper errorMapper,
        IUnitOfWork unitOfWork,
        ILogger<AbsaAccountVerificationService> logger)
    {
        _capiClient = capiClient;
        _mapper = mapper;
        _errorMapper = errorMapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<ApiResult<AccountVerificationResponse>> VerifyAsync(
        AccountVerificationRequest request,
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        var validationError = Validate(request);
        if (validationError is not null)
        {
            LogOutcome(request, null, false, validationError.Code, validationError.Message, correlationId);
            return ApiResult<AccountVerificationResponse>.Fail(validationError, correlationId);
        }

        try
        {
            var absaRequest = _mapper.ToAbsaRequest(request, correlationId);

            _logger.LogInformation(
                "Absa AVS start Account={Account} Branch={Branch} BankCode={BankCode} CorrelationId={CorrelationId} Simulator={Simulator}",
                absaRequest.AccountNumber,
                absaRequest.BranchCode,
                absaRequest.BankCode,
                correlationId,
                _capiClient.UseSimulator);

            var absaResponse = await _capiClient.VerifyAccountAsync(absaRequest, cancellationToken);

            if (absaResponse.HasErrors)
            {
                var first = absaResponse.ErrorList![0];
                var code = first.Code ?? "AVS_ERROR";
                var message = first.Description
                    ?? first.DetailedMessages?.FirstOrDefault()?.Message
                    ?? "Absa AVS failed.";
                var apiError = _errorMapper.Map(code, message);
                LogOutcome(request, absaResponse, false, apiError.Code, apiError.Message, correlationId);
                return ApiResult<AccountVerificationResponse>.Fail(apiError, correlationId);
            }

            var gpayResponse = _mapper.ToGpayResponse(absaResponse, request.Reference);
            LogOutcome(
                request,
                absaResponse,
                gpayResponse.IsVerified,
                gpayResponse.ResultCode,
                gpayResponse.ResultDescription,
                correlationId);

            _ = _unitOfWork;

            return ApiResult<AccountVerificationResponse>.Ok(gpayResponse, correlationId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Absa AVS failed CorrelationId={CorrelationId}", correlationId);
            var apiError = _errorMapper.Map("AVS_EXCEPTION", ex.Message);
            LogOutcome(request, null, false, apiError.Code, apiError.Message, correlationId);
            return ApiResult<AccountVerificationResponse>.Fail(apiError, correlationId);
        }
    }

    private static ApiError? Validate(AccountVerificationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.AccountNumber))
        {
            return new ApiError { Code = "GPAY_AVS_ACCOUNT_REQUIRED", Message = "AccountNumber is required." };
        }

        if (string.IsNullOrWhiteSpace(request.BranchCode))
        {
            return new ApiError { Code = "GPAY_AVS_BRANCH_REQUIRED", Message = "BranchCode is required." };
        }

        if (string.IsNullOrWhiteSpace(request.IdentityNumber) &&
            request.AdditionalData?.ContainsKey("ClientIdNumber") != true)
        {
            return new ApiError
            {
                Code = "GPAY_AVS_IDENTITY_REQUIRED",
                Message = "IdentityNumber (Absa ClientIdNumber) is required."
            };
        }

        var hasName =
            !string.IsNullOrWhiteSpace(request.LastName) ||
            !string.IsNullOrWhiteSpace(request.AccountHolderName) ||
            request.AdditionalData?.ContainsKey("ClientName") == true;

        if (!hasName)
        {
            return new ApiError
            {
                Code = "GPAY_AVS_NAME_REQUIRED",
                Message = "AccountHolderName or LastName (Absa ClientName) is required."
            };
        }

        return null;
    }

    private void LogOutcome(
        AccountVerificationRequest request,
        object? response,
        bool success,
        string? code,
        string? message,
        string correlationId)
    {
        _logger.LogInformation(
            "Absa AVS completed Success={Success} Code={Code} Message={Message} CorrelationId={CorrelationId} Request={Request} Response={Response}",
            success,
            code,
            message,
            correlationId,
            JsonSerializer.Serialize(request, JsonOptions),
            response is null ? null : JsonSerializer.Serialize(response, JsonOptions));
    }
}
