# WSApi.Client (.NET SDK)

[![.NET](https://img.shields.io/badge/.NET-8.0%20|%209.0%20|%2010.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![CI](https://github.com/wsapi-chat/wsapi-dotnet/actions/workflows/ci.yml/badge.svg)](https://github.com/wsapi-chat/wsapi-dotnet/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/WSApi.Client?logo=nuget&logoColor=white)](https://www.nuget.org/packages/WSApi.Client)

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
- .NET 8.0 or later
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
    Console.WriteLine($"Message sent with ID: {result.Id}");
}
catch (ApiException ex)
{
    Console.WriteLine($"Failed to send message: {ex.Message}");
}

// Method 2: ApiResponse-based (no exceptions)
var response = await messagesClient.TrySendTextAsync(request);
if (response.IsSuccess)
{
    Console.WriteLine($"Message sent with ID: {response.Result?.Id}");
}
else
{
    Console.WriteLine($"Failed to send message: {response.Error.Detail}");
}
```

#### Pairing and Passkey Verification

Pair a device with `Session.GetLoginQRImageAsync()` / `GetLoginQRCodeAsync()` or `GetLoginPairCodeAsync(phone)`. Some WhatsApp accounts then require the owner's **passkey** (WebAuthn) before the link completes:

- `Session.GetSessionStatusAsync()` reports `PasskeyStatus` (see `PasskeyStatuses`). While it is `required`, `submitted` or `confirmation_required` (`PasskeyStatuses.IsPending(...)`), **stop polling** the QR / pair-code endpoints: they return HTTP 409 (an `ApiException` / `ApiResponse.Error` with `Status == 409`) and generating a new code would discard the pairing.
- `Session.GetPasskeyStateAsync()` returns the `PasskeyState`, including the WebAuthn `Challenge` and `Origin` while the status is `required`, and `ConfirmationCode` while it is `confirmation_required`.
- The challenge must be signed in a browser on `https://web.whatsapp.com` with `navigator.credentials.get()` (a server cannot sign it). Send the JSON from `PublicKeyCredential.toJSON()` with `Session.SubmitPasskeyAsync(assertion)`; `PasskeyAssertion` deserializes directly from that JSON.
- `Session.RequestPasskeyAsync()` asks WhatsApp for a challenge when the notification does not arrive, or to start a passkey-only link without a QR or pair code.
- When the status is `confirmation_required`, show `ConfirmationCode` so the owner can compare it and approve on their phone. The server sends the confirmation automatically; `Session.ConfirmPasskeyAsync()` is an optional compatibility call.
- The step expires 5 minutes after it is requested (`ExpiresAt`); the status then becomes `error` and a new QR or pair code must be generated.

```csharp
var status = await client.Session.GetSessionStatusAsync();
if (PasskeyStatuses.IsPending(status.PasskeyStatus))
{
    var state = await client.Session.GetPasskeyStateAsync();
    if (state.Status == PasskeyStatuses.Required)
    {
        // Hand state.Challenge to a browser on state.Origin, then submit the result:
        var assertion = JsonSerializer.Deserialize<PasskeyAssertion>(credentialJsonFromBrowser)!;
        state = await client.Session.SubmitPasskeyAsync(assertion);
    }
    if (state.Status == PasskeyStatuses.ConfirmationRequired)
        Console.WriteLine($"Approve on your phone; code: {state.ConfirmationCode}");
}
```

### 2. Receiving Events

#### a) Webhooks

Configure the webhook URL and signing secret on your instance. Validate the `X-Webhook-Signature` HMAC-SHA256 signature against the **original request bytes**, before parsing the event. Missing or invalid signatures must be rejected with HTTP 401 when signing is enabled.

The runnable [webhook example](src/WSAPI.Client.Examples.Webhook) includes a buffering authorization filter and controller. Configure `WSAPI:SigningSecret` to match the instance; a static `X-Auth-Secret` header is not the WSAPI signature protocol. Keep production secrets outside committed configuration.

```csharp
var evt = EventFactory.ParseEvent(json);
if (evt is MessageEvent message)
{
    Console.WriteLine($"Message from {message.Sender.Id}: {message.Text}");
    // Optional attribution, also present on messages inside history events:
    var clickId = message.AdReferral?.CtwaClid;
}
```

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
                        messageEvent.Text, messageEvent.Sender.Id, messageEvent.ReceivedAt);
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
- `SessionLoginErrorEvent` - When there's a pairing/authentication error
- `SessionInitialSyncFinished` - When the initial history synchronization has completed

**Message Events:**
- `MessageEvent` - New incoming/outgoing messages
- `MessageDeleteEvent` - When messages are deleted
- `MessageReadEvent` - When messages are read/delivered
- `MessageStarEvent` - When messages are starred/unstarred
- `MessageHistorySyncEvent` - Message history synchronization

**Chat Events:**
- `ChatPresenceEvent` - User typing indicators (typing, recording, paused)
- `ChatSettingEvent` - Chat settings changes (mute, pin, read, archive, ephemeral)
- `ChatPushNameEvent` - Chat push name updates
- `ChatPictureEvent` - Chat picture updates
- `ChatStatusEvent` - Chat about/status text updates

**Contact Events:**
- `ContactEvent` - Contact information updates

**Group Events:**
- `GroupEvent` - Group and community updates (name/topic changes, members join/leave, admin promote/demote, community linking)

**Newsletter Events:**
- `NewsletterEvent` - Newsletter subscription and mute changes

**User Events:**
- `UserPresenceEvent` - User online/offline status and last seen

**Call Events:**
- `CallOfferEvent` - Incoming call offers
- `CallAcceptEvent` - Call acceptance
- `CallTerminateEvent` - Call termination

You only need to implement handlers for the events relevant to your application. The `EventFactory.ParseEvent()` method automatically deserializes the JSON into the appropriate strongly-typed event object.

## Unreleased contract update

The working source includes current REST routes and payloads, passkey pairing (`Session.*Passkey*`, `SessionStatus.PasskeyStatus`), optional group-creation participants, `UserInfo.VerifiedName`, optional ad attribution on message/history events, and the optional name when creating a Cloud subscription instance. These changes are not published yet. See [CHANGELOG.md](CHANGELOG.md) for compatibility details. Cloud Account management and SSE are not the OSS administrative API; OSS uses `/admin/instances` and webhook/Redis event delivery.
