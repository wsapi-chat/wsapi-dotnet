using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Communities;

public record CommunityUpdateLockedRequest
{
    [JsonPropertyName("locked")] public bool Locked { get; init; }
}
