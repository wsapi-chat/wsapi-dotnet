using System;
using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Events.Messages;

public record MessageDeleteEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("chatId")] public string ChatId { get; init; } = null!;
    [JsonPropertyName("sender")] public Sender Sender { get; init; } = null!;
    [JsonPropertyName("senderName")] public string SenderName { get; init; } = null!;
    [JsonPropertyName("time")] public DateTime Time { get; init; }
    [JsonPropertyName("isFromMe")] public bool IsFromMe { get; init; }
    [JsonPropertyName("isDeletedForMe")] public bool IsDeletedForMe { get; init; }
    [JsonPropertyName("isDeletedForAll")] public bool IsDeletedForAll { get; init; }
    [JsonPropertyName("isStatus")] public bool IsStatus { get; init; }
}
