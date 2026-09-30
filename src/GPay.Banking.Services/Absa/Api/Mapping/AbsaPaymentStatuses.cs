namespace GPay.Banking.Services.Absa.Api.Mapping;

/// <summary>
/// Absa payment status codes from MIG – Payment API v1.8 (§5.3 and §6.3).
/// </summary>
public static class AbsaPaymentStatuses
{
    /// <summary>
    /// Maps an Absa status integer to the GPay label used on payments and callbacks.
    /// </summary>
    /// <remarks>
    /// None=0, Saved=1, Queued=2, Accepted=3, Rejected=4, NoResponse=5,
    /// InternalError=6, SystemError=7, Duplicate=8.
    /// </remarks>
    public static string ToLabel(int? statusCode) =>
        statusCode switch
        {
            1 => "Pending",
            2 => "Submitted",
            3 => "Completed",
            4 => "Failed",
            5 => "Pending",
            6 => "Failed",
            7 => "Failed",
            8 => "Duplicate",
            0 or null => "Failed",
            _ => "Pending"
        };

    /// <summary>
    /// MIG name for the status code, used as the result description when Absa returns no error text.
    /// </summary>
    public static string ToBankName(int? statusCode) =>
        statusCode switch
        {
            0 => "None",
            1 => "Saved",
            2 => "Queued",
            3 => "Accepted",
            4 => "Rejected",
            5 => "NoResponse",
            6 => "InternalError",
            7 => "SystemError",
            8 => "Duplicate",
            _ => "Unknown"
        };
}
