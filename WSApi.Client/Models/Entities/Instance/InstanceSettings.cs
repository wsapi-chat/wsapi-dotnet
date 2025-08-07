using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Instance;

public record InstanceSettings
{
    [JsonPropertyName("name")] public string Name { get; init; } = null!;
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("webhookUrl")] public string? WebhookUrl { get; init; }
    [JsonPropertyName("webhookAuthHeader")] public string? WebhookAuthHeader { get; init; }
    [JsonPropertyName("webhookAuthValue")] public string? WebhookAuthValue { get; init; }
    [JsonPropertyName("pullMode")] public bool PullMode { get; init; }
    
}   