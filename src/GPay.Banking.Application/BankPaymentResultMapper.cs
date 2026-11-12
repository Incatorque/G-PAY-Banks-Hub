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
                BankError = BankErrorResponses.From(result.Error),
                AbsaError = BankErrorResponses.FromAbsa(result.Error)
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
            BankMessageIdentification = data.BankMessageIdentification,
            BankInstructionUetr = data.BankInstructionUetr,
            BankStatusCode = data.BankStatusCode?.ToString(),
            PaymentRail = data.PaymentRail,
            ResultDescription = data.ResultDescription,
            CorrelationId = correlationId,
            ErrorCode = data.ErrorCode,
            BankError = BankErrorResponses.FromCode(data.ErrorCode, data.ResultDescription, data.BankCode),
            AbsaError = BankErrorResponses.FromAbsaCode(data.ErrorCode, data.ResultDescription, data.BankCode),
            Success = true
        };
    }
}
