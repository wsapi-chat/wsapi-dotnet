using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupJoinWithInviteRequest
{
    [JsonPropertyName("groupId")] public string GroupId { get; init; } = null!;
    [JsonPropertyName("inviterId")] public string InviterId { get; init; } = null!;
    [JsonPropertyName("code")] public string Code { get; init; } = null!;
    [JsonPropertyName("expiration")] public long? Expiration { get; init; }
}
