using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupUpdateNameRequest
{
    [JsonPropertyName("name")] public string Name { get; init; } = null!;
}