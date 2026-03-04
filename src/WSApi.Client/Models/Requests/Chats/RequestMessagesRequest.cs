using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Chats;

public record RequestMessagesRequest
{
    [JsonPropertyName("lastMessageId")] public string LastMessageId { get; init; } = null!;
    [JsonPropertyName("lastMessageSenderId")] public string LastMessageSenderId { get; init; } = null!;
    [JsonPropertyName("count")] public int? Count { get; init; }
}
