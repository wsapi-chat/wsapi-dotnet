using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageDeleteForMeRequest
{
    [JsonPropertyName("chatId")] public string ChatId { get; init; } = null!;
    [JsonPropertyName("senderId")] public string SenderId { get; init; } = null!;
    [JsonPropertyName("isFromMe")] public bool IsFromMe { get; init; }
    [JsonPropertyName("timestamp")] public DateTime Time { get; init; }
}