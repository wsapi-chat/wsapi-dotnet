using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Users;

public record PrivacySettings
{
    [JsonPropertyName("groupAdd")] public string? GroupAdd { get; init; }
    [JsonPropertyName("lastSeen")] public string? LastSeen { get; init; }
    [JsonPropertyName("status")] public string? Status { get; init; }
    [JsonPropertyName("profile")] public string? Profile { get; init; }
    [JsonPropertyName("readReceipts")] public string? ReadReceipts { get; init; }
    [JsonPropertyName("online")] public string? Online { get; init; }
    [JsonPropertyName("callAdd")] public string? CallAdd { get; init; }
}
