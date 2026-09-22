using Volo.Abp.Application.Services;

namespace GPay.Banking.BankHub;

/// <summary>
/// Account verification (AVS) API — single enquiry and async batch processing.
/// </summary>
/// <remarks>
/// <para>
/// Conventional HTTP routes (ABP):
/// </para>
/// <list type="bullet">
/// <item><c>POST /api/app/account-verification/verify</c></item>
/// <item><c>POST /api/app/account-verification/submit-batch</c></item>
/// <item><c>GET /api/app/account-verification/batch/{id}</c></item>
/// <item><c>GET /api/app/account-verification/batch-records?batchId={guid}</c></item>
/// </list>
/// <para>
/// Requires JWT Bearer. Permissions: <c>Banking.Avs.Verify</c>, <c>Banking.Avs.Upload</c>, <c>Banking.Avs.View</c>.
/// Absa AVS has no bank webhook — pending non-Absa results are handled inside the adapter via polling.
/// </para>
/// </remarks>
public interface IAccountVerificationAppService : IApplicationService
{
    /// <summary>
    /// Verifies a single bank account (AVS) via the bank named in the request body.
    /// </summary>
    /// <param name="input">Account, branch, identity/name fields, and target <c>Bank</c>.</param>
    /// <returns>
    /// Normalized match flags, result codes, <c>correlationId</c>, and persisted <c>recordId</c>.
    /// </returns>
    /// <remarks>
    /// Permission: <c>Banking.Avs.Verify</c>.
    /// Persists a <c>BankHubAvsRecord</c> for audit and dashboard drill-down.
    /// </remarks>
    Task<AccountVerificationResultDto> VerifyAsync(VerifyAccountRequestDto input);

    /// <summary>
    /// Accepts an AVS batch from an external API client (not the Angular dashboard).
    /// </summary>
    /// <param name="input">Batch metadata and ordered list of verification items.</param>
    /// <returns>Queued batch id and initial processing status.</returns>
    /// <remarks>
    /// <para>Permission: <c>Banking.Avs.Upload</c>.</para>
    /// <para>
    /// Maximum items is configured by <c>AvsBatch:MaxItems</c> (default 20 000).
    /// Items are persisted as Queued, then processed by background job <c>AvsBatchProcessJob</c>.
    /// Poll <see cref="GetBatchAsync"/> / <see cref="GetBatchRecordsAsync"/> for progress.
    /// </para>
    /// </remarks>
    Task<UploadAvsBatchResultDto> SubmitBatchAsync(SubmitAvsBatchRequestDto input);

    /// <summary>
    /// Returns batch header progress (counts, percent complete, status).
    /// </summary>
    /// <param name="id">Batch id returned by <see cref="SubmitBatchAsync"/>.</param>
    /// <remarks>Permission: <c>Banking.Avs.View</c>.</remarks>
    Task<BankHubAvsBatchDto> GetBatchAsync(Guid id);

    /// <summary>
    /// Returns all AVS records for a batch, ordered by row number.
    /// </summary>
    /// <param name="batchId">Batch id.</param>
    /// <remarks>Permission: <c>Banking.Avs.View</c>.</remarks>
    Task<List<BankHubAvsRecordDto>> GetBatchRecordsAsync(Guid batchId);
}
