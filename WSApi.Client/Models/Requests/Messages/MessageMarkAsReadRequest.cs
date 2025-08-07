using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageMarkAsReadRequest
{
    [JsonPropertyName("chatId")] public string ChatId { get; init; } = null!;
    [JsonPropertyName("senderId")] public string SenderId { get; init; } = null!;
    [JsonPropertyName("receiptType")] public string ReceiptType { get; init; } = null!;
}