using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Events.Chats;

public record ChatPushNameEvent : BaseEvent
{
    [JsonPropertyName("user")] public Identity User { get; init; } = null!;
    [JsonPropertyName("pushName")] public string PushName { get; init; } = null!;
}
