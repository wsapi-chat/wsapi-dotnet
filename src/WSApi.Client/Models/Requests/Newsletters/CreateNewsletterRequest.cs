using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Newsletters;

public record CreateNewsletterRequest
{
    [JsonPropertyName("name")] public string Name { get; init; } = null!;
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("picture")] public string? Picture { get; init; }
}
