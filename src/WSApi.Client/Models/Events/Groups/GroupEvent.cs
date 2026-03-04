using System;
using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Events.Groups;

public record GroupEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("sender")] public Sender? Sender { get; init; }
    [JsonPropertyName("timestamp")] public DateTime? Timestamp { get; init; }
    [JsonPropertyName("isCommunity")] public bool? IsCommunity { get; init; }
    [JsonPropertyName("communityId")] public string? CommunityId { get; init; }
    [JsonPropertyName("isAnnouncementGroup")] public bool? IsAnnouncementGroup { get; init; }
    [JsonPropertyName("joinReason")] public string? JoinReason { get; init; }
    [JsonPropertyName("groupType")] public string? GroupType { get; init; }
    [JsonPropertyName("name")] public GroupNameChange? Name { get; init; }
    [JsonPropertyName("description")] public GroupDescriptionChange? Description { get; init; }
    [JsonPropertyName("locked")] public GroupLockedChange? Locked { get; init; }
    [JsonPropertyName("announce")] public GroupAnnounceChange? Announce { get; init; }
    [JsonPropertyName("join")] public Identity[]? Join { get; init; }
    [JsonPropertyName("leave")] public Identity[]? Leave { get; init; }
    [JsonPropertyName("promote")] public Identity[]? Promote { get; init; }
    [JsonPropertyName("demote")] public Identity[]? Demote { get; init; }
    [JsonPropertyName("link")] public GroupLink? Link { get; init; }
    [JsonPropertyName("unlink")] public GroupUnlink? Unlink { get; init; }
    [JsonPropertyName("membershipApproval")] public GroupMembershipApprovalChange? MembershipApproval { get; init; }
    [JsonPropertyName("delete")] public GroupDeleteChange? Delete { get; init; }
    [JsonPropertyName("suspended")] public bool? Suspended { get; init; }

    public bool HasNameChange => Name != null;
    public bool HasDescriptionChange => Description != null;
    public bool HasLockedChange => Locked != null;
    public bool HasAnnounceChange => Announce != null;
    public bool HasJoin => Join != null && Join.Length > 0;
    public bool HasLeave => Leave != null && Leave.Length > 0;
    public bool HasPromote => Promote != null && Promote.Length > 0;
    public bool HasDemote => Demote != null && Demote.Length > 0;
    public bool HasLink => Link != null;
    public bool HasUnlink => Unlink != null;
    public bool HasMembershipApprovalChange => MembershipApproval != null;
    public bool HasDelete => Delete != null;
}

public record GroupNameChange
{
    [JsonPropertyName("name")] public string? Name { get; init; }
}

public record GroupDescriptionChange
{
    [JsonPropertyName("topic")] public string? Topic { get; init; }
}

public record GroupLockedChange
{
    [JsonPropertyName("isLocked")] public bool IsLocked { get; init; }
}

public record GroupAnnounceChange
{
    [JsonPropertyName("isAnnounce")] public bool IsAnnounce { get; init; }
}

public record GroupLink
{
    [JsonPropertyName("type")] public string Type { get; init; } = null!;
    [JsonPropertyName("groupId")] public string GroupId { get; init; } = null!;
    [JsonPropertyName("groupName")] public string? GroupName { get; init; }
    [JsonPropertyName("isAnnouncementGroup")] public bool IsAnnouncementGroup { get; init; }
}

public record GroupUnlink
{
    [JsonPropertyName("type")] public string? Type { get; init; }
    [JsonPropertyName("reason")] public string? Reason { get; init; }
    [JsonPropertyName("groupId")] public string GroupId { get; init; } = null!;
}

public record GroupMembershipApprovalChange
{
    [JsonPropertyName("isRequired")] public bool IsRequired { get; init; }
}

public record GroupDeleteChange
{
    [JsonPropertyName("reason")] public string? Reason { get; init; }
}
