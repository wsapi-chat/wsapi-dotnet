using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupUpdateDescriptionRequest
{
    [JsonPropertyName("description")] public string Description { get; init; } = null!;
}