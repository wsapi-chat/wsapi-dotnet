using System;
using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Common;

namespace WSApi.Client.Models.Entities.Groups;

public record GroupInfo
{
    [JsonPropertyName("groupId")] public string Id { get; init; } = null!;
    [JsonPropertyName("owner")] public Identity? Owner { get; init; }
    [JsonPropertyName("name")] public string Name { get; init; } = null!;
    [JsonPropertyName("createdAt")] public DateTime Created { get; init; }
    [JsonPropertyName("description")] public string Description { get; init; } = null!;
    [JsonPropertyName("communityId")] public string? CommunityId { get; init; }
    [JsonPropertyName("isAnnouncementGroup")] public bool IsAnnouncementGroup { get; init; }
    [JsonPropertyName("isAnnounce")] public bool IsAnnounce { get; init; }
    [JsonPropertyName("isLocked")] public bool IsLocked { get; init; }
    [JsonPropertyName("isJoinApprovalRequired")] public bool IsJoinApprovalRequired { get; init; }
    [JsonPropertyName("memberAddMode")] public string? MemberAddMode { get; init; }
    [JsonPropertyName("isEphemeral")] public bool IsEphemeral { get; init; }
    [JsonPropertyName("ephemeralExpiration")] public int EphemeralExpiration { get; init; }
    [JsonPropertyName("participants")] public GroupParticipantInfo[] Participants { get; init; } = [];
}
