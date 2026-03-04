using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Events.Chats;

public record ChatPresenceEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("sender")] public Sender Sender { get; init; } = null!;
    [JsonPropertyName("state")] public string State { get; init; } = null!;
}
