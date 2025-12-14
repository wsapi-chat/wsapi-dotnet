using System;
using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Events.Messages;

public record MessageReadEvent : BaseEvent
{
    [JsonPropertyName("chatId")] public string ChatId { get; init; } = null!;
    [JsonPropertyName("sender")] public Sender Sender { get; init; } = null!;
    [JsonPropertyName("time")] public DateTime Time { get; init; }
    [JsonPropertyName("isGroup")] public bool IsGroup { get; init; }
    [JsonPropertyName("messageSender")] public Sender? MessageSender { get; init; }
    [JsonPropertyName("receiptType")] public string ReceiptType { get; init; } = null!;
    [JsonPropertyName("messageIds")] public string[]? MessageIds { get; init; }
}
