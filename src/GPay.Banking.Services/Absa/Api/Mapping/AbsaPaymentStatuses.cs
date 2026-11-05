namespace GPay.Banking.Services.Absa.Api.Mapping;

/// <summary>
/// Absa payment status codes from MIG – Payment API v1.8 (§5.3 and §6.3).
/// </summary>
internal static class AbsaPaymentStatuses
{
    /// <summary>
    /// Maps an Absa status integer to the GPay label used on payments and callbacks.
    /// </summary>
    /// <remarks>
    /// None=0, Saved=1, Queued=2, Accepted=3, Rejected=4, NoResponse=5,
    /// InternalError=6, SystemError=7, Duplicate=8.
    /// </remarks>
    public static (string Label, string BankName) Map(int? statusCode) =>
        statusCode switch
        {
            0 => ("Failed", "None"),
            1 => ("Pending", "Saved"),
            2 => ("Submitted", "Queued"),
            3 => ("Completed", "Accepted"),
            4 => ("Failed", "Rejected"),
            5 => ("Pending", "NoResponse"),
            6 => ("Failed", "InternalError"),
            7 => ("Failed", "SystemError"),
            8 => ("Duplicate", "Duplicate"),
            null => ("Failed", "Unknown"),
            _ => ("Pending", "Unknown")
        };
}
