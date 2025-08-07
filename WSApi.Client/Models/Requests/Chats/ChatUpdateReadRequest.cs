using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Chats;

public record ChatUpdateReadRequest
{
    [JsonPropertyName("read")] public bool Read { get; init; }
} 