using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Communities;

public record CommunityPictureUpdated
{
    [JsonPropertyName("pictureId")] public string PictureId { get; init; } = null!;
}
