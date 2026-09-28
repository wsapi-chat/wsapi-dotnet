using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Groups;

public record GroupJoined
{
    [JsonPropertyName("id")] public string GroupId { get; init; } = null!;
}
