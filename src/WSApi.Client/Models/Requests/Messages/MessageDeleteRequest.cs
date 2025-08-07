using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageDeleteRequest
{
    [JsonPropertyName("chatId")] public string ChatId { get; init; } = null!;
    [JsonPropertyName("senderId")] public string SenderId { get; init; } = null!;
}   