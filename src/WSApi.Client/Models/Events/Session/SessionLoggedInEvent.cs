using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Events.Session;

public record SessionLoggedInEvent : BaseEvent
{
    [JsonPropertyName("deviceId")] public string DeviceId { get; init; } = null!;
}