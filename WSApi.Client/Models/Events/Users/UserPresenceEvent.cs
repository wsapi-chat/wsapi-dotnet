using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Events.Users;

public record UserPresenceEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("status")] public string Status { get; init; } = null!;
    [JsonPropertyName("lastSeen")] public DateTime LastSeen { get; init; }
}
