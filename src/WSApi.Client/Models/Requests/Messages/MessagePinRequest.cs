using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessagePinRequest
{
    [JsonPropertyName("chatId")] public string ChatId { get; init; } = null!;
    [JsonPropertyName("senderId")] public string SenderId { get; init; } = null!;
    [JsonPropertyName("pinned")] public bool? Pinned { get; init; }
    [JsonPropertyName("pinExpiration")] public string? PinExpiration { get; init; }
}
