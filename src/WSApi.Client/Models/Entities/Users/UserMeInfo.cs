using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Users;

public record UserMeInfo
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("lid")] public string? Lid { get; init; }
    [JsonPropertyName("deviceId")] public int DeviceId { get; init; }
    [JsonPropertyName("phone")] public string Phone { get; init; } = null!;
    [JsonPropertyName("pushName")] public string PushName { get; init; } = null!;
    [JsonPropertyName("businessName")] public string BusinessName { get; init; } = null!;
    [JsonPropertyName("status")] public string Status { get; init; } = null!;
    [JsonPropertyName("pictureId")] public string PictureId { get; init; } = null!;
    [JsonPropertyName("isVerified")] public bool IsVerified { get; init; }
}
