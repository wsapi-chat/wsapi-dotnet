using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupUpdateRequestParticipantsRequest
{
    [JsonPropertyName("participants")] public string[] Participants { get; init; } = [];
    [JsonPropertyName("action")] public string Action { get; init; } = null!;
}