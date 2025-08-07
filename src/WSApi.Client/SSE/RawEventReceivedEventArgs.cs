using System;

namespace WSApi.Client.SSE;

public class RawEventReceivedEventArgs : EventArgs
{
    public string RawJson { get; }
    public DateTime ReceivedAt { get; }

    public RawEventReceivedEventArgs(string rawJson)
    {
        RawJson = rawJson ?? throw new ArgumentNullException(nameof(rawJson));
        ReceivedAt = DateTime.UtcNow;
    }
}