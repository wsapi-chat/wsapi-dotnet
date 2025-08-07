using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Messages;

public record MessagePin
{
    [JsonPropertyName("messageId")] public string MessageId { get; init; } = null!;
    [JsonPropertyName("isFromMe")] public bool IsFromMe { get; init; }
    [JsonPropertyName("pinned")] public bool Pinned { get; init; }
    [JsonPropertyName("expiration")] public string Expiration { get; init; } = null!;
}