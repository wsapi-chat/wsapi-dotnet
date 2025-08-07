using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WSAPI.Client.Examples.Web.Authorization;

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