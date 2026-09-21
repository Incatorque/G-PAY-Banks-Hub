using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Dtos.Statements;
using GPay.Banking.Contracts.Interfaces;
using GPay.Banking.Absa.Clients;

namespace GPay.Banking.Absa.Services;

/// <summary>
/// Absa statement / transaction history stub.
/// </summary>
public sealed class AbsaStatementService : IStatementService, ITransactionHistoryService
{
    private readonly IAbsaCapiClient _capiClient;
    private readonly ILogger<AbsaStatementService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbsaStatementService"/> class.
    /// </summary>
    public AbsaStatementService(IAbsaCapiClient capiClient, ILogger<AbsaStatementService> logger)
    {
        _capiClient = capiClient;
        _logger = logger;
    }

    /// <inheritdoc />
    public Task<ApiResult<StatementResponse>> GetStatementAsync(
        StatementRequest request,
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Absa statement stub Account={Account} CorrelationId={CorrelationId} CapiConfigured={Configured}",
            request.AccountNumber,
            correlationId,
            _capiClient.IsConfigured);

        var response = new StatementResponse
        {
            AccountNumber = request.AccountNumber,
            Transactions = []
        };

        return Task.FromResult(ApiResult<StatementResponse>.Ok(response, correlationId));
    }
}
