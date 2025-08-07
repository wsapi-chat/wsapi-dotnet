using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Messages;

public class MessageReaction
{
    [JsonPropertyName("messageId")] public string MessageId { get; init; } = null!;
    // [JsonPropertyName("chatId")] public string ChatId { get; init; } = null!;
    // [JsonPropertyName("isFromMe")] public bool IsFromMe { get; init; }
    [JsonPropertyName("emoji")] public string Emoji { get; init; } = null!;
}