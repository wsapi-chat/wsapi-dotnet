using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Chats;

public record ChatInfo
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("lid")] public string? Lid { get; init; } = null!;
    [JsonPropertyName("isReadOnly")] public bool IsReadOnly { get; init; }
    [JsonPropertyName("isGroup")] public bool IsGroup { get; init; }
    [JsonPropertyName("isArchived")] public bool IsArchived { get; init; }
    [JsonPropertyName("isPinned")] public bool IsPinned { get; init; }
    [JsonPropertyName("isEphemeral")] public bool IsEphemeral { get; init; }
    [JsonPropertyName("ephemeralExpiration")] public string? EphemeralExpiration { get; init; }
    [JsonPropertyName("isMuted")] public bool IsMuted { get; init; }
    [JsonPropertyName("muteEndTime")] public DateTime? MuteEndTime { get; init; }
    [JsonPropertyName("isSpam")] public bool IsSpam { get; init; }
    [JsonPropertyName("fullName")] public string? FullName { get; init; }
    [JsonPropertyName("businessName")] public string BusinessName { get; init; } = null!;
    [JsonPropertyName("pushName")] public string PushName { get; init; } = null!;
    [JsonPropertyName("status")] public string Status { get; init; } = null!;
    [JsonPropertyName("lastActivity")] public DateTime? LastActivity { get; init; }
}
