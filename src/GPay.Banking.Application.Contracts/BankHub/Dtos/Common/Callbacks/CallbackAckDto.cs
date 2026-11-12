namespace GPay.Banking;

/// <summary>
/// Minimal acknowledgement returned to the bank after a payment callback is accepted or rejected safely.
/// </summary>
/// <remarks>
/// The shared acknowledgement body returned after processing a bank callback.
/// </remarks>
public class CallbackAckDto
{
    /// <summary>
    /// <c>true</c> when the callback was accepted (including idempotent duplicates).
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Optional human-readable detail.
    /// </summary>
    public string Message { get; set; } = string.Empty;
}
