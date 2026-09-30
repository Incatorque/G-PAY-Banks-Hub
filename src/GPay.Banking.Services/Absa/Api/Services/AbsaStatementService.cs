using GPay.Banking;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;

namespace GPay.Banking.Services.Absa.Api.Services;

/// <summary>
/// Absa account statement / transaction history.
/// </summary>
/// <remarks>
/// The From ABSA Corporate API pack (Payment API v1.8, AVS API v00.5, Callback API v2.9)
/// does not define an account statement or transaction-history operation. Payment status
/// is exposed on the instant-payment status API (<c>api/payment/Status</c>).
/// </remarks>
public sealed class AbsaStatementService : IStatementService, ITransactionHistoryService
{
    private readonly ILogger<AbsaStatementService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbsaStatementService"/> class.
    /// </summary>
    public AbsaStatementService(ILogger<AbsaStatementService> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public Task<ApiResult<StatementResponse>> GetStatementAsync(
        StatementRequest request,
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Absa statement not supported by CAPI Account={Account} CorrelationId={CorrelationId}",
            request.AccountNumber,
            correlationId);

        return Task.FromResult(ApiResult<StatementResponse>.Fail(
            new ApiError
            {
                Code = "ABSA_STATEMENT_NOT_SUPPORTED",
                Message =
                    "Absa Corporate API does not provide account transaction history or statements. " +
                    "Use payment status (api/payment/Status) for payment outcomes. " +
                    "Statement references are returned on payment initiate and status as sourceStatementRef and targetStatementRef."
            },
            correlationId));
    }
}
