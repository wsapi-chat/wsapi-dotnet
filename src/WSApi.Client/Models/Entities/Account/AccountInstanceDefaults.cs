using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Account;

public record AccountInstanceDefaults
{
    [JsonPropertyName("defaultWebhookUrl")] public string? DefaultWebhookUrl { get; init; }
    [JsonPropertyName("defaultSigningSecret")] public string? DefaultSigningSecret { get; init; }
    [JsonPropertyName("defaultEventFilters")] public string[]? DefaultEventFilters { get; init; }
    [JsonPropertyName("defaultHistorySync")] public bool DefaultHistorySync { get; init; }
    [JsonPropertyName("defaultPullMode")] public bool DefaultPullMode { get; init; }
}
