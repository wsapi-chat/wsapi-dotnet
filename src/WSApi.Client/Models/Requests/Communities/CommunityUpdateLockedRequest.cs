using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Communities;

public record CommunityUpdateLockedRequest
{
    [JsonPropertyName("enabled")] public bool Locked { get; init; }
}
