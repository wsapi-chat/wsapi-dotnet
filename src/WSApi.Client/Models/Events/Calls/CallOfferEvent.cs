using System;
using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Events.Calls;

public record CallOfferEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("caller")] public Identity Caller { get; init; } = null!;
    [JsonPropertyName("chatId")] public string ChatId { get; init; } = null!;
    [JsonPropertyName("isGroup")] public bool IsGroup { get; init; }
    [JsonPropertyName("time")] public DateTime Time { get; init; }
    [JsonPropertyName("isVideo")] public bool IsVideo { get; init; }
}
