namespace GPay.Banking;

/// <summary>
/// Minimal acknowledgement returned to the bank after a payment callback is accepted or rejected safely.
/// </summary>
/// <remarks>
/// Absa CAPI expects a JSON body with success semantics; keep <see cref="Message"/> empty on happy path
/// where the bank MIG requires it.
/// </remarks>
public class CallbackAckDto
{
    /// <summary>
    /// <c>true</c> when the callback was accepted (including idempotent duplicates).
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Optional human-readable detail. Prefer empty string on success for Absa compatibility.
    /// </summary>
    public string Message { get; set; } = string.Empty;
}
