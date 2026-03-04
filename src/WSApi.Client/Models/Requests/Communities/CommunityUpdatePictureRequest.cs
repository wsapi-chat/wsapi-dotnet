using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Communities;

public record CommunityUpdatePictureRequest
{
    [JsonPropertyName("data")] public string Data { get; init; } = null!;
}
