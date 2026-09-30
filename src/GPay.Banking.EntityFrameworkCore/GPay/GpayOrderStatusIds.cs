namespace GPay.Banking.EntityFrameworkCore.GPay;

/// <summary>
/// GPay <c>OrderStatus</c> ids used by host-to-host (<c>OrderAudit.GetKey</c>).
/// </summary>
internal static class GpayOrderStatusIds
{
    public static readonly Guid Prs = new("7F276857-8F41-4A41-B2EA-AFD4F6C0A3BC");
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
        Prs, Fprs, SubmittedToQueue, PendingBankTransfer, BankTransferProcessing
    ];

    public static bool IsOpen(Guid statusId) => Open.Contains(statusId);

    /// <summary>
    /// Maps a hub payment label onto the host-to-host status.
    /// Submitted matches the file-sent step (Pending Bank Transfer).
    /// Completed matches an accepted payment; the statement link then moves it to Reconciled.
    /// </summary>
    public static Guid? FromHubStatus(string? hubStatus) =>
        hubStatus switch
        {
            "Submitted" or "Queued" => PendingBankTransfer,
            "Pending" or "Saved" => BankTransferProcessing,
            "Completed" or "Accepted" => Completed,
            "Failed" or "Error" or "Duplicate" or "Rejected" => Error,
            _ => null
        };
}
