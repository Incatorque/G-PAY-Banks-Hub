using System.Collections.Concurrent;

namespace GPay.Banking.Infrastructure.Correlation;

/// <summary>
/// Thread-safe in-memory correlator for bank response messages.
/// </summary>
public sealed class InMemoryResponseCorrelator : IResponseCorrelator
{
    private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _pending = new();

    /// <inheritdoc />
    public TaskCompletionSource<string> Register(string correlationId, TimeSpan timeout)
    {
        var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
        _pending[correlationId] = tcs;

        _ = Task.Delay(timeout).ContinueWith(_ =>
        {
            if (_pending.TryRemove(correlationId, out var pending))
            {
                pending.TrySetException(new TimeoutException($"Timed out waiting for bank response {correlationId}."));
            }
        }, TaskScheduler.Default);

        return tcs;
    }

    /// <inheritdoc />
    public bool TryComplete(string correlationId, string payload)
    {
        if (!_pending.TryRemove(correlationId, out var tcs))
        {
            return false;
        }

        return tcs.TrySetResult(payload);
    }

    /// <inheritdoc />
    public void Cancel(string correlationId)
    {
        if (_pending.TryRemove(correlationId, out var tcs))
        {
            tcs.TrySetCanceled();
        }
    }
}
