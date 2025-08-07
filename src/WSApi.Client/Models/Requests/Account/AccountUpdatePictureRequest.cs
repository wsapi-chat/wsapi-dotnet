using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Account;

public record AccountUpdatePictureRequest
{
    [JsonPropertyName("pictureBase64")] public string PictureBase64 { get; set; } = null!;
}