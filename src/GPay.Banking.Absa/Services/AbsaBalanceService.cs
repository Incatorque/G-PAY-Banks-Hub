using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Dtos.Balances;
using GPay.Banking.Contracts.Interfaces;
using GPay.Banking.Absa.Clients;

namespace GPay.Banking.Absa.Services;

/// <summary>
/// Absa balance enquiry stub.
/// </summary>
public sealed class AbsaBalanceService : IBalanceService
{
    private readonly IAbsaCapiClient _capiClient;
    private readonly ILogger<AbsaBalanceService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbsaBalanceService"/> class.
    /// </summary>
    public AbsaBalanceService(IAbsaCapiClient capiClient, ILogger<AbsaBalanceService> logger)
    {
        _capiClient = capiClient;
        _logger = logger;
    }

    /// <inheritdoc />
    public Task<ApiResult<BalanceResponse>> GetBalanceAsync(
        BalanceRequest request,
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Absa balance stub Account={Account} CorrelationId={CorrelationId} CapiConfigured={Configured}",
            request.AccountNumber,
            correlationId,
            _capiClient.IsConfigured);

        var response = new BalanceResponse
        {
            AccountNumber = request.AccountNumber,
            AvailableBalance = 0m,
            CurrentBalance = 0m,
            Currency = "ZAR",
            AsOfUtc = DateTimeOffset.UtcNow
        };

        return Task.FromResult(ApiResult<BalanceResponse>.Ok(response, correlationId));
    }
}
