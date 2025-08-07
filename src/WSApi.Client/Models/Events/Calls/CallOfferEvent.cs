using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Events.Calls;

public record CallOfferEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("caller")] public string Caller { get; init; } = null!;
    [JsonPropertyName("chatId")] public string ChatId { get; init; } = null!;
    [JsonPropertyName("isGroup")] public bool IsGroup { get; init; }
    [JsonPropertyName("time")] public DateTime Time { get; init; }
    [JsonPropertyName("isVideo")] public bool IsVideo { get; init; }
}