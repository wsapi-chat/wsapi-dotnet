using System;

namespace WSApi.Client.SSE;

public class SSEConnectionStateChangedEventArgs : EventArgs
{
    public SSEConnectionState State { get; }
    public Exception? Exception { get; }

    public SSEConnectionStateChangedEventArgs(SSEConnectionState state, Exception? exception = null)
    {
        State = state;
        Exception = exception;
    }
}