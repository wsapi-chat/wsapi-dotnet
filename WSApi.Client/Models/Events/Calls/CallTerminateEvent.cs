using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Events.Calls;

public record CallTerminateEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("caller")] public string Caller { get; init; } = null!;
    [JsonPropertyName("time")] public DateTime Time { get; init; }
    [JsonPropertyName("reason")] public string Reason { get; init; } = null!;
}