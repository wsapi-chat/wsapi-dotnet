using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Users;

public record Sender
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("user")] public string? User { get; init; }
    [JsonPropertyName("device")] public int? Device { get; init; }
    [JsonPropertyName("isMe")] public bool IsMe { get; init; }
}