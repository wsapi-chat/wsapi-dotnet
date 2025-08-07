using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageStarRequest
{
    [JsonPropertyName("chatId")] public string ChatId { get; init; } = null!;
    [JsonPropertyName("senderId")] public string SenderId { get; init; } = null!;
    [JsonPropertyName("starred")] public bool Starred { get; init; }
    
}