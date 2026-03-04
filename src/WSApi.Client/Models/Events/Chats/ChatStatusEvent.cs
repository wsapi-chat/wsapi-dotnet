using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Events.Chats;

public record ChatStatusEvent : BaseEvent
{
    [JsonPropertyName("user")] public Identity User { get; init; } = null!;
    [JsonPropertyName("status")] public string Status { get; init; } = null!;
}
