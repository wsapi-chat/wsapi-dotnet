using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Communities;

public record CommunityUpdateParticipantsRequest
{
    [JsonPropertyName("participants")] public string[] Participants { get; init; } = [];
    [JsonPropertyName("action")] public string Action { get; init; } = null!;
}
