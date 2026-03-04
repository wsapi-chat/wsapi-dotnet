using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Communities;

public record CommunityLinkGroupRequest
{
    [JsonPropertyName("groupId")] public string GroupId { get; init; } = null!;
}
