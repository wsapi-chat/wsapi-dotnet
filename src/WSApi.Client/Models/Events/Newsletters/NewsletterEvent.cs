using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Events.Newsletters;

public record NewsletterEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("action")] public string Action { get; init; } = null!;
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("role")] public string? Role { get; init; }
    [JsonPropertyName("mute")] public string? Mute { get; init; }
}
