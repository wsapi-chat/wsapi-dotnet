using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Events.Chats;

public record ChatPushNameEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("pushName")] public string PushName { get; init; } = null!;
}
