using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Chats;

public record ChatUpdateEphemeralExpirationRequest
{
    [JsonPropertyName("expiration")] public string Expiration { get; init; } = null!;
} 