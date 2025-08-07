using Microsoft.AspNetCore.Mvc;
using WSApi.Client;
using WSAPI.Client.Examples.Webhook.Authorization;
using WSApi.Client.Models.Constants;
using WSApi.Client.Models.Events.Messages;

namespace WSAPI.Client.Examples.Webhook.Controllers;

[ApiController]
[Route("wsapi")]
public class WebhookController(ILogger<WebhookController> logger) : ControllerBase
{
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
                logger.LogInformation("Message received: {Text} From: {From} at {ReceivedAt}", messageEvent.Text, messageEvent.Sender.User, messageEvent.ReceivedAt);
                break;
            
            // Handle other event types as needed
        }
        
        return Ok();
    }
}