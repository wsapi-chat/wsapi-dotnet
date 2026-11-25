using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupCreateRequest
{
    [JsonPropertyName("name")] public string Name { get; init; } = null!;

    /// <summary>
    /// Optional initial participant JIDs. Leave empty to create a group with only the paired account.
    /// </summary>
    [JsonPropertyName("participants")] public string[] Participants { get; init; } = [];
}
