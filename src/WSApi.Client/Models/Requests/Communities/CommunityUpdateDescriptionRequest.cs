using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Communities;

public record CommunityUpdateDescriptionRequest
{
    [JsonPropertyName("description")] public string? Description { get; init; }
}
