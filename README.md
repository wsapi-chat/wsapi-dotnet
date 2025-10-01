# WSApi.Client (.NET SDK)

A .NET SDK for integrating with the WSApi API, enabling developers to send WhatsApp messages, manage groups and chats, and receive real-time events via Webhooks or Server-Sent Events (SSE).

## Features

- **HTTP Client**: Simple, strongly-typed client for all API commands (messages, groups, chats, contacts, etc).
- **Events**: Receive real-time events from the API via:
  - **Webhooks**: Configure your endpoint to receive HTTP POST events.
  - **SSE**: Use the built-in SSE client to receive events over a persistent connection.
- **Example Projects**:
  - **Console Example**: Demonstrates SSE event handling.
  - **Web Example**: Demonstrates webhook event handling.

---

## Getting Started

### Prerequisites
- .NET 6.0 or later (change .csproj `TargetFramework` tag if necessary)
- Access to the WSApi API (Valid instance with ID and API key)

### Installation

#### From NuGet (Recommended)

```bash
dotnet add package WSApi.Client
```

Or using Package Manager Console in Visual Studio:
```powershell
Install-Package WSApi.Client
```

#### From Source

Clone the repository and add a reference to `WSApi.Client` in your project:

```
dotnet add package WSApi.Client --source ./WSApi.Client
```

Or add the project reference directly in your `.csproj`:

```xml
<ProjectReference Include="../WSApi.Client/WSApi.Client.csproj" />
```

---

## Usage

### 1. Sending Messages & Using the API

```csharp
// Method 1: Direct instantiation
using System.Net.Http;
using WSApi.Client.ApiClient;
using WSApi.Client.Models.Requests.Messages;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://api.wsapi.chat")
};
httpClient.DefaultRequestHeaders.Add("X-Api-Key", "<your-api-key>");
httpClient.DefaultRequestHeaders.Add("X-Instance-Id", "<instance-id>");

var messagesClient = new MessagesClient(httpClient);
var request = new MessageSendTextRequest
{
    To = "1234567890@s.whatsapp.net", // Phone number in WhatsApp format
    Text = "Hello from .NET SDK!"
};
await messagesClient.SendTextAsync(request);
```

```csharp
// Method 2: Using dependency injection (recommended)
// In Program.cs or Startup.cs:
builder.Services.AddWsApiClient("<your-api-key>", "<instance-id>");

// Then inject IWSApiClient in your services:
public class MyService
{
    private readonly IWSApiClient _wsApiClient;
    
    public MyService(IWSApiClient wsApiClient)
    {
        _wsApiClient = wsApiClient;
    }
    
    public async Task SendMessage()
    {
        var request = new MessageSendTextRequest
        {
            To = "1234567890@s.whatsapp.net", // Phone number in WhatsApp format
            Text = "Hello from .NET SDK!"
        };
        await _wsApiClient.Messages.SendTextAsync(request);
    }
}
```

All API endpoints are available via the corresponding client classes in `WSApi.Client.ApiClient` (e.g., `GroupsClient`, `ChatsClient`, `ContactsClient`, etc) or through the unified `IWSApiClient` interface.

#### Exception Handling: Two Method Versions

For each API method available in the SDK, there are **two versions** to handle different error scenarios:

1. **Standard Methods** (e.g., `SendTextAsync()`, `SendImageAsync()`, etc.)
   - **Throws exceptions** when API calls fail
   - Returns the result directly on success
   - Use when you want exceptions to bubble up for centralized error handling

2. **Try Methods** (e.g., `TrySendTextAsync()`, `TrySendImageAsync()`, etc.)
   - **Never throws exceptions**
   - Returns an `ApiResponse<T>` object that contains either the result or error details
   - Use when you want to handle errors inline without exception handling

**ApiResponse<T> Object Structure:**
- `Result`: Contains the successful response data (null if failed)
- `Error`: Contains error details as `ProblemDetails` (null if successful)  
- `IsSuccess`: Boolean indicating if the operation was successful

**Example:**

```csharp
// Method 1: Exception-based (throws on error)
try
{
    var result = await messagesClient.SendTextAsync(request);
    Console.WriteLine($"Message sent with ID: {result.MessageId}");
}
catch (ApiException ex)
{
    Console.WriteLine($"Failed to send message: {ex.Message}");
}

// Method 2: ApiResponse-based (no exceptions)
var response = await messagesClient.TrySendTextAsync(request);
if (response.IsSuccess)
{
    Console.WriteLine($"Message sent with ID: {response.Result.MessageId}");
}
else
{
    Console.WriteLine($"Failed to send message: {response.Error.Detail}");
}
```

### 2. Receiving Events

#### a) Webhooks

1. **Configure your webhook URL** in the WSApi dashboard.

2. **Set up authentication** using the `WebhookAuthorizationAttribute`:

```csharp
// Authorization/WebhookAuthorizationAttribute.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class WebhookAuthorizationAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var webhookHeader = configuration["WSAPI:WebhookHeader"];
        var webhookSecret = configuration["WSAPI:WebhookSecret"];

        if (string.IsNullOrEmpty(webhookHeader) || string.IsNullOrEmpty(webhookSecret))
            return; // Skip validation if not configured

        if (!context.HttpContext.Request.Headers.TryGetValue(webhookHeader, out var headerValue) || headerValue != webhookSecret)
        {
            context.Result = new UnauthorizedObjectResult("Invalid or missing webhook secret");
        }
    }
}
```

