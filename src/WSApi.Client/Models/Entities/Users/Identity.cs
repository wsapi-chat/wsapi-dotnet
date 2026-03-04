using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Users;

public record Identity
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("lid")] public string? Lid { get; init; }
    [JsonPropertyName("phone")] public string? Phone { get; init; }
    [JsonPropertyName("device")] public int? Device { get; init; }
}
