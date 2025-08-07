using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Chats;

public record ChatInfo
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("isReadOnly")] public bool IsReadOnly { get; init; }
    [JsonPropertyName("isGroup")] public bool IsGroup { get; init; }
    [JsonPropertyName("isArchived")] public bool IsArchived { get; init; }
    [JsonPropertyName("isPinned")] public bool IsPinned { get; init; }
    [JsonPropertyName("isEphemeral")] public bool IsEphemeral { get; init; }
    [JsonPropertyName("ephemeralExpiration")] public int EphemeralExpiration { get; init; }
    [JsonPropertyName("isMuted")] public bool IsMuted { get; init; }
    [JsonPropertyName("muteEndTime")] public DateTime? MuteEndTime { get; init; }
    [JsonPropertyName("isSpam")] public bool IsSpam { get; init; }
}

