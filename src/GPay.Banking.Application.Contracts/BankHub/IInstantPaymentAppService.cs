using Volo.Abp.Application.Services;

namespace GPay.Banking.BankHub;

/// <summary>
/// Instant payment API — initiate PayShap/RTC payments, query bank status, and load persisted records.
/// </summary>
/// <remarks>
/// <para>
/// Conventional HTTP routes (ABP):
/// </para>
/// <list type="bullet">
/// <item><c>POST /api/app/instant-payment/initiate</c></item>
/// <item><c>POST /api/app/instant-payment/get-status</c></item>
/// <item><c>GET /api/app/instant-payment/{id}</c></item>
/// </list>
/// <para>
/// Requires JWT Bearer. Permissions: <c>Banking.Payments.Initiate</c>, <c>Banking.Payments.View</c>.
/// Final status may also arrive via <see cref="ICallbackAppService.ProcessPaymentAsync"/>;
/// use get-status as a fallback when callbacks are missed.
/// </para>
/// </remarks>
public interface IInstantPaymentAppService : IApplicationService
{
    /// <summary>
    /// Submits an instant payment to the bank named in the request body.
    /// </summary>
    /// <param name="input">Debit/credit accounts, amount, reference, rail, and target <c>Bank</c>.</param>
    /// <returns>
    /// Submission result with status (e.g. Submitted), bank references, <c>correlationId</c>, and <c>recordId</c>.
    /// </returns>
    /// <remarks>
    /// Permission: <c>Banking.Payments.Initiate</c>.
    /// Amount must be greater than zero. Default rail is <c>RPP</c> (PayShap).
    /// </remarks>
    Task<PaymentResultDto> InitiateAsync(InitiatePaymentRequestDto input);

    /// <summary>
    /// Queries live payment status from the bank and updates the persisted record when found.
    /// </summary>
    /// <param name="input">Bank plus <c>transactionReference</c> and/or <c>apiReference</c>.</param>
    /// <returns>Normalized status payload from the bank.</returns>
    /// <remarks>
    /// Permission: <c>Banking.Payments.View</c>.
    /// At least one of <c>transactionReference</c> or <c>apiReference</c> is required.
    /// </remarks>
    Task<PaymentResultDto> GetStatusAsync(PaymentStatusRequestDto input);

    /// <summary>
    /// Loads a previously persisted payment record by id (no live bank call).
    /// </summary>
    /// <param name="id">Payment record id returned as <c>recordId</c> from initiate/status.</param>
    /// <remarks>Permission: <c>Banking.Payments.View</c>.</remarks>
    Task<PaymentRecordDto> GetAsync(Guid id);
}
