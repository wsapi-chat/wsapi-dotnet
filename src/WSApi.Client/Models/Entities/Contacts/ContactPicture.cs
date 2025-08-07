using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Contacts;

public record ContactPicture
{
    [JsonPropertyName("pictureId")] public string PictureId { get; init; } = null!;
    [JsonPropertyName("pictureUrl")] public string PictureUrl { get; init; } = null!;

}