using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Chats;

public record ChatUpdatePresenceRequest
{
    [JsonPropertyName("state")] public string State { get; init; } = null!;
} 