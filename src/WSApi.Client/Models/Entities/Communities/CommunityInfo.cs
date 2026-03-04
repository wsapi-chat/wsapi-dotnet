using System;
using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Common;
using WSApi.Client.Models.Entities.Groups;

namespace WSApi.Client.Models.Entities.Communities;

public record CommunityInfo
{
    [JsonPropertyName("communityId")] public string CommunityId { get; init; } = null!;
    [JsonPropertyName("owner")] public Identity? Owner { get; init; }
    [JsonPropertyName("name")] public string Name { get; init; } = null!;
    [JsonPropertyName("created")] public DateTime Created { get; init; }
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("isLocked")] public bool IsLocked { get; init; }
    [JsonPropertyName("communityApprovalMode")] public string? CommunityApprovalMode { get; init; }
    [JsonPropertyName("participants")] public GroupParticipantInfo[] Participants { get; init; } = [];
}
