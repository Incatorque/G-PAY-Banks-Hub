using GPay.Banking.BankHub;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Helpers;

namespace GPay.Banking;

/// <summary>Maps normalized bank adapter responses into the shared HTTP contract.</summary>
internal static class BankPaymentResultMapper
{
    public static PaymentResultDto From<T>(ApiResult<T> result, string correlationId)
        where T : IPaymentResponse
    {
        if (!result.Success || result.Data is null)
        {
            return new PaymentResultDto
            {
                CorrelationId = correlationId,
                Status = PaymentStatuses.Error,
                ErrorCode = result.Error?.Code,
                ErrorMessage = result.Error?.Message,
                AbsaError = AbsaErrorResponses.From(result.Error)
            };
        }

        var data = result.Data;
        return new PaymentResultDto
        {
            Status = PaymentStatusClassifier.Normalize(data),
            Reference = data.Reference,
            Amount = data.Amount,
            Currency = data.Currency,
            ApiReference = data.ApiReference,
            TransactionReference = data.TransactionReference ?? data.TransactionId,
            SourceStatementRef = data.SourceStatementRef,
            TargetStatementRef = data.TargetStatementRef,
            BankStatusCode = data.BankStatusCode?.ToString(),
            PaymentRail = data.PaymentRail,
            ResultDescription = data.ResultDescription,
            CorrelationId = correlationId,
            ErrorCode = data.ErrorCode,
            AbsaError = AbsaErrorResponses.FromCode(data.ErrorCode, data.ResultDescription),
            Success = true
        };
    }
}
