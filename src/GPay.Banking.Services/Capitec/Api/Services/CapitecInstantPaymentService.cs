using System.Text.Json;
using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Services.Capitec.Api.Clients;
using GPay.Banking.Services.Capitec.Api.Mapping;
using Volo.Abp.Domain.Repositories;

namespace GPay.Banking.Services.Capitec.Api.Services;

/// <summary>Capitec CPS Bulk Payments API adapter for single-payment Bank Hub orders.</summary>
public sealed class CapitecInstantPaymentService : IInstantPaymentService
{
    private readonly ICapitecClient _client;
    private readonly ICapitecPaymentMapper _mapper;
    private readonly CapitecErrorMapper _errorMapper;
    private readonly IRepository<BankHubApiCall, Guid> _apiCalls;
    private readonly ILogger<CapitecInstantPaymentService> _logger;

    public CapitecInstantPaymentService(ICapitecClient client, ICapitecPaymentMapper mapper,
        CapitecErrorMapper errorMapper, IRepository<BankHubApiCall, Guid> apiCalls,
        ILogger<CapitecInstantPaymentService> logger)
    {
        _client = client;
        _mapper = mapper;
        _errorMapper = errorMapper;
        _apiCalls = apiCalls;
        _logger = logger;
    }

    public async Task<ApiResult<InstantPaymentResponse>> PayAsync(InstantPaymentRequest request,
        string correlationId, CancellationToken cancellationToken = default)
    {
        var validationError = ValidatePay(request);
        if (validationError is not null)
        {
            return ApiResult<InstantPaymentResponse>.Fail(validationError, correlationId);
        }

        try
        {
            var capitecRequest = _mapper.ToCapitecInitiateRequest(request, correlationId);
            var response = await _client.InitiatePaymentAsync(capitecRequest, cancellationToken);
            var result = _mapper.ToGpayInitiateResponse(response, request);
            _logger.LogInformation(
                "Capitec payout accepted. OrderId={OrderId} MessageId={MessageId} InstructionUETR={InstructionUetr} EndToEndId={EndToEndId} Amount={Amount} Method={MethodOfPayment}.",
                request.GpayOrderId, response.MessageIdentification, response.InstructionUetr,
                response.EndToEndIdentification, request.Amount, request.PaymentRail);
            return ApiResult<InstantPaymentResponse>.Ok(result, correlationId);
        }
        catch (Exception ex) when (ex is not OperationCanceledException || !cancellationToken.IsCancellationRequested)
        {
            _logger.LogError(ex, "Capitec payout request failed. CorrelationId={CorrelationId} OrderId={OrderId}.",
                correlationId, request.GpayOrderId);
            var error = _errorMapper.Map(ex is HttpRequestException { StatusCode: not null } httpError
                ? ((int)httpError.StatusCode.Value).ToString()
                : "PAYMENT_EXCEPTION", ex.Message);
            return ApiResult<InstantPaymentResponse>.Fail(error, correlationId);
        }
    }

    public async Task<ApiResult<PaymentStatusResponse>> GetStatusAsync(PaymentStatusRequest request,
        string correlationId, CancellationToken cancellationToken = default)
    {
        try
        {
            request = await RestoreBankReferencesAsync(request);
            if (string.IsNullOrWhiteSpace(request.BankMessageIdentification) || string.IsNullOrWhiteSpace(request.BankInstructionUetr))
            {
                return ApiResult<PaymentStatusResponse>.Fail(new ApiError
                {
                    Code = "CAPITEC_STATUS_IDENTIFIERS_REQUIRED",
                    Message = "Capitec status enquiry requires the original BankMessageIdentification and BankInstructionUetr returned at payment initiation."
                }, correlationId);
            }

            var capitecRequest = _mapper.ToCapitecStatusRequest(request);
            var response = await _client.GetPaymentStatusAsync(capitecRequest, cancellationToken);
            var result = _mapper.ToGpayStatusResponse(response, request);
            return ApiResult<PaymentStatusResponse>.Ok(result, correlationId);
        }
        catch (Exception ex) when (ex is not OperationCanceledException || !cancellationToken.IsCancellationRequested)
        {
            _logger.LogError(ex, "Capitec status enquiry failed. CorrelationId={CorrelationId} OrderId={OrderId}.",
                correlationId, request.GpayOrderId);
            var error = _errorMapper.Map(ex is HttpRequestException { StatusCode: not null } httpError
                ? ((int)httpError.StatusCode.Value).ToString()
                : "PAYMENT_STATUS_EXCEPTION", ex.Message);
            return ApiResult<PaymentStatusResponse>.Fail(error, correlationId);
        }
    }

    private async Task<PaymentStatusRequest> RestoreBankReferencesAsync(PaymentStatusRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.BankMessageIdentification) && !string.IsNullOrWhiteSpace(request.BankInstructionUetr))
        {
            return request;
        }
        if (request.GpayOrderId is not Guid orderId)
        {
            return request;
        }

        var calls = await _apiCalls.GetListAsync(call => call.BankCode == BankCode.Capitec.ToString() &&
            call.Direction == "Outbound" && call.GpayOrderId == orderId && call.RequestJson != null);
        var initiation = calls.OrderByDescending(call => call.CreationTime)
            .FirstOrDefault(call => call.Path != null && call.Path.Contains("creditTransferRequest/pbac", StringComparison.OrdinalIgnoreCase));
        if (initiation?.RequestJson is null)
        {
            return request;
        }

        try
        {
            using var json = JsonDocument.Parse(initiation.RequestJson);
            var root = json.RootElement;
            var messageId = root.GetProperty("groupHeader").GetProperty("messageIdentification").GetString();
            var instruction = root.GetProperty("paymentInstruction")[0].GetProperty("instructionUETR").GetString();
            return new PaymentStatusRequest
            {
                GpayOrderId = request.GpayOrderId,
                TransactionReference = request.TransactionReference,
                ApiReference = request.ApiReference,
                BankMessageIdentification = request.BankMessageIdentification ?? messageId,
                BankInstructionUetr = request.BankInstructionUetr ?? instruction
            };
        }
        catch (Exception ex) when (ex is JsonException or InvalidOperationException or KeyNotFoundException or IndexOutOfRangeException)
        {
            _logger.LogWarning(ex, "Could not restore Capitec request identifiers for order {OrderId}.", orderId);
            return request;
        }
    }

    private static ApiError? ValidatePay(InstantPaymentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FromAccountNumber)) return Error("GPAY_PAY_FROM_REQUIRED", "FromAccountNumber is required.");
        if (string.IsNullOrWhiteSpace(request.ToAccountNumber)) return Error("GPAY_PAY_TO_REQUIRED", "ToAccountNumber is required.");
        if (string.IsNullOrWhiteSpace(request.Reference)) return Error("GPAY_PAY_REF_REQUIRED", "Reference is required.");
        if (request.Reference.Trim().Length > 35) return Error("GPAY_PAY_REF_TOO_LONG", "Capitec endToEndIdentification cannot exceed 35 characters.");
        if (request.Amount <= 0) return Error("GPAY_PAY_AMOUNT_INVALID", "Amount must be greater than zero.");
        return null;
    }

    private static ApiError Error(string code, string message) => new() { Code = code, Message = message };
}
