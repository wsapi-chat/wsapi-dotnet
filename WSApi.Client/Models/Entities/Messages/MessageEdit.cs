using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Messages;

public record MessageEdit
{
    [JsonPropertyName("originalMessageId")] public string OriginalMessageId { get; init; } = null!;
    [JsonPropertyName("originalMessageTime")] public DateTime OriginalMessageTime { get; init; }
}