using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Account;

public record AccountUpdatePictureResponse
{
    [JsonPropertyName("pictureId")] public string PictureId { get; set; } = null!;
}