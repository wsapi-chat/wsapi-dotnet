using WSApi.Client;
using WSApi.Client.Models.Constants;
using WSApi.Client.Models.Events.Messages;
using WSApi.Client.SSE;

namespace WSAPI.Client.Examples.SSE;

public class SSEClientService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<SSEClientService> _logger;
    private readonly ISSEClient _sseClient;

    public SSEClientService(IServiceScopeFactory scopeFactory, ILogger<SSEClientService> logger, ISSEClient sseClient)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _sseClient = sseClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Subscribe to SSE events
        _sseClient.RawEventReceived += OnRawEventReceived;
        _sseClient.ConnectionStateChanged += OnConnectionStateChanged;

        _logger.LogInformation("Starting SSE client...");
        
        // Start the SSE client
        await _sseClient.StartAsync(stoppingToken);
    }

    private void OnRawEventReceived(object? sender, RawEventReceivedEventArgs args)
    {
        _logger.LogDebug("Raw event received: {Json}", args.RawJson);

        try
        {
            // Parse the event using EventFactory
            var evt = EventFactory.ParseEvent(args.RawJson);

            // Create a scope for dependency injection (if needed)
            using var scope = _scopeFactory.CreateScope();
            
            // Handle specific event types
            switch (evt.EventType)
            {
                case EventTypes.Message:
                    var messageEvent = (MessageEvent)evt;
                    _logger.LogInformation("Message received: {Text} From: {From} at {ReceivedAt}", messageEvent.Text, messageEvent.Sender.Id, messageEvent.ReceivedAt);
                    break;

                // Add more event type handlers as needed
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling event: {ExMessage} - Json: {Json}", ex.Message, args.RawJson);
        }
    }

    private void OnConnectionStateChanged(object? sender, SSEConnectionStateChangedEventArgs args)
    {
        _logger.LogInformation("SSE Connection state changed to: {State}", args.State);

        if (args.Exception != null)
        {
            _logger.LogError(args.Exception, "Connection error occurred");
        }
    }
    

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping SSE client...");
        
        await _sseClient.StopAsync(cancellationToken);
        await base.StopAsync(cancellationToken);
    }
}