using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Status;

public record StatusPrivacy
{
    [JsonPropertyName("type")] public string? Type { get; init; }
    [JsonPropertyName("list")] public string[] List { get; init; } = [];
    [JsonPropertyName("isDefault")] public bool IsDefault { get; init; }
}
