using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Events.Users;

public record UserStatusEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("status")] public string Status { get; init; } = null!;
}
