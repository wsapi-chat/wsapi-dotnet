using Microsoft.AspNetCore.Mvc;
using WSApi.Client;
using WSApi.Client.Models.Constants;
using WSApi.Client.Models.Events.Messages;
using WSAPI.Client.Examples.Webhook.Authorization;

namespace WSAPI.Client.Examples.Webhook.Controllers;

[ApiController]
[Route("wsapi")]
public class WebhookController(ILogger<WebhookController> logger) : ControllerBase
{
    [WebhookAuthorization]
    [HttpPost("webhook")]
    public async Task<IActionResult> Webhook(CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(HttpContext.Request.Body);
        var json = await reader.ReadToEndAsync(cancellationToken);
        var evt = EventFactory.ParseEvent(json);
        
        switch (evt.EventType)
        {
            case EventTypes.Message:
                var messageEvent = (MessageEvent)evt;
                logger.LogInformation("Message received: {Text} From: {From} at {ReceivedAt}", messageEvent.Text, messageEvent.Sender.Id, messageEvent.ReceivedAt);
                break;

            // Handle other event types as needed
        }
        
        return Ok();
    }
}