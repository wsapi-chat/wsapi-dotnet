using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Events.Session;

public record SessionLoggedErrorEvent : BaseEvent
{
    [JsonPropertyName("deviceId")] public string DeviceId { get; init; } = null!;
    [JsonPropertyName("error")] public string Error { get; init; } = null!;
}