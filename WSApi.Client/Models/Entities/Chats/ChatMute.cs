using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Chats;

public record ChatMute
{
    [JsonPropertyName("isMuted")] public bool IsMuted { get; init; }
    [JsonPropertyName("mutedEndTime")] public DateTime? MutedEndTime { get; init; }
}