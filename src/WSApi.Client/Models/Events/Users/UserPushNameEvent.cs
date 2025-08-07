using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Events.Users;

public record UserPushNameEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("pushName")] public string PushName { get; init; } = null!;
}
