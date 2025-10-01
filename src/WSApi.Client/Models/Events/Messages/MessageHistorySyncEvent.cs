using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Events.Messages;

public record MessageHistorySyncEvent : BaseEvent
{
    [JsonPropertyName("chatId")] public string ChatId { get; init; } = null!;
    [JsonPropertyName("messages")] public MessageEvent[] Messages { get; init; } = null!;
}
