using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Users;

public record UserInfo
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("isInWhatsApp")] public bool IsInWhatsApp { get; init; }
    [JsonPropertyName("status")] public string Status { get; init; } = null!;
    [JsonPropertyName("pictureId")] public string PictureId { get; init; } = null!;
    [JsonPropertyName("pictureUrl")] public string PictureUrl { get; init; } = null!;
    [JsonPropertyName("isVerified")] public bool IsVerified { get; init; }
}