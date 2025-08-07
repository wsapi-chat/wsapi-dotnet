using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Groups;

public record GroupInfo
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("ownerId")] public string OwnerId { get; init; } = null!;
    [JsonPropertyName("name")] public string Name { get; init; } = null!;
    [JsonPropertyName("created")] public DateTime Created { get; init; } 
    [JsonPropertyName("description")] public string Description { get; init; } = null!;
    [JsonPropertyName("isAnnounce")] public bool IsAnnounce { get; init; }
    [JsonPropertyName("isLocked")] public bool IsLocked { get; init; }
    [JsonPropertyName("isEphemeral")] public bool IsEphemeral { get; init; }
    [JsonPropertyName("ephemeralExpiration")] public int EphemeralExpiration { get; init; }
    [JsonPropertyName("participants")] public GroupParticipantInfo[] Participants { get; init; } = [];
}