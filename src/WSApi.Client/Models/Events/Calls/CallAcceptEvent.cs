using System;
using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Events.Calls;

public record CallAcceptEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("caller")] public Identity Caller { get; init; } = null!;
    [JsonPropertyName("time")] public DateTime Time { get; init; }
}
