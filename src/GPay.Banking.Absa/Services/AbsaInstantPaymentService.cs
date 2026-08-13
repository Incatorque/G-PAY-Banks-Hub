using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Dtos.InstantPayment;
using GPay.Banking.Contracts.Interfaces;
using GPay.Banking.Absa.Clients;

namespace GPay.Banking.Absa.Services;

/// <summary>
/// Absa instant payment (PayShap) stub.
/// </summary>
public sealed class AbsaInstantPaymentService : IInstantPaymentService
{
    private readonly IAbsaCapiClient _capiClient;
    private readonly ILogger<AbsaInstantPaymentService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbsaInstantPaymentService"/> class.
    /// </summary>
    public AbsaInstantPaymentService(IAbsaCapiClient capiClient, ILogger<AbsaInstantPaymentService> logger)
    {
        _capiClient = capiClient;
        _logger = logger;
    }

    /// <inheritdoc />
    public Task<ApiResult<InstantPaymentResponse>> PayAsync(
        InstantPaymentRequest request,
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Absa instant payment stub Reference={Reference} Amount={Amount} CorrelationId={CorrelationId} CapiConfigured={Configured}",
            request.Reference,
            request.Amount,
            correlationId,
            _capiClient.IsConfigured);

        var response = new InstantPaymentResponse
        {
            TransactionId = $"STUB-{correlationId}",
            Status = "AcceptedStub",
            Reference = request.Reference,
            Amount = request.Amount,
            Currency = request.Currency
        };

        return Task.FromResult(ApiResult<InstantPaymentResponse>.Ok(response, correlationId));
    }
}
