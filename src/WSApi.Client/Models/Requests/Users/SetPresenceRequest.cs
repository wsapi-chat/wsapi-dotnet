using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Users;

public record SetMyPresenceRequest
{
    [JsonPropertyName("presence")] public string Presence { get; init; } = null!;
}
