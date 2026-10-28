using GPay.Banking.BankHub;
using GPay.Banking.Domain.Dtos;

namespace GPay.Banking;

/// <summary>Maps normalized bank adapter responses into the shared HTTP contract.</summary>
internal static class BankPaymentResultMapper
{
    public static PaymentResultDto From<T>(ApiResult<T> result, string correlationId)
        where T : IPaymentResponse =>
        Map(result, correlationId, data => new PaymentResultDto
        {
            Status = data.RawStatusLabel ?? data.Status,
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
            ErrorCode = data.ErrorCode,
            AbsaError = AbsaErrorResponses.FromCode(data.ErrorCode, data.ResultDescription)
        });

    private static PaymentResultDto Map<T>(
        ApiResult<T> result,
        string correlationId,
        Func<T, PaymentResultDto> mapData)
    {
        if (!result.Success || result.Data is null)
        {
            return new PaymentResultDto
            {
                Success = false,
                CorrelationId = correlationId,
                Status = "Error",
                ErrorCode = result.Error?.Code,
                ErrorMessage = result.Error?.Message,
                AbsaError = AbsaErrorResponses.From(result.Error)
            };
        }

        var dto = mapData(result.Data);
        dto.Success = true;
        dto.CorrelationId = correlationId;
        return dto;
    }
}
