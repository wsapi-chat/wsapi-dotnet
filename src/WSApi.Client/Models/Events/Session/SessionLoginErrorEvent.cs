using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Events.Session;

public record SessionLoginErrorEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("error")] public string Error { get; init; } = null!;
}
