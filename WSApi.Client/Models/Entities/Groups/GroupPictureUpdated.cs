using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Groups;

public record GroupPictureUpdated
{
    [JsonPropertyName("pictureId")] public string PictureId { get; set; } = null!;
}