using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Communities;

public record CommunityInviteLink
{
    [JsonPropertyName("link")] public string Link { get; init; } = null!;
}
