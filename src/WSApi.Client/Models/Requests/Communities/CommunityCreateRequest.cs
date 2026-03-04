using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Communities;

public record CommunityCreateRequest
{
    [JsonPropertyName("name")] public string Name { get; init; } = null!;
    [JsonPropertyName("participants")] public string[]? Participants { get; init; }
    [JsonPropertyName("approvalMode")] public string? ApprovalMode { get; init; }
}
