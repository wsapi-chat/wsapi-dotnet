using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Users;

public record UserInfo
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("lid")] public string? Lid { get; init; }
    [JsonPropertyName("phone")] public string? Phone { get; init; }
    [JsonPropertyName("device")] public int? Device { get; init; }
    [JsonPropertyName("isInWhatsApp")] public bool IsInWhatsApp { get; init; }
    [JsonPropertyName("status")] public string Status { get; init; } = null!;
    [JsonPropertyName("isVerified")] public bool IsVerified { get; init; }
}
