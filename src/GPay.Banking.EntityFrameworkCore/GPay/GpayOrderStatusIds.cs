namespace GPay.Banking.EntityFrameworkCore.GPay;

/// <summary>
/// GPay <c>OrderStatus</c> ids used by host-to-host (<c>OrderAudit.GetKey</c>).
/// </summary>
internal static class GpayOrderStatusIds
{
    public static readonly Guid BankHubPrs = new("a32560ca-2245-4a8b-871d-ea59ef5c3e37");
    public static readonly Guid Fprs = new("95F9D56C-0F2B-432B-98D3-BD753D8C4023");
    public static readonly Guid SubmittedToQueue = new("E93C9166-A328-4C27-9ABB-41187ACD60D8");
    public static readonly Guid PendingBankTransfer = new("CA2D245E-F1E4-48A1-89D3-33ABFCAA19D2");
    public static readonly Guid BankTransferProcessing = new("D79902A3-9A54-48EC-8A7D-3F7DA7DA7DE4");
    public static readonly Guid Completed = new("4AB48EE4-6A7C-4027-BEAA-D5737FA5344D");
    public static readonly Guid Reconciled = new("561D8712-A05C-472B-A88A-964C27D858DC");
    public static readonly Guid Error = new("761F645E-1AB3-4248-80CB-24EABFB47392");

    /// <summary>History category used by <c>ChangeOrderStatusAsync</c> in G-PAY.</summary>
    public static readonly Guid StatusChangeCategory = new("306BC6AA-7892-42F7-A010-DBDE49878D68");

    private static readonly Guid[] Open =
    [
        BankHubPrs, Fprs, SubmittedToQueue, PendingBankTransfer, BankTransferProcessing
    ];

    public static bool IsOpen(Guid statusId) => Open.Contains(statusId);

    /// <summary>
    /// Maps a hub payment label onto the host-to-host status.
    /// Submitted matches the initial Bank Hub handoff step (Submitted to Queue).
    /// Completed matches an accepted payment; the statement link then moves it to Reconciled.
    /// </summary>
    public static Guid? FromHubStatus(string? hubStatus) =>
        hubStatus?.Trim().ToUpperInvariant() switch
        {
            "SUBMITTED" or "QUEUED" => SubmittedToQueue,
            "PENDING" or "SAVED" or "PROCESSING" => BankTransferProcessing,
            "COMPLETED" or "ACCEPTED" => Completed,
            "FAILED" or "ERROR" or "DUPLICATE" or "REJECTED" => Error,
            _ => null
        };
}
