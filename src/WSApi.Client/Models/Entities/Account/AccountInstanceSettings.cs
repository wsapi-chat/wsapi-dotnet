using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Account;

public record AccountInstanceSettings
{
    [JsonPropertyName("useCustomDefaults")] public bool UseCustomDefaults { get; init; }
    [JsonPropertyName("pullMode")] public bool PullMode { get; init; }
    [JsonPropertyName("webhookUrl")] public string? WebhookUrl { get; init; }
    [JsonPropertyName("eventSigningSecret")] public string? EventSigningSecret { get; init; }
    [JsonPropertyName("historySync")] public bool HistorySync { get; init; }
    [JsonPropertyName("eventFilters")] public string[]? EventFilters { get; init; }
}
