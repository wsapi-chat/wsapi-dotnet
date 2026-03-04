using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Communities;

public record CommunityCreated
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
}
