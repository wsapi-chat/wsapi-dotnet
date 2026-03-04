using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WSAPI.Client.Examples.Webhook.Authorization;

public class WebhookAuthorizationAttribute : Attribute, IAsyncAuthorizationFilter
{
    private const string SignatureHeader = "X-Webhook-Signature";
    private const string SignaturePrefix = "sha256=";

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var signingSecret = configuration["WSAPI:SigningSecret"];

        if (string.IsNullOrEmpty(signingSecret))
            return; // Skip validation if not configured

        if (!context.HttpContext.Request.Headers.TryGetValue(SignatureHeader, out var signatureHeader)
            || string.IsNullOrEmpty(signatureHeader))
        {
            context.Result = new UnauthorizedObjectResult("Missing webhook signature");
            return;
        }

        var signature = signatureHeader.ToString();
        if (!signature.StartsWith(SignaturePrefix))
        {
            context.Result = new UnauthorizedObjectResult("Invalid webhook signature format");
            return;
        }

        // Read and buffer the request body so downstream can still read it
        context.HttpContext.Request.EnableBuffering();
        using var reader = new StreamReader(context.HttpContext.Request.Body, Encoding.UTF8, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        context.HttpContext.Request.Body.Position = 0;

        var expectedSignature = SignaturePrefix + ComputeHmacSha256(body, signingSecret);

        if (!CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(expectedSignature),
                Encoding.UTF8.GetBytes(signature)))
        {
            context.Result = new UnauthorizedObjectResult("Invalid webhook signature");
        }
    }

    private static string ComputeHmacSha256(string payload, string secret)
    {
        var keyBytes = Encoding.UTF8.GetBytes(secret);
        var payloadBytes = Encoding.UTF8.GetBytes(payload);
        var hash = HMACSHA256.HashData(keyBytes, payloadBytes);
        return Convert.ToHexStringLower(hash);
    }
}
