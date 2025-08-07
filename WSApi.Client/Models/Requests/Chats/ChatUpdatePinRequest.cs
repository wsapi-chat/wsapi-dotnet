using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Chats;

public record ChatUpdatePinRequest
{
    [JsonPropertyName("pinned")] public bool Pinned { get; init; }
} 