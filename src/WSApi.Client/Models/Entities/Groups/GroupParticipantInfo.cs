using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Groups;

public class GroupParticipantInfo
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("isAdmin")] public bool IsAdmin { get; init; }
    [JsonPropertyName("isSuperAdmin")] public bool IsSuperAdmin { get; init; }
    [JsonPropertyName("displayName")] public string? DisplayName { get; init; }
}