using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupUpdatePictureRequest
{
    [JsonPropertyName("pictureBase64")] public string PictureBase64 { get; init; } = null!;
}