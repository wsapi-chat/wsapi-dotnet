namespace WSApi.Client.SSE;

public enum SSEConnectionState
{
    Connecting,
    Connected,
    Disconnected,
    Error,
    Reconnecting
}