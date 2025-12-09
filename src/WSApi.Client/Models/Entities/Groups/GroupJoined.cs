using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Groups;

public record GroupJoined
{
    [JsonPropertyName("groupId")] public string GroupId { get; init; } = null!;
}
