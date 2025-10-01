using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Chats;

public record ChatPicture
{
    [JsonPropertyName("pictureId")] public string PictureId { get; init; } = null!;
    [JsonPropertyName("pictureUrl")] public string PictureUrl { get; init; } = null!;

}