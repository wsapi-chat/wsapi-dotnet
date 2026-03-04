using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WSApi.Client.SSE;

public class SSEClient : ISSEClient, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SSEClient>? _logger;
    private readonly SemaphoreSlim _connectionSemaphore = new(1, 1);

    private Task? _streamingTask;
    private bool _disposed;

    public event EventHandler<RawEventReceivedEventArgs>? RawEventReceived;
    public event EventHandler<SSEConnectionStateChangedEventArgs>? ConnectionStateChanged;

    public bool IsConnected { get; private set; }
    public TimeSpan ReconnectDelay { get; set; } = TimeSpan.FromSeconds(5);
    public bool AutoReconnect { get; set; } = true;

    public SSEClient(HttpClient httpClient, ILogger<SSEClient>? logger = null)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        await _connectionSemaphore.WaitAsync(cancellationToken);
        try
        {
            if (_streamingTask != null && !_streamingTask.IsCompleted)
            {
                _logger?.LogWarning("SSE client is already running");
                return;
            }
            
            _streamingTask = ExecuteStreamingAsync(cancellationToken);
        }
        finally
        {
            _connectionSemaphore.Release();
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        await _connectionSemaphore.WaitAsync(cancellationToken);
        try
        {
            if (_streamingTask != null)
            {
                try
                {
                    await _streamingTask;
                }
                catch (OperationCanceledException)
                {
                    // Expected when cancelling
                }
            }

            OnConnectionStateChanged(SSEConnectionState.Disconnected);
        }
        finally
        {
            _connectionSemaphore.Release();
        }
    }

    private async Task ExecuteStreamingAsync(CancellationToken cancellationToken)
    {
        _logger?.LogInformation("SSE client starting");

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await ConnectAndStreamAsync(cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "SSE connection error");
                OnConnectionStateChanged(SSEConnectionState.Error, ex);

                if (!AutoReconnect || cancellationToken.IsCancellationRequested)
                    break;

                OnConnectionStateChanged(SSEConnectionState.Reconnecting);
                _logger?.LogInformation("Retrying connection in {Delay}s", ReconnectDelay.TotalSeconds);

                try
                {
                    await Task.Delay(ReconnectDelay, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }

        _logger?.LogInformation("SSE client stopped");
    }

    private async Task ConnectAndStreamAsync(CancellationToken cancellationToken)
    {
        OnConnectionStateChanged(SSEConnectionState.Connecting);
        _logger?.LogInformation("Connecting to WSAPI event service at {Url}", _httpClient.BaseAddress);

        HttpResponseMessage? response = null;
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, new Uri(_httpClient.BaseAddress!, "events/stream"));
            response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            IsConnected = true;
            OnConnectionStateChanged(SSEConnectionState.Connected);
            _logger?.LogInformation("Connected to SSE stream");

            await ProcessStreamAsync(response, cancellationToken);
        }
        finally
        {
            IsConnected = false;
            response?.Dispose();
            OnConnectionStateChanged(SSEConnectionState.Disconnected);
            _logger?.LogInformation("WSAPI Events server disconnected");
        }
    }

    private async Task ProcessStreamAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var reader = new StreamReader(stream);

        while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
        {
            var line = await reader.ReadLineAsync(cancellationToken);

            if (string.IsNullOrWhiteSpace(line) || !line.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
                continue;

            var json = line.Substring("data:".Length).Trim();

            if (!string.IsNullOrWhiteSpace(json))
            {
                OnRawEventReceived(json);
            }
        }
    }

    private void OnRawEventReceived(string rawJson)
    {
        try
        {
            var args = new RawEventReceivedEventArgs(rawJson);
            RawEventReceived?.Invoke(this, args);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error in RawEventReceived event handler");
        }
    }

    private void OnConnectionStateChanged(SSEConnectionState state, Exception? exception = null)
    {
        try
        {
            var args = new SSEConnectionStateChangedEventArgs(state, exception);
            ConnectionStateChanged?.Invoke(this, args);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error in ConnectionStateChanged event handler");
        }
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;
        _connectionSemaphore.Dispose();
        GC.SuppressFinalize(this);
    }
}

public interface ISSEClient
{
    event EventHandler<RawEventReceivedEventArgs> RawEventReceived;
    event EventHandler<SSEConnectionStateChangedEventArgs> ConnectionStateChanged;

    bool IsConnected { get; }
    TimeSpan ReconnectDelay { get; set; }
    bool AutoReconnect { get; set; }

    Task StartAsync(CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);
}