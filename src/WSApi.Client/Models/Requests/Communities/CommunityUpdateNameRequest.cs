using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Communities;

public record CommunityUpdateNameRequest
{
    [JsonPropertyName("name")] public string Name { get; init; } = null!;
}
