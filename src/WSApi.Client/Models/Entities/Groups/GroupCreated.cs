using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Groups;

public record GroupCreated
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
}