3. **Configure your application** in `Program.cs`:

```csharp
using WSApi.Client;

var builder = WebApplication.CreateBuilder(args);

// Configure WSAPI Client
var apiKey = builder.Configuration["WSAPI:ApiKey"] ?? throw new InvalidOperationException("WSAPI:ApiKey is not set in configuration");
var instanceId = builder.Configuration["WSAPI:InstanceId"] ?? throw new InvalidOperationException("WSAPI:InstanceId is not set in configuration");
builder.Services.AddWsApiClient(apiKey, instanceId);

builder.Services.AddControllers();

var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.Run();
```

4. **Create your webhook controller**:

```csharp
using Microsoft.AspNetCore.Mvc;
using WSApi.Client;
using WSApi.Client.Models.Constants;
using WSApi.Client.Models.Events.Messages;

[ApiController]
[Route("wsapi")]
public class WebhookController : ControllerBase
{
    private readonly ILogger<WebhookController> _logger;

    public WebhookController(ILogger<WebhookController> logger)
    {
        _logger = logger;
    }

    [WebhookAuthorization]
    [HttpPost("webhook")]
    public async Task<IActionResult> Webhook(CancellationToken cancellationToken)
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync(cancellationToken);
        var evt = EventFactory.ParseEvent(json);
        
        switch (evt.EventType)
        {
            case EventTypes.Message:
                var messageEvent = (MessageEvent)evt;
                _logger.LogInformation("Message received: {Text} From: {From} at {ReceivedAt}", 
                    messageEvent.Text, messageEvent.Sender.User, messageEvent.ReceivedAt);
                break;
            
            // Handle other event types as needed
        }
        
        return Ok();
    }
}
```

5. **Configure authentication in `appsettings.json`**:

```json
{
  "WSAPI": {
    "ApiKey": "sk_your_api_key_here",
    "InstanceId": "ins_your_instance_id_here",
    "WebhookHeader": "X-Auth-Secret",
    "WebhookSecret": "your_webhook_secret_here"
  }
}
```

The webhook authorization is not required, but strongly recommended. The `WebhookAuthorizationAttribute` validates incoming webhook requests by checking for a specific header (configured as `WebhookHeader`) containing a secret value (configured as `WebhookSecret`). This ensures that only authorized requests from WSApi reach your webhook endpoint. If the header is missing or contains an incorrect value, the request is rejected with a 401 Unauthorized response.

#### b) SSE (Server-Sent Events)

1. **Configure your application** in `Program.cs`:

```csharp
using WSApi.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSimpleConsole(o =>
{
    o.TimestampFormat = "[HH:mm:ss.fff] ";
    o.SingleLine = true;
});

//Configure WSAPI Client
var apiKey = builder.Configuration["WSAPI:ApiKey"] ?? throw new InvalidOperationException("WSApi:ApiKey is not set in configuration");
var instanceId = builder.Configuration["WSAPI:InstanceId"] ?? throw new InvalidOperationException("WSApi:InstanceId is not set in configuration");
builder.Services.AddWsApiClient(apiKey,  instanceId);
//End WSAPI Config

//Add SSE Client Service
builder.Services.AddHostedService<SSEClientService>();

var app = builder.Build();
app.Run();

```

2. **Create an SSE service** to handle events:

```csharp
using WSApi.Client;
using WSApi.Client.Models.Constants;
using WSApi.Client.Models.Events.Messages;
using WSApi.Client.SSE;

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
                    _logger.LogInformation("Message received: {Text} From: {From} at {ReceivedAt}", 
                        messageEvent.Text, messageEvent.SenderName, messageEvent.ReceivedAt);
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
```

3. **Configure your `appsettings.json`**:

```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Debug",
            "System": "Information",
            "Microsoft": "Information"
        }
    },
    "WSAPI": {
        "ApiKey": "sk_your_api_key_here",
        "InstanceId": "ins_your_instance_id_here"
    }
}
```

#### Available Event Types

The SDK supports the following event types that you can handle in your application:

**Session Events:**
- `SessionLoggedInEvent` - When the WhatsApp session is established
- `SessionLoggedOutEvent` - When the session is terminated
- `SessionLoggedErrorEvent` - When there's an authentication error
- `SessionInitialSyncFinishedEvent` - When the initial synchronization has finished

**Message Events:**
- `MessageEvent` - New incoming/outgoing messages
- `MessageDeleteEvent` - When messages are deleted
- `MessageReadEvent` - When messages are read
- `MessageStarEvent` - When messages are starred
- `MessageHistorySyncEvent` - Message history synchronization

**Chat Events:**
- `ChatPresenceEvent` - User typing indicators, online status
- `ChatSettingEvent` - Chat settings changes
- `ChatPushNameEvent` - Chat display name changes
- `ChatPictureEvent` - Chat picture updates
- `ChatPresenceEvent` - Chat Online/offline status changes

**Contact Events:**
- `ContactEvent` - Contact information updates

**User Events:**
- `UserStatusEvent` - Status message updates

**Call Events:**
- `CallOfferEvent` - Incoming call offers
- `CallAcceptEvent` - Call acceptance
- `CallTerminateEvent` - Call termination

You only need to implement handlers for the events relevant to your application. The `EventFactory.ParseEvent()` method automatically deserializes the JSON into the appropriate strongly-typed event object.
