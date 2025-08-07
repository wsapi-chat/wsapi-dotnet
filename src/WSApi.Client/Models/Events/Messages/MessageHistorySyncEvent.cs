using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Events.Messages;

public record MessageHistorySyncEvent : BaseEvent
{
    [JsonPropertyName("messages")] public MessageEvent[] Messages { get; init; } = null!;   
}
