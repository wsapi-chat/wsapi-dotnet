using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Chats;

public record ChatPin
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("isPinned")] public bool IsPinned { get; init; }
}