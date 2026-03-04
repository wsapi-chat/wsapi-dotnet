using System;
using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Events.Messages;

public record MessageStarEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("chatId")] public string ChatId { get; init; } = null!;
    [JsonPropertyName("sender")] public Sender Sender { get; init; } = null!;
    [JsonPropertyName("time")] public DateTime Time { get; init; }
    [JsonPropertyName("isStarred")] public bool IsStarred { get; init; }
}
