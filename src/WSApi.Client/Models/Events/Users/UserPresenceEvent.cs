using System;
using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Events.Users;

public record UserPresenceEvent : BaseEvent
{
    [JsonPropertyName("user")] public Identity User { get; init; } = null!;
    [JsonPropertyName("status")] public string Status { get; init; } = null!;
    [JsonPropertyName("lastSeen")] public DateTime? LastSeen { get; init; }
}
